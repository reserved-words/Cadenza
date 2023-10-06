using Cadenza.Common.Utilities.Http;

namespace Cadenza.Web.LastFM.Services;

public class LastFmHttpHelper : HttpHelper, ILastFmHttpHelper
{
    public LastFmHttpHelper(IJsonService jsonConverter, IHttpRequestSender sender) 
        : base(HttpClientName.Database, jsonConverter, sender)
    {
    }
}