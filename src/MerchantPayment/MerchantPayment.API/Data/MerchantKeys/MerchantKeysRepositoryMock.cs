namespace MerchantPayment.API.Data;

public class MerchantKeysRepositoryMock : IMerchantKeysRepository
{
    private readonly Dictionary<Guid, MerchantKey> _credentialKeys = new ();

    public MerchantKeysRepositoryMock()
    {
        // seed data
        var normalMerhcant = new MerchantKey(Guid.Parse("34f25424-088c-482a-a75e-8ccbbecf8112"),
            "ActiveEshop",
            DateTime.Parse("2026-08-01T00:00:00-07:00"), true);

        var blockedMerhcant = new MerchantKey(Guid.Parse("8f71c4ee-cb94-4877-a3f7-7382d41d9918"),
            "BlockedEshop",
            DateTime.Parse("2026-08-01T00:00:00-07:00"), false);

        _credentialKeys.Add(normalMerhcant.Id, normalMerhcant);
        _credentialKeys.Add(blockedMerhcant.Id, blockedMerhcant);
    }

    public async Task<MerchantKey?> GetByIdAsync(Guid id)
    {
        // Simulating an external call
        await Task.Delay(10);

        if (!_credentialKeys.ContainsKey(id))
        {
            return null;
        }

        return _credentialKeys[id];
    }
}
