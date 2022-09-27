using Microsoft.Extensions.Options;

namespace Cadenza.API.LastFM.Services;

internal class Signer : ISigner
{
    private readonly IHasher _hasher;
    private readonly IOptions<ApiSettings> _config;

    public Signer(IOptions<ApiSettings> config, IHasher hasher)
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