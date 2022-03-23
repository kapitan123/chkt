namespace MerchantPayment.API.IntegrationEvents.Events;

// AK TODO extensive event
public record PaymentStatusChangedToReadyForExternalTransaction(Guid PaymentId) : IntegrationEvent;

