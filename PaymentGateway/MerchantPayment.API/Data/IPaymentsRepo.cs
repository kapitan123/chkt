using MerchantPayment.API.Models.Persistance;

namespace MerchantPayment.API.Data
{
    public interface IPaymentsRepo
    {
        Task<Guid> CreatePayment();
        Task<PaymentTransaction> GetById(Guid paymentId);
        Task UpdateState(Guid paymentId, PaymentStatus newStatus);
        Task UpdateValidationState(Guid paymentId, bool newValidationState);
    }
}