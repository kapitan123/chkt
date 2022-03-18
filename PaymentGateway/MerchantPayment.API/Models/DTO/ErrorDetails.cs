namespace MerchantPayment.API.Models.DTO
{
    public class ErrorDetails
    {
        public string Message { get; set; }

        public ErrorDetails(string message)
        {
            Message = message;
        }
    }
}
