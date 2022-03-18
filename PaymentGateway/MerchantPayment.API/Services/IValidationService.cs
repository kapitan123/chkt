using MerchantPayment.API.Models.DTO;

namespace MerchantPayment.API.Services
{
    public interface IValidationService
    {
        ValidationResult Validate(CardDetails details);
    }
}