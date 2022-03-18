namespace MerchantPayment.API.IntegrationEvents.Events
{
    public class PaymentValidationFinishedEvent
    {
        public Guid Id { get; set; }

        public DateTime TimeStamp { get; set; }
    }
}
