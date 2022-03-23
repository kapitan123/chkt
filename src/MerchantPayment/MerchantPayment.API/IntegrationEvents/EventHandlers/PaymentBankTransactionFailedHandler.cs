namespace MerchantPayment.API.IntegrationEvents.EventHandlers;

public class PaymentValidationFinishedHandler : IIntegrationEventHandler<PaymentBankTransactionFailedEvent>
{
    private readonly IPaymentsRepository _payments;
    public PaymentValidationFinishedHandler(IPaymentsRepository payments)
    {
        _payments = payments;
    }

    public async Task Handle(PaymentBankTransactionFailedEvent @event)
    {
        await _payments.FinalizeFailure(@event.Id, @event.Reason);
    }
}

