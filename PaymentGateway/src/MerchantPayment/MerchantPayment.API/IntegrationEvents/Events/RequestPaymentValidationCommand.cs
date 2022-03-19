namespace MerchantPayment.API.IntegrationEvents.Events
{
    public class RequestPaymentValidationCommand
    {
        public Guid Id { get; set; }

        public DateTime TimeStamp { get; set; }
    }
}
