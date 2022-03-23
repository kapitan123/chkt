using Common.EventBus.Events;

namespace Common.DomainModels.Events;

public record PaymentBankTransactionSucceededEvent(Guid PaymentId, string BankReference) : IntegrationEvent;

