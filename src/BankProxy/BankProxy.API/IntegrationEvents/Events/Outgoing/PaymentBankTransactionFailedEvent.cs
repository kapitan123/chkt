namespace BankProxy.API.IntegrationEvents.Events;

public record PaymentBankTransactionFailedEvent(Guid PaymentId, string Reason) : IntegrationEvent;

