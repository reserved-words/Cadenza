using System.Net.Http.Json;

namespace Cadenza.Library.Repositories
{
    internal class ApiRepository : ApiRepositoryBase, ISourceTrackRepository, ISourcePlayTrackRepository, ISourceArtistRepository, ISourceSearchRepository
    {
        private readonly IApiRepositorySettings _settings;

        public ApiRepository(IHttpHelper http, ISource source, IApiRepositorySettings settings)
            : base(http, settings, source)
        {
            _settings = settings;
        }

        public async Task<TrackFull> GetTrack(string id)
        {
            return await Get<TrackFull>(_settings.Track, id);
        }

        public async Task<ListResponse<PlayTrack>> GetAll(int page, int limit)
        {
            return await Get<ListResponse<PlayTrack>>(_settings.PlayTracks, page, limit);
        }

        public async Task<ListResponse<PlayTrack>> GetByAlbum(string id, int page, int limit)
        {
            return await Get<ListResponse<PlayTrack>>(_settings.PlayAlbum, id, page, limit);
        }

        public async Task<ListResponse<PlayTrack>> GetByArtist(string id, int page, int limit)
        {
            return await Get<ListResponse<PlayTrack>>(_settings.PlayArtist, id, page, limit);
        }

        public async Task<ListResponse<Artist>> GetAlbumArtists(int page, int limit)
        {
            return await Get<ListResponse<Artist>>(_settings.AlbumArtists, page, limit);
        }

        public async Task<ListResponse<AlbumInfo>> GetAlbums(string artistId, int page, int limit)
        {
            return await Get<ListResponse<AlbumInfo>>(_settings.ArtistAlbums, page, limit);
        }

        public async Task<ListResponse<Artist>> GetAllArtists(int page, int limit)
        {
            return await Get<ListResponse<Artist>>(_settings.AllArtists, page, limit);
        }

        public async Task<ArtistInfo> GetArtist(string id)
        {
            return await Get<ArtistInfo>(_settings.Artist, id);
        }

        public async Task<ListResponse<Artist>> GetTrackArtists(int page, int limit)
        {
            return await Get<ListResponse<Artist>>(_settings.TrackArtists, page, limit);
        }

        public async Task<ListResponse<SearchableItem>> GetSearchAlbums(int page, int limit)
        {
            return await Get<ListResponse<SearchableItem>>(_settings.SearchAlbums, page, limit);
        }

        public async Task<ListResponse<SearchableItem>> GetSearchArtists(int page, int limit)
        {
            return await Get<ListResponse<SearchableItem>>(_settings.SearchArtists, page, limit);
        }

        public async Task<ListResponse<SearchableItem>> GetSearchPlaylists(int page, int limit)
        {
            return await Get<ListResponse<SearchableItem>>(_settings.SearchPlaylists, page, limit);
        }

        public async Task<ListResponse<SearchableItem>> GetSearchTracks(int page, int limit)
        {
            return await Get<ListResponse<SearchableItem>>(_settings.SearchTracks, page, limit);
        }
    }

    internal abstract class ApiRepositoryBase
    {
        private readonly IApiRepositorySettings _settings;
        private readonly IHttpHelper _http;
        private readonly ISource _source;

        public ApiRepositoryBase(IHttpHelper http, IApiRepositorySettings settings, ISource source)
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

        public async Task<T> Get<T>(string endpoint, string id, int page, int limit)
        {
            var url = GetApiEndpoint(endpoint, id, page, limit);
            var response = await _http.Get(url);
            return await response.Content.ReadFromJsonAsync<T>();
        }

        public async Task<T> Get<T>(string endpoint, string id)
        {
            var url = GetApiEndpoint(endpoint, id);
            var response = await _http.Get(url);
            return await response.Content.ReadFromJsonAsync<T>();
        }

        private string GetApiEndpoint(string endpoint)
        {
            return $"{_settings.BaseUrl}{endpoint}";
        }

        private string GetApiEndpoint(string endpoint, string id)
        {
            return $"{GetApiEndpoint(endpoint)}?id={id}";
        }

        private string GetApiEndpoint(string endpoint, int page, int limit)
        {
            return $"{GetApiEndpoint(endpoint)}?page={page}&limit={limit}";
        }

        private string GetApiEndpoint(string endpoint, string id, int page, int limit)
        {
            return $"{GetApiEndpoint(endpoint)}?id={id}&page={page}&limit={limit}";
        }
    }

    public interface IApiRepositorySettings
    {
        string BaseUrl { get; }

        string Artist { get; }
        string ArtistAlbums { get; }
        string AllArtists { get; }
        string AlbumArtists { get; }
        string TrackArtists { get; }

        string PlayTracks { get; }
        string PlayArtist { get; }
        string PlayAlbum { get; }

        string SearchArtists { get; }
        string SearchAlbums { get; }
        string SearchTracks { get; }
        string SearchPlaylists { get; }

        string Track { get; }
    }
}
