using Microsoft.AspNetCore.Mvc;

namespace MerchantPayment.API.Controllers;

[ApiController]
[Route("[controller]")]
public class IntegrationEventController
{
    private readonly ILogger<PaymentsController> _logger;

    public IntegrationEventController(ILogger<PaymentsController> logger)
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

        throw new NotImplementedException();
    }
}
