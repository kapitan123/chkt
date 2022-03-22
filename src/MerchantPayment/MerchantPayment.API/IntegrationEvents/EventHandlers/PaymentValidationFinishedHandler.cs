namespace MerchantPayment.API.IntegrationEvents.EventHandlers;

public class PaymentValidationFinishedHandler : IIntegrationEventHandler<PaymentBankTransactionFailedEvent>
{
    private readonly IPaymentsRepository _payments;
    public PaymentValidationFinishedHandler(IPaymentsRepository payments)
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

