using Cadenza.Utilities.Interfaces;
using Cadenza.Web.Database.Settings;
using Microsoft.Extensions.Options;
using System.Net.Http.Json;

namespace Cadenza.Web.Database.Services
{
    // TODO: All this stuff was done in an awkward way previously to allow different repository types, can  be simplified now

    internal abstract class ApiRepositoryBase
    {
        private readonly IOptions<DatabaseApiSettings> _settings;
        private readonly IHttpHelper _http;

        public ApiRepositoryBase(IHttpHelper http, IOptions<DatabaseApiSettings> settings)
        {
            _http = http;
            _settings = settings;
        }

        public async Task<T> Get<T>(string endpoint)
        {
            var url = GetApiEndpoint(endpoint);
            var response = await _http.Get(url);
            return await response.Content.ReadFromJsonAsync<T>();
        }

        public async Task<T> Get<T>(string endpoint, string id)
        {
            var url = GetApiEndpoint(endpoint, id);
            var response = await _http.Get(url);
            return await response.Content.ReadFromJsonAsync<T>();
        }

        public async Task Post<T>(string endpoint, T data)
        {
            var url = GetApiEndpoint(endpoint);
            await _http.Post(url, null, data);
        }

        private string GetApiEndpoint(string endpoint)
        {
            return $"{_settings.Value.BaseUrl}{endpoint}";
        }

        private string GetApiEndpoint(string endpoint, string id)
        {
            return $"{GetApiEndpoint(endpoint)}?id={id}";
        }
    }
}
