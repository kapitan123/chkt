namespace BankProxy.API.Models
{
    public class CheckoutRequest
    {
        // AK TODO what to do with nullable shit? Do I really need it as a record type?
        public CardDetails CardDetails { get; set; }

        public PaymentAmount PaymentAmount { get; set; }

        public string Message { get; set; }
    }
}
