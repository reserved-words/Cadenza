using Cadenza.Common;
using Microsoft.Extensions.Options;

namespace Cadenza.LastFM;

public class LastFmSigner : ILastFmSigner
{
    private readonly IHasher _hasher;
    private readonly IOptions<LastFmSettings> _config;

    public LastFmSigner(IOptions<LastFmSettings> config, IHasher hasher)
    {
        _config = config;
        _hasher = hasher;
    }

    public void Sign(Dictionary<string, string> parameters)
    {
        var signature = string.Join("", parameters.Keys
            .OrderBy(p => p)
            .Select(p => $"{p}{parameters[p]}"));

        signature += _config.Value.ApiSecret;

        var hashedSignature = _hasher.MD5Hash(signature);

        parameters.Add("api_sig", hashedSignature);
    }
}