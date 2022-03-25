namespace MerchantPayment.API.IntegrationEvents.EventHandlers;

public class PaymentBankTransactionFailedHandler : IIntegrationEventHandler<PaymentBankTransactionFailedEvent>
{
    private readonly IPaymentsRepository _payments;
    public PaymentBankTransactionFailedHandler(IPaymentsRepository payments)
    {
        _payments = payments;
    }

    public async Task Handle(PaymentBankTransactionFailedEvent @event)
    {
        await _payments.FinalizeFailure(@event.PaymentId, @event.Reason);
    }
}

