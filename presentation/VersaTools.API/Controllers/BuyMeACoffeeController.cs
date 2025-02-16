

using Microsoft.AspNetCore.Mvc;
using Stripe;
using VersaTools.Application.DTOs.StripeDTO;

namespace VersaTools.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BuyMeACoffeeController : ControllerBase
    {
        [HttpPost("tip")]
        public async Task<IActionResult> Tip([FromBody] TipRequest request)
        {
            if (request == null || request.Amount <= 0)
            {
                return BadRequest(new { error = "Invalid value" });
            }

            try
            {
                var options = new PaymentIntentCreateOptions
                {
                    Amount = request.Amount,
                    Currency = "usd",
                    PaymentMethodTypes = new List<string> { "card" },
                };

                var service = new PaymentIntentService();
                PaymentIntent paymentIntent = await service.CreateAsync(options);

                return Ok(new { clientSecret = paymentIntent.ClientSecret });
            }
            catch (StripeException ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }
    }
}
