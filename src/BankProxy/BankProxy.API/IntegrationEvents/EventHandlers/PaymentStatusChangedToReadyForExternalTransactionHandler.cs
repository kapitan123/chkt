namespace BankProxy.API.IntegrationEvents.EventHandlers;

public class PaymentStatusChangedToReadyForExternalTransactionHandler : IIntegrationEventHandler<PaymentStatusChangedToReadyForExternalTransactionEvent>
{
    private readonly IBankFactory _bankProvidersFactory;
    private readonly IProcessedMessagesRepository _messagesRepo;
    private readonly IEventBus _eventBus;
    private readonly ILogger<PaymentStatusChangedToReadyForExternalTransactionHandler> _logger;

    public PaymentStatusChangedToReadyForExternalTransactionHandler(
            ILogger<PaymentStatusChangedToReadyForExternalTransactionHandler> logger,
            IBankFactory bankProvidersFactory,
            IProcessedMessagesRepository messagesRepo,
            IEventBus eventBus)
    {
        _bankProvidersFactory = bankProvidersFactory;
        _eventBus = eventBus;
        _logger = logger;
        _messagesRepo = messagesRepo;
    }

    public async Task Handle(PaymentStatusChangedToReadyForExternalTransactionEvent @event)
    {
        try
        {
            var isDuplicateMessage = await _messagesRepo.IsMessageDuplicate(@event.Id);
            if (isDuplicateMessage)
            {
                _logger.LogWarning("The message was duplicated");
                return;
            }

            await _messagesRepo.SavePendingMessage(@event);

            var cardNumber = new CardNumber(@event.CardDetails.Number);

            var issuerBank = _bankProvidersFactory.GetBankByCardNumber(cardNumber);

            var bankPayment = new BankPayment
            {
                CardDetails = @event.CardDetails,
                PaymentAmount = @event.Amount,
                Message = @event.Message
            };

            var bankProcessingResult = await issuerBank.ProcessTransaction(bankPayment);

            await _messagesRepo.FinishMessageProcessing(@event.Id);

            if (bankProcessingResult.IsSuccess)
            {
                await _eventBus.PublishAsync(
                    new PaymentBankTransactionSucceededEvent(@event.PaymentId, bankProcessingResult.BankReference));
            } 
            else
            {
                await _eventBus.PublishAsync(
                    new PaymentBankTransactionFailedEvent(@event.PaymentId, bankProcessingResult.Message));
            }         
        }
        catch (Exception ex)
        {
            _logger.LogError("Bank transaction has failed {Message}", ex.Message);
            await _messagesRepo.FinishMessageProcessing(@event.Id);
            await _eventBus.PublishAsync(new PaymentBankTransactionFailedEvent(@event.PaymentId, ex.Message));
        }
    }
}

