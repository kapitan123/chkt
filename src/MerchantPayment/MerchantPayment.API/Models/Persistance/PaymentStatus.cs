namespace MerchantPayment.API.Models.Persistance;

public enum PaymentStatus
{
    Created,
    Processing,
    SentToProvider,
    Failed,
    Succeed
}
