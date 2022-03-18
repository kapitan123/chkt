using MerchantPayment.API.Models.DTO;
using Microsoft.AspNetCore.Mvc;

namespace MerchantPayment.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PaymentController : ControllerBase
    {
        private readonly ILogger<PaymentController> _logger;

        public PaymentController(ILogger<PaymentController> logger)
        {
            _logger = logger;
        }

        [HttpPost("submit")]
        public async Task<SubmitPaymentResponse> Submit(SubmitPaymentRequest submitReq)
        {
            // AK TODO submit  to state
            // publish a message
            // return ID
        }
    }
}