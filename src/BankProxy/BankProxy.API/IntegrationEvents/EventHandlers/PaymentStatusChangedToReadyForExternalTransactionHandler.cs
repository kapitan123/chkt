namespace BankProxy.API.IntegrationEvents.EventHandlers;

public class PaymentStatusChangedToReadyForExternalTransactionHandler : IIntegrationEventHandler<PaymentStatusChangedToReadyForExternalTransactionEvent>
{
    private readonly IBankFactory _bankProvidersFactory;
    private readonly IEventBus _eventBus;
    private readonly ILogger<PaymentStatusChangedToReadyForExternalTransactionHandler> _logger;

    public PaymentStatusChangedToReadyForExternalTransactionHandler(
            ILogger<PaymentStatusChangedToReadyForExternalTransactionHandler> logger,
            IBankFactory bankProvidersFactory,
            IEventBus eventBus)
    {
        _bankProvidersFactory = bankProvidersFactory;
        _eventBus = eventBus;
        _logger = logger;
    }

    public async Task Handle(PaymentStatusChangedToReadyForExternalTransactionEvent @event)
    {
        // We should store the message and send an akk
        // We should retry failed transactions 
        // We should be protected from double charge
        // We should implement idempotency
        // We should have it in the state storage
        // We should retry on saved transactions if they were not successfullly finished
        // We store transaction id for idempotency reasons as well, because of at least once delivery
        try
        {
            var bank = _bankProvidersFactory.GetBankByCardNumber(checkoutReq.CardDetails.CardNumber);

            var bankProcessingResult = await bank.ProcessTransaction(checkoutReq);

            var response = new CheckoutResponse
            {
                Message = bankProcessingResult.Message,
                IsSuccess = bankProcessingResult.StatusCode == 0,
                Code = bankProcessingResult.StatusCode
            };

            return Ok(response);
        }
        catch (ArgumentException ex)
        {
            _logger.LogError(ex.Message);
            return BadRequest(new ErrorDetails(ex.Message));
        }
    }
}

