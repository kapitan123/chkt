namespace MerchantPayment.API.Models.DTO
{
    // AK TODO we should separate but I'm too lasy
    // should be moved from DTO
    // SHould perform validation on payment amout > 0
    // SHould have 3 digits currency code
    public record PaymentAmount(decimal Amount, string CurrencyCode);
}
