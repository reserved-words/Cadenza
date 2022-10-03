namespace Cadenza.Web.Common.Model;

public class ApiOptions<TEndpoints>
{
    public string BaseUrl { get; set; }
    public TEndpoints Endpoints { get; set; }
}
