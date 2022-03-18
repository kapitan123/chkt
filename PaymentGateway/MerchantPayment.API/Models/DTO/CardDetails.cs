namespace MerchantPayment.API.Models.DTO
{
    public record CardDetails(
        string CardNumber,
        string CardHolderName,
        DateTime CardExpiration,
        string CardSecurityCode
    );
}
