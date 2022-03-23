namespace BankProxy.API.Models.Persistance;

public class ProcessedMessage
{
    public Guid Id { get; set; }

    public PaymentStatusChangedToReadyForExternalTransactionEvent Payload { get; set; }

    public bool IsFinished { get; set; }
}
