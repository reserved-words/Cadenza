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

        public async Task<ListResponse<SearchableAlbum>> GetSearchAlbums(int page, int limit)
        {
            var response = await _http.Get(GetApiEndpoint(e => e.SearchAlbums));
            return await response.Content.ReadFromJsonAsync<ListResponse<SearchableAlbum>>();
        }

        public async Task<ListResponse<SearchableArtist>> GetSearchArtists(int page, int limit)
        {
            var response = await _http.Get(GetApiEndpoint(e => e.SearchArtists));
            return await response.Content.ReadFromJsonAsync<ListResponse<SearchableArtist>>();
        }

        public async Task<ListResponse<SearchablePlaylist>> GetSearchPlaylists(int page, int limit)
        {
            var response = await _http.Get(GetApiEndpoint(e => e.SearchPlaylists));
            return await response.Content.ReadFromJsonAsync<ListResponse<SearchablePlaylist>>();
        }

        public async Task<ListResponse<SearchableTrack>> GetSearchTracks(int page, int limit)
        {
            var response = await _http.Get(GetApiEndpoint(e => e.SearchTracks));
            return await response.Content.ReadFromJsonAsync<ListResponse<SearchableTrack>>();
        }

        private string GetApiEndpoint(Func<LocalApiEndpoints, string> getEndpoint, string parameter = null)
        {
            return string.Format(_settings.GetApiEndpoint(getEndpoint), parameter);
        }
    }
}
