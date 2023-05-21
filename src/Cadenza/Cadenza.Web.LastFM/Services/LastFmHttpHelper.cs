using Cadenza.Common.Utilities.Services;

namespace Cadenza.Web.LastFM.Services;

public class LastFmHttpHelper : HttpHelper, ILastFmHttpHelper
{
    public LastFmHttpHelper(IHttpClientFactory httpClientFactory, IJsonConverter jsonConverter) : base(httpClientFactory, jsonConverter)
    {
    }

    protected override string ClientName => "MainAPI";
}