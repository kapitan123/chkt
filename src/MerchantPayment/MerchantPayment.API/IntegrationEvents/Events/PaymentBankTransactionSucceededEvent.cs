namespace MerchantPayment.API.IntegrationEvents.Events
{
    public record PaymentBankTransactionSucceededEvent(Guid OrderId) : IntegrationEvent;
}
