using Common.EventBus.Events;

namespace BankProxy.API.IntegrationEvents.Events
{
    public record PaymentBankTransactionSucceededEvent(Guid OrderId) : IntegrationEvent;
}
