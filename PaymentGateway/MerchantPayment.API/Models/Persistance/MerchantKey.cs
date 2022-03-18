namespace MerchantPayment.API.Models.Persistance
{
    public record MerchantKey(
        Guid Id,
        string MerchantName,
        DateTime ExpiratioDate,
        bool IsActive
    );
}
