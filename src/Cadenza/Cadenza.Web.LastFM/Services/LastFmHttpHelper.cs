namespace Cadenza.Web.LastFM.Services;

public class LastFmHttpHelper : HttpHelper, ILastFmHttpHelper
{
    public LastFmHttpHelper(IHttpRequestSender sender) 
        : base(HttpClientName.Database, sender)
    {
    }
}