namespace Common.DomainModels;

public record CardDetails(
    string Number,
    string HolderName,
    DateTime Expiration,
    string Cvv
)
{
    public string MaskedNumber => Number[..3] + new string('*', 6)+ Number[11..];
}