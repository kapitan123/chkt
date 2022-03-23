namespace MerchantPayment.API.Data;

public interface IPaymentsRepository
{
    Task<Guid> CreatePaymentAsync(PaymentAmount amount, CardDetails cardDetails, string message);
    Task<PaymentTransaction> GetByIdAsync(Guid paymentId);
    Task UpdateStatusAsync(Guid paymentId, PaymentStatus newStatus);
    Task FinalizeSuccess(Guid paymentId, string bankReference);
    Task FinalizeFailure(Guid paymentId, string statusReason);
}
