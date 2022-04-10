using System.Net.Http.Json;

namespace Cadenza.Core;

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

    public static async Task Post(this IHttpHelper http, string url)
    {
        var response = await http.Post(url, null, null);
        response.Validate();
    }

    private static async Task<string> GetString(this HttpResponseMessage response)
    {
        if (!response.IsSuccessStatusCode)
        {
            throw new HttpException();
        }

        return await response.Content.ReadAsStringAsync();
    }

    private static async Task<T> Get<T>(this HttpResponseMessage response)
    {
        if (!response.IsSuccessStatusCode)
        {
            throw new HttpException();
        }

        return await response.Content.ReadFromJsonAsync<T>();
    }

    private static void Validate(this HttpResponseMessage response)
    {
        if (!response.IsSuccessStatusCode)
        {
            throw new HttpException();
        }
    }
}
