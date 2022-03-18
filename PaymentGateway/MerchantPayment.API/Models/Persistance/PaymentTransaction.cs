using MerchantPayment.API.Models.DTO;

namespace MerchantPayment.API.Models.Persistance
{
    // This should be a class
    public record PaymentTransaction(Guid Id, 
        PaymentAmount PaymentAmount, 
        CardDetails CardDetails, // AK TODO we store only masked card number and an encrypted card
        bool ValidationStatus,
        PaymentStatus Status,
        string StatusReason, // AK TODO error messages
        DateTime CreatedOn,
        string Message,
        string BankReference // AK TODO set after we finished out transaction
        );
}
