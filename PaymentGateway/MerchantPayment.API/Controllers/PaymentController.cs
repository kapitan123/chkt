using MerchantPayment.API.Data;
using MerchantPayment.API.Models.DTO;
using MerchantPayment.API.Models.Persistance;
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
        private readonly IPaymentsRepo _paymentsRepo;
        public PaymentController(ILogger<PaymentController> logger, IValidationService validationService, IPaymentsRepo paymentsRepo)
        {
            _logger = logger;
            _validationService = validationService;
            _paymentsRepo = paymentsRepo;
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

        // AK TODO should separate DTO and persistance
        [HttpPost("{id}/details")]
        [ProducesResponseType(typeof(PaymentTransaction), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorDetails), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ErrorDetails), (int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<PaymentTransaction>> GetTransactionDetails(Guid id)
        {
            // AK TODO add error handling
            var paymentsTransaction = await _paymentsRepo.GetById(id);
            return Ok(paymentsTransaction);
        }
    }
}