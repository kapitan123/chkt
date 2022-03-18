namespace MerchantPayment.API.IntegrationEvents.EventHandlers
{
    public class PaymentValidationFinishedHandler
    {
        private readonly IBasketRepository _repository;
            

        public OrderStatusChangedToSubmittedIntegrationEventHandler(
            PaymentsRepo repository)
        {
            _repository = repository;
        }

        public Task Handle(OrderStatusChangedToSubmittedIntegrationEvent @event)
        {
            PaymentValidationFinishedHandler
        }

 
    }
