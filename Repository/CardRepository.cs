using CargoPayAPI.Data;
using CargoPayAPI.Model;
using CargoPayAPI.Repository.Interfaces;
using CargoPayAPI.Service.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CargoPayAPI.Repository
{
    public class CardRepository : ICardRepository
    {
        private readonly AppDbContext _context;
        private readonly IPaymentFeeService _paymentFeeService;


        public CardRepository(AppDbContext context, IPaymentFeeService paymentFeeService)
        {
            _context = context;
            _paymentFeeService = paymentFeeService;
        }

        public async Task<Card> CreateCardAsync(string cardNumber)
        {
            var card = new Card { CardNumber = cardNumber, Balance = 1000 };
            _context.Cards.Add(card);
            await _context.SaveChangesAsync();
            return card;
        }

        public async Task<Card?> GetCardAsync(string cardNumber)
        {
            return await _context.Cards.FirstOrDefaultAsync(c => c.CardNumber == cardNumber);
        }

        public async Task<bool> PayAsync(string cardNumber, decimal amount)
        {
            var card = await GetCardAsync(cardNumber);
            if (card == null)
                return false;

            decimal fee = _paymentFeeService.GetCurrentFee();
            decimal totalAmount = amount + (amount * fee);

            if (card.Balance < totalAmount)
                return false;

            card.Balance -= totalAmount;

            var transaction = new Transaction
            {
                CardId = card.Id,
                Amount = amount,
                FeeApplied = fee
            };

            _context.Transactions.Add(transaction);
            await _context.SaveChangesAsync();
            return true;
        }

    }
}
