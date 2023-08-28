using Cadenza.Common.Utilities.Services;

namespace Cadenza.Web.LastFM.Services;

public class LastFmHttpHelper : HttpHelper, ILastFmHttpHelper
{
    public LastFmHttpHelper(IJsonConverter jsonConverter, IHttpRequestSender sender) 
        : base(HttpClientName.Database, jsonConverter, sender)
    {
    }
}