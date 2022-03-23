namespace MerchantPayment.API.Models.DTO;

public record CardDetails(
    string Number,
    string HolderName,
    DateTime Expiration,
    string Cvv
);