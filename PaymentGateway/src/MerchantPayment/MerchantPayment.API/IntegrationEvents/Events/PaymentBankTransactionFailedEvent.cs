namespace MerchantPayment.API.IntegrationEvents.Events
{
    public record PaymentBankTransactionFailedEvent(Guid OrderId, string Reason) : IntegrationEvent;
}
