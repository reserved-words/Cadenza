﻿using Cadenza.API.LastFM.Interfaces;
using Cadenza.API.LastFM.Model;
using Cadenza.Utilities.Interfaces;
using Microsoft.Extensions.Options;

namespace Cadenza.API.LastFM.Services;

public class LastFmAuthorisedClient : ILastFmAuthorisedClient
{
    private readonly IHttpHelper _httpClient;
    private readonly IOptions<LastFmSettings> _config;
    private readonly ILastFmSigner _signer;

    public LastFmAuthorisedClient(IHttpHelper httpClient, IOptions<LastFmSettings> config, ILastFmSigner signer)
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
