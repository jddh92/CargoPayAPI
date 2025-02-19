using CargoPayAPI.Model;

namespace CargoPayAPI.Service.Interfaces
{
    public interface ICardService
    {
        public Task<Card> CreateCardAsync();
        public Task<decimal?> GetBalanceAsync(string cardNumber);
        public Task<bool> PayAsync(string cardNumber, decimal amount);
    }
}
