namespace MerchantPayment.API.IntegrationEvents.Events
{
    public class PaymentCreatedEvent
    {
        public Guid Id { get; set; }

        public DateTime TimeStamp { get; set; } 
    }
}
