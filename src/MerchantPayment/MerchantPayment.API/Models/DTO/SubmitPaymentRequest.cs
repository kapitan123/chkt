namespace MerchantPayment.API.Models.DTO;

public class SubmitPaymentRequest
{
    public CardDetails CardDetails { get; set; }

    public PaymentAmount Sum { get; set; }

    public string Message { get; set; }
}