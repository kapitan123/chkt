using Common.EventBus.Events;

namespace Common.DomainModels.Events;

public record PaymentBankTransactionFailedEvent(Guid PaymentId, string Reason) : IntegrationEvent;

