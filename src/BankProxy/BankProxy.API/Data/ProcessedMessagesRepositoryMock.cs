namespace BankProxy.API.Data;

public class ProcessedMessagesRepositoryMock : IProcessedMessagesRepository
{
    private readonly List<ProcessedMessage> _transactionsList;

    public ProcessedMessagesRepositoryMock()
    {
        _transactionsList = new List<ProcessedMessage>();
    }

    public Task SavePendingMessage(PaymentStatusChangedToReadyForExternalTransactionEvent payload)
    {
        var b = new ProcessedMessage()
        {
            Id = payload.Id,
            Payload = payload
        };

        _transactionsList.Add(b);

        return Task.CompletedTask;
    }

    public Task<bool> IsMessageDuplicate(Guid messageId)
    {
        var exists = _transactionsList.Any(t => t.Id == messageId);

        return Task.FromResult(exists);
    }

    public Task FinishMessageProcessing(Guid messageId)
    {
        var pedningPayment = _transactionsList.First(t => t.Id == messageId);
        pedningPayment.IsFinished = true;
        return Task.CompletedTask;
    }
}

