namespace BankProxy.API.Data;

public interface IProcessedMessagesRepository
{
    Task SavePendingMessage(PaymentStatusChangedToReadyForExternalTransactionEvent payload);

    Task<bool> IsMessageDuplicate(Guid messageId);

    Task FinishMessageProcessing(Guid messageId);
}

