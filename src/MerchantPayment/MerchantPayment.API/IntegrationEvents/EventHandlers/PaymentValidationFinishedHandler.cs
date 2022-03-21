using MerchantPayment.API.Data;
using MerchantPayment.API.IntegrationEvents.Events;

namespace MerchantPayment.API.IntegrationEvents.EventHandlers
{
    public class PaymentValidationFinishedHandler : IIntegrationEventHandler<PaymentBankTransactionFailedEvent>
    {
        private readonly IPaymentsRepo _payments;
        public PaymentValidationFinishedHandler(IPaymentsRepo payments)
        {
            _payments = payments;
        }

        public Task Handle(PaymentBankTransactionFailedEvent @event)
        {
            // Update Status to sent to provider
            // publish SendToProviderEvent
            throw new NotImplementedException();
        }
    }
}
