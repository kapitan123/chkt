using MerchantPayment.API.Models.DTO;
using MerchantPayment.API.Services;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace MerchantPayment.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [ApiVersion("1.0")]
    public class PaymentController : ControllerBase
    {
        private readonly ILogger<PaymentController> _logger;
        private readonly IValidationService _validationService;

        public PaymentController(ILogger<PaymentController> logger, IValidationService validationService)
        {
            _logger = logger;
            _validationService = validationService;
        }

        [HttpPost("submit")]
        [ProducesResponseType(typeof(SubmitPaymentResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorDetails), (int)HttpStatusCode.BadRequest)]
        public async Task<SubmitPaymentResponse> Submit(SubmitPaymentRequest submitReq)
        {
            // AK TODO perform basic Validation
            var cardValidationResult = _validationService.Validate(submitReq.CardDetails);

            // Mask CardNumber
            // submit to state
            // publish a PaymentCreatedEvent - publish so we can notify or count
            // publish RequestPaymentValidation - because we need at least one consumer
            // return ID
            throw new NotImplementedException();
        }
    }
}