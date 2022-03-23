using MerchantPayment.API.IntegrationEvents.EventHandlers;
using Microsoft.AspNetCore.Mvc;

namespace MerchantPayment.API.Controllers;

[Route("api/v1/[controller]")]
[ApiController]
public class IntegrationEventController : ControllerBase
{
    private const string DAPR_PUBSUB_NAME = "pubsub";

    [HttpPost("PaymentBankTransactionSucceeded")]
    [Topic(DAPR_PUBSUB_NAME, nameof(PaymentBankTransactionSucceededEvent))]
    public Task HandleAsync(
        PaymentBankTransactionSucceededEvent @event,
        [FromServices] PaymentBankTransactionSucceededHandler handler)
            => handler.Handle(@event);

    [HttpPost("PaymentBankTransactionFailed")]
    [Topic(DAPR_PUBSUB_NAME, nameof(PaymentBankTransactionFailedEvent))]
    public Task HandleAsync(
        PaymentBankTransactionFailedEvent @event,
        [FromServices] PaymentBankTransactionFailedHandler handler)
            => handler.Handle(@event);
}
