namespace MerchantPayment.API.Data;

public interface IPaymentsRepository
{
    Task<Guid> CreatePaymentAsync(SubmitPaymentRequest req);
    Task<PaymentTransaction> GetByIdAsync(Guid paymentId);
    Task UpdateStateAsync(Guid paymentId, PaymentStatus newStatus);
    Task UpdateValidationStateAsync(Guid paymentId, bool newValidationState);
}
