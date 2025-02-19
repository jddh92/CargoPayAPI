using CargoPayAPI.Service.Interfaces;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CargoPayAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
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
            var card = await _cardService.CreateCardAsync();
            return Ok(card);
        }

        [HttpGet("balance")]
        public async Task<IActionResult> GetBalance(string cardNumber)
        {
            var balance = await _cardService.GetBalanceAsync(cardNumber);
            if (balance == null) return NotFound("Card not found");
            return Ok(balance);
        }

        [HttpPost("pay")]
        public async Task<IActionResult> Pay(string cardNumber, decimal amount)
        {
            var success = await _cardService.PayAsync(cardNumber, amount);
            if (!success) return BadRequest("Payment failed");
            return Ok("Payment successful");
        }
    }
}
