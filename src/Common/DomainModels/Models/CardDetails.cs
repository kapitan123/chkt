namespace Common.DomainModels;

public record CardDetails(
    string Number,
    string HolderName,
    DateTime Expiration,
    string Cvv
);