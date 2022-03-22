namespace MerchantPayment.API.Models.DTO;

public record CardDetails(
    string Number, // Should be a value object
    string HolderName, // Should be a value object
    DateOnly Expiration, 
    string Cvv // Should be a value object
);
