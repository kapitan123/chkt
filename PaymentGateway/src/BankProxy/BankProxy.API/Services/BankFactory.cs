namespace BankProxy.API.Services
{
    public class BankFactory : IBankFactory
    {
        private readonly IEnumerable<IBank> _registeredProviders;

        // AK TODO How to register an array of implementations
        public BankFactory(IEnumerable<IBank> registeredProviders)
        {
            _registeredProviders = registeredProviders;
        }

        public IBank GetBankByCardNumber(CardNumber cardNumber)
        {
            var provider = _registeredProviders.FirstOrDefault(p => p.IsIssuerOf(cardNumber));

            if (provider == null)
            {
                throw new ArgumentException($"Unknow issuer of the card bank numbers: {cardNumber.BankDigits}");
            }

            return provider;
        }
    }
}
