using BankProxy.API.IntegrationEvents.EventHandlers;
using Dapr;
using Microsoft.AspNetCore.Mvc;

namespace BankProxy.API.Controllers;

[Route("api/v1/[controller]")]
[ApiController]
public class IntegrationEventController : ControllerBase
{
    private const string DAPR_PUBSUB_NAME = "pubsub";

    [HttpPost("PaymentStatusChangedToReadyForExternalTransaction")]
    [Topic(DAPR_PUBSUB_NAME, nameof(PaymentStatusChangedToReadyForExternalTransactionEvent))]
    public Task HandleAsync(
        PaymentStatusChangedToReadyForExternalTransactionEvent @event,
        [FromServices] PaymentStatusChangedToReadyForExternalTransactionHandler handler)
            => handler.Handle(@event);
}
