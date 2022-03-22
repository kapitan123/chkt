namespace MerchantPayment.API.Data
{
    // AK TODO this is a mock of credentials storage. In practice  this will be a separate sevice
    public class MerchantKeysRepositoryMock : IMerchantKeysRepository
    {
        private readonly Dictionary<Guid, MerchantKey> _credentialKeys = new ();

        public MerchantKeysRepositoryMock()
        {
            // seed data
            var normalMerhcant = new MerchantKey(Guid.Parse("34f25424-088c-482a-a75e-8ccbbecf8112"),
                "ActiveEshop",
                DateTime.Parse("18.03.2026"), true);

            var blockedMerhcant = new MerchantKey(Guid.Parse("8f71c4ee-cb94-4877-a3f7-7382d41d9918"),
                "BlockedEshop",
                DateTime.Parse("18.03.2026"), false);

            _credentialKeys.Add(normalMerhcant.Id, normalMerhcant);
            _credentialKeys.Add(blockedMerhcant.Id, blockedMerhcant);
        }

        public async Task<MerchantKey> GetByIdAsync(Guid id)
        {
            // Simulating an external call
            await Task.Delay(10);
            
            return _credentialKeys[id];
        }
    }
}
