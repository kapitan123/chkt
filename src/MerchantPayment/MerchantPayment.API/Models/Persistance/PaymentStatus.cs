namespace MerchantPayment.API.Models.Persistance;

public enum PaymentStatus
{
    Created,
    SentToProvider,
    Failed,
    Succeed
}
