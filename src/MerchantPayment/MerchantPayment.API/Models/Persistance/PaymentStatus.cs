namespace MerchantPayment.API.Models.Persistance;

public enum PaymentStatus
{
    Created,
    ReadyForExternalTransaction,
    Failed,
    Succeed
}
