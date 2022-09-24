using Cadenza.Utilities.Interfaces;
using System.Net.Http.Json;

namespace Cadenza.Library.Repositories
{
    internal abstract class ApiRepositoryBase
    {
        private readonly IApiRepositorySettings _settings;
        private readonly IHttpHelper _http;

        public ApiRepositoryBase(IHttpHelper http, IApiRepositorySettings settings)
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
            var response = await _http.Post(url, null, data);
            return;
        }

        private string GetApiEndpoint(string endpoint)
        {
            return $"{_settings.BaseUrl}{endpoint}";
        }

        private string GetApiEndpoint(string endpoint, string id)
        {
            return $"{GetApiEndpoint(endpoint)}?id={id}";
        }
    }
}
