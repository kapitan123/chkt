namespace BankProxy.API.Models
{
    public record BankResponse(int StatusCode, string Message, Guid Id = default);
}
