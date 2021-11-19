using Cadenza.Common;

namespace Cadenza.LastFM;

public class LastFmSigner : ILastFmSigner
{
    private readonly IHasher _hasher;
    private readonly ILastFmConfig _config;

    public LastFmSigner(ILastFmConfig config, IHasher hasher)
    {
        _config = config;
        _hasher = hasher;
    }

    public void Sign(Dictionary<string, string> parameters)
    {
        var signature = string.Join("", parameters.Keys
            .OrderBy(p => p)
            .Select(p => $"{p}{parameters[p]}"));

        signature += _config.ApiSecret;

        var hashedSignature = _hasher.MD5Hash(signature);

        parameters.Add("api_sig", hashedSignature);
    }
}