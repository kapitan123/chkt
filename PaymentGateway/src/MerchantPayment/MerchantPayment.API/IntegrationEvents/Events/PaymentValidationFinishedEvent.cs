namespace MerchantPayment.API.IntegrationEvents.Events
{
    public record PaymentValidationFinishedEvent(Guid Id, DateTime TimeStamp);
}
