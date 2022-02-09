using Microsoft.Extensions.Options;
using System.Net.Http.Json;

namespace Cadenza.Library.Repositories
{
    internal class ApiRepository : ApiRepositoryBase, ISourceTrackRepository, ISourcePlayTrackRepository, ISourceArtistRepository, ISourceSearchRepository
    {
        private readonly IOptions<ApiRepositorySettings> _settings;

        public ApiRepository(IHttpHelper http, IOptions<ApiRepositorySettings> settings, ISource source)
            : base(http, settings, source)
        {
            _settings = settings;
        }

        public async Task<TrackFull> GetTrack(string id)
        {
            return await Get<TrackFull>(_settings.Value.Track);
        }

        public async Task<ListResponse<PlayTrack>> GetAll(int page, int limit)
        {
            return await Get<ListResponse<PlayTrack>>(_settings.Value.PlayTracks, page, limit);
        }

        public async Task<ListResponse<PlayTrack>> GetByAlbum(string id, int page, int limit)
        {
            return await Get<ListResponse<PlayTrack>>(_settings.Value.PlayAlbum, page, limit);
        }

        public async Task<ListResponse<PlayTrack>> GetByArtist(string id, int page, int limit)
        {
            return await Get<ListResponse<PlayTrack>>(_settings.Value.PlayArtist, page, limit);
        }

        public async Task<ListResponse<Artist>> GetAlbumArtists(int page, int limit)
        {
            return await Get<ListResponse<Artist>>(_settings.Value.AlbumArtists, page, limit);
        }

        public async Task<ListResponse<AlbumInfo>> GetAlbums(string artistId, int page, int limit)
        {
            return await Get<ListResponse<AlbumInfo>>(_settings.Value.ArtistAlbums, page, limit);
        }

        public async Task<ListResponse<Artist>> GetAllArtists(int page, int limit)
        {
            return await Get<ListResponse<Artist>>(_settings.Value.AllArtists, page, limit);
        }

        public async Task<ArtistInfo> GetArtist(string id)
        {
            return await Get<ArtistInfo>(_settings.Value.Artist);
        }

        public async Task<ListResponse<Artist>> GetTrackArtists(int page, int limit)
        {
            return await Get<ListResponse<Artist>>(_settings.Value.TrackArtists, page, limit);
        }

        public async Task<ListResponse<SearchableItem>> GetSearchAlbums(int page, int limit)
        {
            return await Get<ListResponse<SearchableItem>>(_settings.Value.SearchAlbums, page, limit);
        }

        public async Task<ListResponse<SearchableItem>> GetSearchArtists(int page, int limit)
        {
            return await Get<ListResponse<SearchableItem>>(_settings.Value.SearchArtists, page, limit);
        }

        public async Task<ListResponse<SearchableItem>> GetSearchPlaylists(int page, int limit)
        {
            return await Get<ListResponse<SearchableItem>>(_settings.Value.SearchPlaylists, page, limit);
        }

        public async Task<ListResponse<SearchableItem>> GetSearchTracks(int page, int limit)
        {
            return await Get<ListResponse<SearchableItem>>(_settings.Value.SearchTracks, page, limit);
        }
    }

    internal abstract class ApiRepositoryBase
    {
        private readonly IOptions<ApiRepositorySettings> _settings;
        private readonly IHttpHelper _http;
        private readonly ISource _source;

        public ApiRepositoryBase(IHttpHelper http, IOptions<ApiRepositorySettings> settings, ISource source)
        {
            _http = http;
            _settings = settings;
            _source = source;
        }

        public LibrarySource Source => _source.Source;

        public async Task<T> Get<T>(string endpoint, int page, int limit)
        {
            var url = GetApiEndpoint(endpoint, page, limit);
            var response = await _http.Get(url);
            return await response.Content.ReadFromJsonAsync<T>();
        }

        public async Task<T> Get<T>(string endpoint)
        {
            var url = GetApiEndpoint(endpoint);
            var response = await _http.Get(url);
            return await response.Content.ReadFromJsonAsync<T>();
        }

        private string GetApiEndpoint(string endpoint)
        {
            return $"{_settings.Value.BaseUrl}{endpoint}";
        }

        private string GetApiEndpoint(string endpoint, int page, int limit)
        {
            return $"{GetApiEndpoint(endpoint)}?page={page}&limit={limit}";
        }
    }

    public class ApiRepositorySettings
    {
        public string BaseUrl { get; set; }

        public string Artist { get; set; }
        public string ArtistAlbums { get; set; }
        public string AllArtists { get; set; }
        public string AlbumArtists { get; set; }
        public string TrackArtists { get; set; }

        public string PlayTracks { get; set; }
        public string PlayArtist { get; set; }
        public string PlayAlbum { get; set; }

        public string SearchArtists { get; set; }
        public string SearchAlbums { get; set; }
        public string SearchTracks { get; set; }
        public string SearchPlaylists { get; set; }

        public string Track { get; set; }
    }
}
