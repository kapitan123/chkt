using MerchantPayment.API.Data;
using MerchantPayment.API.IntegrationEvents.Events;
using MerchantPayment.API.Models.Persistance;

namespace MerchantPayment.API.IntegrationEvents.EventHandlers
{
    public class RequestPaymentValidationHandler
    {
        private readonly IPaymentsRepo _payments;
        public RequestPaymentValidationHandler(IPaymentsRepo payments)
        {
            _payments = payments;
        }

        public async Task Handle(PaymentCreatedEvent @event)
        {
            await _payments.UpdateState(@event.Id, PaymentStatus.Processing);
            
            // Validate If card is blocked in the paymentGateway fraud detection
            var isValid = true;
            await _payments.UpdateValidationState(@event.Id, isValid);

            //
        }
    }
}
