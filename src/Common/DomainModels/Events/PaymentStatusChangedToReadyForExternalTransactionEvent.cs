using Common.EventBus.Events;

namespace Common.DomainModels.Events;

public record PaymentStatusChangedToReadyForExternalTransactionEvent(
    Guid PaymentId, CardDetails CardDetails, PaymentAmount Amount, string Message) : IntegrationEvent;

