using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace MerchantPayment.API.Controllers;

[ApiController]
[Route("[controller]")]
[ApiVersion("1.0")]
[Authorize]
public class PaymentsController : ControllerBase
{
    private readonly ILogger<PaymentsController> _logger;
    private readonly IValidationService _validationService;
    private readonly IPaymentsRepository _paymentsRepo;
    public PaymentsController(ILogger<PaymentsController> logger, IValidationService validationService, IPaymentsRepository paymentsRepo)
    {
        _logger = logger;
        _validationService = validationService;
        _paymentsRepo = paymentsRepo;
    }

    [HttpPost("submit")]
    [ProducesResponseType(typeof(SubmitPaymentResponse), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(ErrorDetails), (int)HttpStatusCode.BadRequest)]
    public async Task<ActionResult<SubmitPaymentResponse>> SubmitAsync(SubmitPaymentRequest submitReq)
    {
        // AK TODO perform basic Validation
        var cardValidationResult = _validationService.Validate(submitReq.CardDetails);

        if (!cardValidationResult.IsValid)
        {
            return BadRequest(new ErrorDetails(cardValidationResult.Errors.ToArray()));
        }

        // AK TODO mask card number
        // AK TODO publish creation event
        var id = await _paymentsRepo.CreatePaymentAsync(submitReq.Sum, submitReq.CardDetails, submitReq.Message);

        return Ok(new SubmitPaymentResponse(id));
    }

    [HttpGet("{id}/details")]
    [ProducesResponseType(typeof(PaymentTransaction), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(ErrorDetails), (int)HttpStatusCode.NotFound)]
    [ProducesResponseType(typeof(ErrorDetails), (int)HttpStatusCode.BadRequest)]
    public async Task<ActionResult<PaymentTransaction>> GetPaymentDetails(Guid id)
    {
        // AK TODO add error handling
        var paymentsTransaction = await _paymentsRepo.GetByIdAsync(id);

        if(paymentsTransaction == null)
        {
            return NotFound();
        }

        return Ok(paymentsTransaction);
    }
}
