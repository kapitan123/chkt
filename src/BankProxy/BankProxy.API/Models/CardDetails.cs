namespace BankProxy.API.Models
{
    public record CardDetails(
        CardNumber CardNumber,
        string CardHolderName,
        DateTime CardExpiration,
        string CardSecurityCode
    );
}
