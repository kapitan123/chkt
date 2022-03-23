namespace MerchantPayment.API.Models.Persistance;

public class PaymentTransaction
{
    public Guid Id { get; set; }
    public PaymentAmount PaymentAmount { get; set; }
    public CardDetails CardDetails { get; set; }
    public PaymentStatus Status { get; set; }
    public string StatusReason { get; set; } = "";
    public DateTime CreatedOn { get; set; }
    public string Message { get; set; } = "";
    public string BankReference { get; set; } = "";
}
