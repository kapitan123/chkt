namespace MerchantPayment.API.IntegrationEvents.Events
{
    public record PaymentValidationFinishedEvent(Guid PaymentTransactionId, bool Result) : IntegrationEvent;
}
