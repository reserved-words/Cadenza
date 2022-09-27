using Cadenza.Domain.Enums;
using Cadenza.Domain.Model;
using Cadenza.Domain.Model.Album;
using Cadenza.Domain.Model.Artist;
using Cadenza.Domain.Model.Track;
using Cadenza.Domain.Model.Update;
using Cadenza.Library.Repositories;
using Cadenza.Utilities.Interfaces;

namespace Cadenza.Web.Database.Services
{
    internal class ApiRepository : ApiRepositoryBase,
        ITrackRepository,
        IPlayTrackRepository,
        IArtistRepository,
        ISearchRepository,
        IAlbumRepository
    {
        private readonly IApiRepositorySettings _settings;

        public ApiRepository(IHttpHelper http, IApiRepositorySettings settings)
            : base(http, settings)
        {
            _settings = settings;
        }

        public async Task<TrackFull> GetTrack(string id)
        {
            return await Get<TrackFull>(_settings.Track, id);
        }

        public async Task<List<PlayTrack>> GetAll()
        {
            return await Get<List<PlayTrack>>(_settings.PlayTracks);
        }

        public async Task<List<PlayTrack>> GetByAlbum(string id)
        {
            return await Get<List<PlayTrack>>(_settings.PlayAlbum, id);
        }

        public async Task<List<PlayTrack>> GetByArtist(string id)
        {
            return await Get<List<PlayTrack>>(_settings.PlayArtist, id);
        }

        public async Task<List<PlayTrack>> GetByGrouping(Grouping id)
        {
            return await Get<List<PlayTrack>>(_settings.PlayGrouping, id.ToString());
        }

        public async Task<List<PlayTrack>> GetByGenre(string id)
        {
            return await Get<List<PlayTrack>>(_settings.PlayGenre, id);
        }

        public async Task<List<Artist>> GetAlbumArtists()
        {
            return await Get<List<Artist>>(_settings.AlbumArtists);
        }

        public async Task<List<Album>> GetAlbums(string id)
        {
            return await Get<List<Album>>(_settings.ArtistAlbums, id);
        }

        public async Task<List<Artist>> GetAllArtists()
        {
            return await Get<List<Artist>>(_settings.AllArtists);
        }

        public async Task<List<Artist>> GetArtistsByGrouping(Grouping id)
        {
            return await Get<List<Artist>>(_settings.ArtistsByGrouping, id.ToString());
        }

        public async Task<List<Artist>> GetArtistsByGenre(string id)
        {
            return await Get<List<Artist>>(_settings.ArtistsByGenre, id);
        }

        public async Task<ArtistInfo> GetArtist(string id)
        {
            return await Get<ArtistInfo>(_settings.Artist, id);
        }

        public async Task<List<Artist>> GetTrackArtists()
        {
            return await Get<List<Artist>>(_settings.TrackArtists);
        }

        public async Task<List<PlayerItem>> GetSearchAlbums()
        {
            return await Get<List<PlayerItem>>(_settings.SearchAlbums);
        }

        public async Task<List<PlayerItem>> GetSearchArtists()
        {
            return await Get<List<PlayerItem>>(_settings.SearchArtists);
        }

        public async Task<List<PlayerItem>> GetSearchPlaylists()
        {
            return await Get<List<PlayerItem>>(_settings.SearchPlaylists);
        }

        public async Task<List<PlayerItem>> GetSearchTracks()
        {
            return await Get<List<PlayerItem>>(_settings.SearchTracks);
        }

        public async Task<List<PlayerItem>> GetSearchGenres()
        {
            return await Get<List<PlayerItem>>(_settings.SearchGenres);
        }

        public async Task<List<PlayerItem>> GetSearchGroupings()
        {
            return await Get<List<PlayerItem>>(_settings.SearchGroupings);
        }

        public async Task<AlbumInfo> GetAlbum(string id)
        {
            return await Get<AlbumInfo>(_settings.Album, id);
        }

        public async Task<List<AlbumTrack>> GetTracks(string id)
        {
            return await Get<List<AlbumTrack>>(_settings.AlbumTracks, id);
        }
    }
}
