namespace MerchantPayment.API.Models.Persistance;

public record MerchantKey(
    Guid Id,
    string Owner,
    DateTime ExpiratioDate,
    bool IsActive
);
