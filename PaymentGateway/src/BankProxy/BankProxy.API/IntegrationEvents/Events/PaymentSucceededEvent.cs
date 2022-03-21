using Common.EventBus.Events;

namespace BankProxy.API.IntegrationEvents.Events
{
    public record PaymentSucceededEvent(Guid OrderId) : IntegrationEvent;
}
