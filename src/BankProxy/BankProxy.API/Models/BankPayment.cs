namespace BankProxy.API.Models;

public class BankPayment
{
    public CardDetails CardDetails { get; set; }

    public PaymentAmount PaymentAmount { get; set; }

    public string Message { get; set; }
}
