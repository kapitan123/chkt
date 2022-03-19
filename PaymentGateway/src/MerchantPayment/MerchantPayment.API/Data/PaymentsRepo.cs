using MerchantPayment.API.Models.Persistance;

namespace MerchantPayment.API.Data
{
    public class PaymentsRepo : IPaymentsRepo
    {
        public Task<Guid> CreatePayment()
        {
            throw new NotImplementedException();
        }

        public Task UpdateValidationState(Guid paymentId, bool newValidationState)
        {
            throw new NotImplementedException();
        }

        public Task UpdateState(Guid paymentId, PaymentStatus newStatus)
        {
            throw new NotImplementedException();
        }

        public Task<PaymentTransaction> GetById(Guid paymentId)
        {
            throw new NotImplementedException();
        }
    }
}
