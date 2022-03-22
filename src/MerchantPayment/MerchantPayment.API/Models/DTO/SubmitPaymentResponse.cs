namespace MerchantPayment.API.Models.DTO
{
    public record SubmitPaymentResponse(Guid PaymentId)
    {
        public Guid PaymentId { get; set; }
    }
}
