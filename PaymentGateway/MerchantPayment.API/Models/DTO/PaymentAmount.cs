namespace MerchantPayment.API.Models.DTO
{
    // AK TODO we should separate but I'm too lasy
    public record PaymentAmount(decimal Amount, string Currency);
}
