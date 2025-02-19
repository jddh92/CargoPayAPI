using CargoPayAPI.Model;

namespace CargoPayAPI.Repository.Interfaces
{
    public interface ICardRepository
    {
       public Task<Card> CreateCardAsync(string cardNumber);
       public Task<Card?> GetCardAsync(string cardNumber);
       public Task<bool> PayAsync(string cardNumber, decimal amount);
    }
}
