namespace BankProxy.API.IntegrationEvents.Events;

public record PaymentBankTransactionSucceededEvent(Guid PaymentId, string BankReference) : IntegrationEvent;

