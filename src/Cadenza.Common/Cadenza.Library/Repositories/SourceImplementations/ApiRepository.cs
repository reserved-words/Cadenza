namespace Cadenza.Library.Repositories
{
    internal class ApiRepository : ApiRepositoryBase, 
        ISourceTrackRepository, 
        ISourcePlayTrackRepository, 
        ISourceArtistRepository, 
        ISourceSearchRepository, 
        ISourceAlbumRepository
    {
        private readonly IApiRepositorySettings _settings;

        public ApiRepository(LibrarySource source, IHttpHelper http, IApiRepositorySettings settings)
            : base(source, http, settings)
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

        public async Task<List<PlayTrack>> GetByAlbum(string id)
        {
            return await Get<List<PlayTrack>>(_settings.PlayAlbum, id);
        }

        public async Task<ListResponse<PlayTrack>> GetByArtist(string id, int page, int limit)
        {
            return await Get<ListResponse<PlayTrack>>(_settings.PlayArtist, id, page, limit);
        }

        public async Task<ListResponse<PlayTrack>> GetByGrouping(Grouping id, int page, int limit)
        {
            return await Get<ListResponse<PlayTrack>>(_settings.PlayGrouping, id.ToString(), page, limit);
        }

        public async Task<ListResponse<PlayTrack>> GetByGenre(string id, int page, int limit)
        {
            return await Get<ListResponse<PlayTrack>>(_settings.PlayGenre, id, page, limit);
        }

        public async Task<ListResponse<Artist>> GetAlbumArtists(int page, int limit)
        {
            return await Get<ListResponse<Artist>>(_settings.AlbumArtists, page, limit);
        }

        public async Task<ListResponse<Album>> GetAlbums(string id, int page, int limit)
        {
            return await Get<ListResponse<Album>>(_settings.ArtistAlbums, id, page, limit);
        }

        public async Task<ListResponse<Artist>> GetAllArtists(int page, int limit)
        {
            return await Get<ListResponse<Artist>>(_settings.AllArtists, page, limit);
        }

        public async Task<ListResponse<Artist>> GetArtistsByGrouping(Grouping id, int page, int limit)
        {
            return await Get<ListResponse<Artist>>(_settings.ArtistsByGrouping, id.ToString(), page, limit);
        }

        public async Task<ListResponse<Artist>> GetArtistsByGenre(string id, int page, int limit)
        {
            return await Get<ListResponse<Artist>>(_settings.ArtistsByGenre, id, page, limit);
        }

        public async Task<ArtistInfo> GetArtist(string id)
        {
            return await Get<ArtistInfo>(_settings.Artist, id);
        }

        public async Task<ListResponse<Artist>> GetTrackArtists(int page, int limit)
        {
            return await Get<ListResponse<Artist>>(_settings.TrackArtists, page, limit);
        }

        public async Task<ListResponse<PlayerItem>> GetSearchAlbums(int page, int limit)
        {
            return await Get<ListResponse<PlayerItem>>(_settings.SearchAlbums, page, limit);
        }

        public async Task<ListResponse<PlayerItem>> GetSearchArtists(int page, int limit)
        {
            return await Get<ListResponse<PlayerItem>>(_settings.SearchArtists, page, limit);
        }

        public async Task<ListResponse<PlayerItem>> GetSearchPlaylists(int page, int limit)
        {
            return await Get<ListResponse<PlayerItem>>(_settings.SearchPlaylists, page, limit);
        }

        public async Task<ListResponse<PlayerItem>> GetSearchTracks(int page, int limit)
        {
            return await Get<ListResponse<PlayerItem>>(_settings.SearchTracks, page, limit);
        }

        public async Task<ListResponse<PlayerItem>> GetSearchGenres(int page, int limit)
        {
            return await Get<ListResponse<PlayerItem>>(_settings.SearchGenres, page, limit);
        }

        public async Task<ListResponse<PlayerItem>> GetSearchGroupings(int page, int limit)
        {
            return await Get<ListResponse<PlayerItem>>(_settings.SearchGroupings, page, limit);
        }

        public async Task<AlbumInfo> GetAlbum(string id)
        {
            return await Get<AlbumInfo>(_settings.Album, id);
        }

        public async Task<List<AlbumTrack>> GetTracks(string id)
        {
            return await Get<List<AlbumTrack>>(_settings.AlbumTracks, id);
        }

        public async Task UpdateArtist(ArtistUpdate update)
        {
            await Post(_settings.UpdateArtist, update);
        }
    }
}
