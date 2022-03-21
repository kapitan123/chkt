using MerchantPayment.API.Models.Persistance;
namespace MerchantPayment.API.Data
{
    // AK TODO Can be extracted to an external service
    // We can join it to Auth service with the fraud detection as well
    public class CredentialKeysRepo: ICredentialKeysRepo
    {
        private readonly Dictionary<Guid, MerchantKey> _credentialKeys = new ();

        public CredentialKeysRepo()
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

        public async Task<MerchantKey> GetById(Guid id)
        {
            // Simulating an external call
            await Task.Delay(10);
            
            // AK TODO should go to the destributed cache
            return _credentialKeys[id];
        }
    }
}
