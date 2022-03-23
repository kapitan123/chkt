using MerchantPayment.API.Infrastructure.Middleware;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace MerchantPayment.API.Controllers;

[ApiController]
[Route("[controller]")]
[ApiVersion("1.0")]
[Authorize(AuthenticationSchemes = MerchantKeyAuthenticationOptions.DefaultScheme)]
public class PaymentsController : ControllerBase
{
    private readonly ILogger<PaymentsController> _logger;
    private readonly IValidationService _validationService;
    private readonly IPaymentsRepository _paymentsRepo;
    private readonly IEventBus _eventBus;
    public PaymentsController(ILogger<PaymentsController> logger, 
        IValidationService validationService, 
        IPaymentsRepository paymentsRepo,
        IEventBus eventBus
        )
    {
        _logger = logger;
        _validationService = validationService;
        _paymentsRepo = paymentsRepo;
        _eventBus = eventBus;
    }

    [HttpPost("submit")]
    [ProducesResponseType(typeof(SubmitPaymentResponse), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(ErrorDetails), (int)HttpStatusCode.BadRequest)]
    public async Task<ActionResult<SubmitPaymentResponse>> SubmitAsync(SubmitPaymentRequest submitReq)
    {
        var cardValidationResult = _validationService.Validate(submitReq.CardDetails);

        if (!cardValidationResult.IsValid)
        {
            return BadRequest(new ErrorDetails(cardValidationResult.Errors));
        }

        // AK TODO mask card number
        var id = await _paymentsRepo.CreatePaymentAsync(submitReq.Sum, submitReq.CardDetails, submitReq.Message);
        
        // AK TODO this event should have all the info about the payment
        // amount and card details
        await _eventBus.PublishAsync(new PaymentCreatedEvent(id));

        return Ok(new SubmitPaymentResponse(id));
    }

    [HttpGet("{id}/details")]
    [ProducesResponseType(typeof(PaymentTransaction), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(ErrorDetails), (int)HttpStatusCode.NotFound)]
    [ProducesResponseType(typeof(ErrorDetails), (int)HttpStatusCode.BadRequest)]
    public async Task<ActionResult<PaymentTransaction>> GetPaymentDetails(Guid id)
    {
        if(id == Guid.Empty)
        {
            return BadRequest(new ErrorDetails("Id is empty"));
        }

        var paymentsTransaction = await _paymentsRepo.GetByIdAsync(id);

        if(paymentsTransaction == null)
        {
            return NotFound("Payment with such id is not present.");
        }

        return Ok(paymentsTransaction);
    }
}
