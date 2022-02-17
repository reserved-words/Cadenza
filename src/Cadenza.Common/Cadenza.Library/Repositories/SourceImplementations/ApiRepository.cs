namespace Cadenza.Library.Repositories
{
    internal class ApiRepository : ApiRepositoryBase, ISourceTrackRepository, ISourcePlayTrackRepository, ISourceArtistRepository, ISourceSearchRepository, ISourceAlbumRepository
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
            return await Get<ListResponse<AlbumInfo>>(_settings.ArtistAlbums, artistId, page, limit);
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

        public async Task<AlbumInfo> GetAlbum(string id)
        {
            return await Get<AlbumInfo>(_settings.Album, id);
        }
    }
}
