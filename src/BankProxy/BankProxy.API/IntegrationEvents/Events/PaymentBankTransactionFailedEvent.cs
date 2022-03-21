using Common.EventBus.Events;

namespace BankProxy.API.IntegrationEvents.Events
{
    public record PaymentBankTransactionFailedEvent(Guid OrderId, string Reason) : IntegrationEvent;
}
