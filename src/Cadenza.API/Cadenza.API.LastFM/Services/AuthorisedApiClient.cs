namespace Cadenza.API.LastFM.Services;

internal class AuthorisedApiClient : IAuthorisedApiClient
{
    private readonly IHttpHelper _httpClient;
    private readonly IOptions<LastFmApiSettings> _config;
    private readonly ISigner _signer;

    public AuthorisedApiClient(IHttpHelper httpClient, IOptions<LastFmApiSettings> config, ISigner signer)
    {
        _httpClient = httpClient;
        _config = config;
        _signer = signer;
    }

    public async Task Post(string sessionKey, Dictionary<string, string> parameters)
    {
        parameters.Add("api_key", _config.Value.ApiKey);
        parameters.Add("sk", sessionKey);

        _signer.Sign(parameters);

        await _httpClient.Post(_config.Value.ApiBaseUrl, null, parameters);
    }
}
