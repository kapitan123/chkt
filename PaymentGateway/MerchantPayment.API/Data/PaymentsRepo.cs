using MerchantPayment.API.Models.Persistance;

namespace MerchantPayment.API.Data
{
    public class PaymentsRepo
    {
        public Task<Guid> CreatePeyment()
        {

        }

        public Task UpdateValidationState(bool newValidationState)
        {

        }

        public Task UpdateProcessState(PaymentStatus newStatus)
        {

        }

        public Task<PaymentTransaction> GetById(Guid paymentId)
        {

        }
    }
}
