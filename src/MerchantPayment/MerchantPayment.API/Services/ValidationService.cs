using System.Text.RegularExpressions;

namespace MerchantPayment.API.Services;

public class ValidationService : IValidationService
{
    private readonly Regex _cardExp = new(@"^(1298|1267|4512|4567|8901|8933)([\-\s]?[0-9]{4}){3}$");
    private readonly Regex _cvvExp = new(@"^\d{3}$");

    public ValidationResult Validate(CardDetails details)
    {
        var errors = new List<string>();
        if (!IsNumberValid(details.Number))
        {
            errors.Add("Card number is invalid");
        }

        if (!IsCvvSet(details.Cvv))
        {
            errors.Add("Card cvv is invalid"); // AK TODO questionabl descision to expose this exception
        }

        if (!IsExpirationValid(details.Expiration))
        {
            errors.Add("Card expiration is invalid");
        }

        return new ValidationResult()
        {
            IsValid = !errors.Any(),
            Errors = errors
        };
    }

    private bool IsNumberValid(string cardNumber) => _cardExp.IsMatch(cardNumber);


    private bool IsCvvSet(string cvv) => _cvvExp.IsMatch(cvv);

    private static bool IsExpirationValid(DateOnly expirationDate) => expirationDate > DateOnly.FromDateTime(DateTime.Now);
}

