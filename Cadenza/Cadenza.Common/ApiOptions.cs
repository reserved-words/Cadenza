using Microsoft.Extensions.Options;

namespace Cadenza.Common
{
    public class ApiOptions<TEndpoints>
    {
        public string BaseUrl { get; set; }
        public TEndpoints Endpoints { get; set; }
    }

    public static class ApiOptionsExtensions
    {
        public static string GetApiEndpoint<TEndpoints>(this IOptions<ApiOptions<TEndpoints>> options, Func<TEndpoints, string> getEndpoint)
        {
            var baseUrl = options.Value.BaseUrl;
            var endpoint = getEndpoint(options.Value.Endpoints);
            return $"{baseUrl}{endpoint}";
        }
    }
}
