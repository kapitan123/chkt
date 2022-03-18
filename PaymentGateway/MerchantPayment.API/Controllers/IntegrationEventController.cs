using MerchantPayment.API.Models.DTO;
using Microsoft.AspNetCore.Mvc;

namespace MerchantPayment.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class IntegrationEventController
    {
        private readonly ILogger<PaymentController> _logger;

        public IntegrationEventController(ILogger<PaymentController> logger)
        {
            _logger = logger;
        }

        [HttpPost("submit")]
        // AK TODO  Handle
        public async Task<SubmitPaymentResponse> Handle(SubmitPaymentRequest submitReq)
        {
            // Invoke a handler
            // IsValidationFinished
            // publish a message
            // return ID
        }
    }
}
