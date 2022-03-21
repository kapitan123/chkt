namespace MerchantPayment.API.IntegrationEvents.Events
{
    public class PaymentStatusChangedToReadyForExternalTransaction
    {
        public Guid Id { get; set; }

        public DateTime TimeStamp { get; set; }
    }
}
