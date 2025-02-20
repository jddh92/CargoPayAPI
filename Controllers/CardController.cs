using CargoPayAPI.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace CargoPayAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CardController : ControllerBase
    {
        private readonly ICardService _cardService;

        public CardController(ICardService cardService)
        {
            _cardService = cardService;
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateCard()
        {
            var card = await Task.Run(() => _cardService.CreateCardAsync());
            return Ok(card);
        }

        [HttpGet("balance")]
        public async Task<IActionResult> GetBalance(string cardNumber)
        {
            if (string.IsNullOrEmpty(cardNumber) || cardNumber.Length != 15)
                return BadRequest("Card number must be 15 digits.");

            var balance = await Task.Run(() => _cardService.GetBalanceAsync(cardNumber));
            if (balance == null) return NotFound("Card not found");
            return Ok(balance);
        }

        [HttpPost("pay")]
        public async Task<IActionResult> Pay(string cardNumber, decimal amount)
        {
            if (string.IsNullOrEmpty(cardNumber) || cardNumber.Length != 15)
                return BadRequest("Card number must be 15 digits.");

            if (amount <= 0)
                return BadRequest("Amount must be greater than 0.");

            var success = await Task.Run(() => _cardService.PayAsync(cardNumber, amount));
            if (!success) return BadRequest("Payment failed");
            return Ok("Payment successful");
        }
    }
}
