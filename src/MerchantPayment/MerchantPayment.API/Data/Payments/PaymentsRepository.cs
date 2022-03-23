namespace MerchantPayment.API.Data;

public class PaymentsRepository : IPaymentsRepository
{
    private const string StoreName = "payment-statestore";

    private readonly DaprClient _daprClient;
    private readonly ILogger _logger;

    public PaymentsRepository(DaprClient daprClient, ILogger<PaymentsRepository> logger)
    {
        _daprClient = daprClient;
        _logger = logger;
    }

    public async Task<Guid> CreatePaymentAsync(PaymentAmount amount, CardDetails cardDetails, string message)
    {
        var payment = new PaymentTransaction()
        {
            Id = Guid.NewGuid(),
            PaymentAmount = amount,
            CardDetails = cardDetails with { Number = cardDetails.MaskedNumber },
            Status = PaymentStatus.Created,
            CreatedOn = DateTime.UtcNow,
            Message = message
        };

        await _daprClient.SaveStateAsync(StoreName, payment.Id.ToString(), payment);
        _logger.LogInformation("Payment with id {Id} was created.", payment.Id);

        return payment.Id;
    }

    public Task<PaymentTransaction> GetByIdAsync(Guid paymentId)
    {
        return _daprClient.GetStateAsync<PaymentTransaction>(StoreName, paymentId.ToString());
    }

    public async Task UpdateStatusAsync(Guid paymentId, PaymentStatus newStatus)
    {
        var state = await _daprClient.GetStateEntryAsync<PaymentTransaction>(StoreName, paymentId.ToString());
        state.Value.Status = newStatus;

        await state.SaveAsync();

        _logger.LogInformation("Status of payment with id {Id} was updated.", paymentId);
    }

    public async Task FinalizeSuccess(Guid paymentId, string bankReference)
    {
        var state = await _daprClient.GetStateEntryAsync<PaymentTransaction>(StoreName, paymentId.ToString());
        state.Value.Status = PaymentStatus.Succeed;
        state.Value.BankReference = bankReference;

        await state.SaveAsync();

        _logger.LogInformation("Payment with id {Id} is finished.", paymentId);
    }

    public async Task FinalizeFailure(Guid paymentId, string statusReason)
    {
        var state = await _daprClient.GetStateEntryAsync<PaymentTransaction>(StoreName, paymentId.ToString());
        state.Value.Status = PaymentStatus.Failed;
        state.Value.BankReference = statusReason;

        await state.SaveAsync();

        _logger.LogInformation("Payment with id {Id} is finished.", paymentId);
    }
}
