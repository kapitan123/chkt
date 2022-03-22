using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace MerchantPayment.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [ApiVersion("1.0")]
    public class PaymentsController : ControllerBase
    {
        private readonly ILogger<PaymentsController> _logger;
        private readonly IValidationService _validationService;
        private readonly IPaymentsRepo _paymentsRepo;
        public PaymentsController(ILogger<PaymentsController> logger, IValidationService validationService, IPaymentsRepo paymentsRepo)
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

            if (!cardValidationResult.IsValid)
            {
                return BadRequest(new ErrorDetails(cardValidationResult.Errors.ToArray()));
            }
            //- CreatedAtAction(nameof(Get), new { id = Guid.NewGuid() }, payment.ToContract()) : BadRequest(errorMessage);
            // Mask CardNumber
            // submit to state
            // publish a PaymentCreatedEvent - publish so we can notify or count
            // publish RequestPaymentValidation - because we need at least one consumer
            // return ID
            throw new NotImplementedException();
        }

        // AK TODO should separate DTO and persistance
        [HttpGet("{id}/details")]
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