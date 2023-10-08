using Cadenza.Common.Http.Interfaces;
using Cadenza.Common.Http.Services;
using Cadenza.Web.Common.Enums;

namespace Cadenza.Web.LastFM.Services;

public class LastFmHttpHelper : HttpHelper, ILastFmHttpHelper
{
    public LastFmHttpHelper(IHttpRequestSender sender) 
        : base(sender, HttpClientName.Database.ToString())
    {
    }
}