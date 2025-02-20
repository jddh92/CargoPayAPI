using CargoPayAPI.Model;
using CargoPayAPI.Repository.Interfaces;
using CargoPayAPI.Service.Interfaces;

namespace CargoPayAPI.Service
{
    public class CardService : ICardService
    {
        private readonly ICardRepository _cardRepository;

        public CardService(ICardRepository cardRepository)
        {
            _cardRepository = cardRepository;
        }

        public async Task<Card> CreateCardAsync()
        {
            string cardNumber;
            Card existingCard;
            do
            {
                cardNumber = new string(Enumerable.Repeat("0123456789", 15)
                    .Select(s => s[new Random().Next(s.Length)]).ToArray());
                existingCard = await _cardRepository.GetCardAsync(cardNumber);
            } while (existingCard != null);

            return await _cardRepository.CreateCardAsync(cardNumber);
        }

        public async Task<decimal?> GetBalanceAsync(string cardNumber)
        {
            var card = await _cardRepository.GetCardAsync(cardNumber);
            return card?.Balance;
        }

        public async Task<bool> PayAsync(string cardNumber, decimal amount)
        {
            return await _cardRepository.PayAsync(cardNumber, amount);
        }
    }
}
