using Cadenza.Utilities;
using System.Net;
using System.Net.Http.Json;

namespace Cadenza.API.Wrapper.LastFM;

public static class HttpHelperExtensions
{
    public static async Task<T> Get<T>(this IHttpHelper http, string url)
    {
        try
        {
            var response = await http.Get(url);
            return await response.Get<T>();
        }
        catch (Exception)
        {
            throw new HttpException();
        }
    }
    public static async Task Post(this IHttpHelper http, string url)
    {
        try
        {
            var response = await http.Post(url);
            response.Validate();
        }
        catch (Exception)
        {
            throw new HttpException();
        }
    }

    private static async Task<T> Get<T>(this HttpResponseMessage response)
    {
        if (!response.IsSuccessStatusCode)
        {
            if (response.StatusCode == HttpStatusCode.NotFound)
            {
                throw new HttpException();
            }
        }

        return await response.Content.ReadFromJsonAsync<T>();
    }

    private static void Validate(this HttpResponseMessage response)
    {
        if (!response.IsSuccessStatusCode)
        {
            if (response.StatusCode == HttpStatusCode.NotFound)
            {
                throw new HttpException();
            }
        }
    }
}
