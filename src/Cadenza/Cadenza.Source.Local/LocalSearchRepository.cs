using Microsoft.Extensions.Options;

namespace Cadenza.Source.Local
{
    internal class LocalSearchRepository : ISearchRepository
    {
        private readonly IOptions<LocalApiSettings> _settings;
        private readonly IHttpHelper _http;

        public LocalSearchRepository(IHttpHelper http, IOptions<LocalApiSettings> settings)
        {
            _http = http;
            _settings = settings;
        }

        public LibrarySource Source => LibrarySource.Local;

        public async Task<ListResponse<SearchableItem>> GetSearchAlbums(int page, int limit)
        {
            var response = await _http.Get(GetApiEndpoint(e => e.SearchAlbums, page, limit));
            return await response.Content.ReadFromJsonAsync<ListResponse<SearchableItem>>();
        }

        public async Task<ListResponse<SearchableItem>> GetSearchArtists(int page, int limit)
        {
            var response = await _http.Get(GetApiEndpoint(e => e.SearchArtists, page, limit));
            return await response.Content.ReadFromJsonAsync<ListResponse<SearchableItem>>();
        }

        public async Task<ListResponse<SearchableItem>> GetSearchPlaylists(int page, int limit)
        {
            var response = await _http.Get(GetApiEndpoint(e => e.SearchPlaylists, page, limit));
            return await response.Content.ReadFromJsonAsync<ListResponse<SearchableItem>>();
        }

        public async Task<ListResponse<SearchableItem>> GetSearchTracks(int page, int limit)
        {
            var response = await _http.Get(GetApiEndpoint(e => e.SearchTracks, page, limit));
            return await response.Content.ReadFromJsonAsync<ListResponse<SearchableItem>>();
        }

        public Task Populate()
        {
            return Task.CompletedTask;
        }

        private string GetApiEndpoint(Func<LocalApiEndpoints, string> getEndpoint, int page, int limit)
        {
            return string.Format(_settings.GetApiEndpoint(getEndpoint), page, limit);
        }
    }
}
