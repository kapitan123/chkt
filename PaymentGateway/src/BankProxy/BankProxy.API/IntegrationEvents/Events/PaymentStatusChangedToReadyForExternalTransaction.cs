using Common.EventBus.Events;

namespace BankProxy.API.IntegrationEvents.Events
{
    public record PaymentStatusChangedToReadyForExternalTransaction(
        Guid PaymentId,
        decimal Total)
        : IntegrationEvent;
}
