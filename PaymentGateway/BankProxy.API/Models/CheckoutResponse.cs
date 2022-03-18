namespace BankProxy.API.Models
{
    public class CheckoutResponse
    {
        public int Code { get; set; }

        public string Message { get; set; } = "";

        public bool IsSuccess { get; set; }
    }
}
