namespace BankProxy.API.Models.DTO;

public record BankResponse(
    int StatusCode, string Message, bool IsSuccess, string BankReference = "");
