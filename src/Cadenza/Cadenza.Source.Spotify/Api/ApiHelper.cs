﻿using Cadenza.Source.Spotify.Exceptions;
using Cadenza.Source.Spotify.Interfaces;
using Cadenza.Source.Spotify.Model;
using Cadenza.Utilities;
using System.Net;
using System.Net.Http.Json;

namespace Cadenza.Source.Spotify.Api;

internal class ApiHelper : IApiHelper
{
    private readonly IHttpHelper _httpClient;

    public ApiHelper(IHttpHelper httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<ApiResponse> Put(string url, string accessToken, object data = null)
    {
        var authHeader = GetAuthHeader(accessToken);

        var response = await _httpClient.Put(url, authHeader, data);

        CheckForCommonErrors(response);

        var error = !response.IsSuccessStatusCode
            ? await response.Content.ReadFromJsonAsync<ApiError>()
            : null;

        return new ApiResponse(error?.Error);
    }

    public async Task<ApiResponse<T>> Get<T>(string url, string accessToken) where T : class
    {
        var authHeader = GetAuthHeader(accessToken);
        var response = await _httpClient.Get(url, authHeader);

        CheckForCommonErrors(response);

        if (response.IsSuccessStatusCode)
        {
            var data = response.StatusCode == HttpStatusCode.NoContent
                ? null
                : await response.Content.ReadFromJsonAsync<T>();

            return new ApiResponse<T>(data);
        }
        else
        {
            var error = await response.Content.ReadFromJsonAsync<ApiError>();
            return new ApiResponse<T>(error);
        }
    }

    private void CheckForCommonErrors(HttpResponseMessage response)
    {
        if (response.StatusCode == HttpStatusCode.Unauthorized)
            throw new UnauthorizedApiException();

        // actually in some cases this could mean something else - narrow down more
        if (response.StatusCode == HttpStatusCode.NotFound)
            throw new DeviceNotFoundException();
    }

    private static string GetAuthHeader(string accessToken)
    {
        return $"Bearer {accessToken}";
    }
}