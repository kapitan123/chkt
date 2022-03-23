namespace BankProxy.API.Services;

public interface IBankFactory
{
    IBank GetBankByCardNumber(CardNumber cardNumber);
}