namespace BankProxy.API.Models
{
    // AK TODO we can separate DTO and internal request
    public class CheckoutRequest
    {
        // AK TODO what to do with nullable shit? Do I really need it as a record type?
        public CardDetails CardDetails { get; set; } = default;

        public PaymentAmount PaymentAmount { get; set; }

        public string Message { get; set; }
    }
}
