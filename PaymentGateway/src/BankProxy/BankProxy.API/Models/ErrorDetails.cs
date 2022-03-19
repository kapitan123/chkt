namespace BankProxy.API.Models
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
