using Common.EventBus.Events;

namespace BankProxy.API.IntegrationEvents.Events
{
    public record PaymentFailedEvent(Guid OrderId, string Reason) : IntegrationEvent;
}
