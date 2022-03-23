namespace MerchantPayment.API.Data;

public class PaymentsRepository : IPaymentsRepository
{
    private const string StoreName = "eshop-statestore";

    private readonly DaprClient _daprClient;
    private readonly ILogger _logger;

    public PaymentsRepository(DaprClient daprClient, ILogger<PaymentsRepository> logger)
    {
        _daprClient = daprClient;
        _logger = logger;
    }

    public Task<Guid> CreatePaymentAsync(SubmitPaymentRequest req)
    {
        throw new NotImplementedException();
    }

    public Task DeleteBasketAsync(string id) =>
        _daprClient.DeleteStateAsync(StoreName, id);

    public Task<CustomerBasket> GetBasketAsync(string customerId) =>
        _daprClient.GetStateAsync<CustomerBasket>(StoreName, customerId);

    public Task<PaymentTransaction> GetByIdAsync(Guid paymentId)
    {
        throw new NotImplementedException();
    }

    public async Task<CustomerBasket> UpdateBasketAsync(CustomerBasket basket)
    {
        var state = await _daprClient.GetStateEntryAsync<CustomerBasket>(StoreName, basket.BuyerId);
        state.Value = basket;

        await state.SaveAsync();

        _logger.LogInformation("Basket item persisted successfully.");

        return await GetBasketAsync(basket.BuyerId);
    }

    public Task UpdateStateAsync(Guid paymentId, PaymentStatus newStatus)
    {
        throw new NotImplementedException();
    }

    public Task UpdateValidationStateAsync(Guid paymentId, bool newValidationState)
    {
        throw new NotImplementedException();
    }
}
