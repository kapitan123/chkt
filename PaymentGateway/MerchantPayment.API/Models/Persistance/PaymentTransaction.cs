using MerchantPayment.API.Models.DTO;

namespace MerchantPayment.API.Models.Persistance
{
    // This should be a class
    public record PaymentTransaction(Guid Id, 
        PaymentAmount PaymentAmount, 
        CardDetails CardDetails, // AK TODO we store only masked card number 
        string MerchantName, // AK TODO can separate to update merchant message
        PaymentStatus Status,
        DateTime CreatedOn
        );
}
