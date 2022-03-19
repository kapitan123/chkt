namespace MerchantPayment.API.IntegrationEvents.Events
{
    public class SendToBankCommand
    {
        public Guid Id { get; set; }

        public DateTime TimeStamp { get; set; }
    }
}
