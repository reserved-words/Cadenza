using Cadenza.Common.Domain.JsonConverters;
using System.Net.Http.Json;

namespace Cadenza.Common.Utilities.Extensions;

public static class HttpHelperExtensions
{
    public static async Task<T> Get<T>(this IHttpHelper http, string url)
    {
        var response = await http.Get(url);
        return await response.Get<T>();
    }
    public static async Task<string> GetString(this IHttpHelper http, string url)
    {
        var response = await http.Get(url);
        return await response.GetString();
    }

    private static async Task<string> GetString(this HttpResponseMessage response)
    {
        if (!response.IsSuccessStatusCode)
        {
            throw new HttpException(response);
        }

        return await response.Content.ReadAsStringAsync();
    }

    private static async Task<T> Get<T>(this HttpResponseMessage response)
    {
        if (!response.IsSuccessStatusCode)
        {
            throw new HttpException(response);
        }

        return await response.Content.ReadFromJsonAsync<T>(JsonSerialization.Options);
    }
}
