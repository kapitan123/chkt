namespace BankProxy.API.IntegrationEvents.Events;

public record PaymentStatusChangedToReadyForExternalTransactionEvent(Guid PaymentId) : IntegrationEvent;

