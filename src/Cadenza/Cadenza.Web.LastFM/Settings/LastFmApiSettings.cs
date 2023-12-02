namespace Cadenza.Web.LastFM.Settings;

public class LastFmApiSettings : ApiOptions<LastFmApiEndpoints>
{
    public string RedirectUri { get; set; }
}

public class LastFmApiEndpoints
{
    public string AuthUrl { get; set; }
    public string CreateSession { get; set; }
    public string HasSession { get; set; }
}


