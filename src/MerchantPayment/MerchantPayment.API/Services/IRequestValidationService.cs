namespace MerchantPayment.API.Services;

public interface IRequestValidationService
{
    ValidationResult ValidateSubmitPaymentRequest(SubmitPaymentRequest req);
}