namespace MerchantPayment.API.IntegrationEvents.EventHandlers;

public class PaymentBankTransactionSucceededHandler : IIntegrationEventHandler<PaymentBankTransactionSucceededEvent>
{
    private readonly IPaymentsRepository _payments;
    public PaymentBankTransactionSucceededHandler(IPaymentsRepository payments)
    {
        _payments = payments;
    }

    public async Task Handle(PaymentBankTransactionSucceededEvent @event)
    {
        await _payments.FinalizeSuccess(@event.PaymentId, @event.BankReference);
    }
}

