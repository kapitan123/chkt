namespace MerchantPayment.API.IntegrationEvents.Events;

public record PaymentCreatedEvent(Guid PaymentId) : IntegrationEvent;

