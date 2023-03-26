using Cadenza.API.Interfaces.Repositories;
using Cadenza.API.SqlLibrary.Interfaces;
using Cadenza.Common.Domain.Model.Updates;

namespace Cadenza.API.SqlLibrary;
internal class MusicRepository : IMusicRepository
{
    private readonly IDataMapper _mapper;
    private readonly IInsertService _insertService;
    private readonly IReadService _readService;

    public MusicRepository(IInsertService insertService, IDataMapper mapper, IReadService readService)
    {
        _insertService = insertService;
        _mapper = mapper;
        _readService = readService;
    }

    public async Task AddTrack(LibrarySource source, TrackFull track)
    {
        track.Track.Source = source;
        track.Album.Source = source;

        var trackArtistData = _mapper.MapTrackArtist(track);
        var trackArtistId = await _insertService.AddArtist(trackArtistData);

        var albumArtistId = trackArtistId;

        if (track.Artist.Name != track.Album.ArtistName)
        {
            var albumArtistData = _mapper.MapAlbumArtist(track);
            albumArtistId = await _insertService.AddArtist(albumArtistData);
        }

        var albumData = _mapper.MapAlbum(track, albumArtistId);
        var albumId = await _insertService.AddAlbum(albumData);

        var discData = _mapper.MapDisc(track, albumId);
        var discId = await _insertService.AddDisc(discData);

        var trackData = _mapper.MapTrack(track, trackArtistId, discId);
        await _insertService.AddTrack(trackData);
    }

    public async Task<FullLibrary> Get(LibrarySource? source)
    {
        try
        {
            var artistsData = await _readService.GetArtists();
            var artists = artistsData.Select(a => _mapper.MapArtist(a)).ToList();

            var library = new FullLibrary
            {
                Artists = artists,
                Albums = new List<AlbumInfo>(),
                Tracks = new List<TrackInfo>(),
                AlbumTracks = new List<AlbumTrackLink>()
            };

            if (source.HasValue)
            {
                await AddSource(library, source.Value);
            }
            else
            {
                foreach (var src in Enum.GetValues<LibrarySource>())
                {
                    await AddSource(library, src);
                }
            }

            return library;
        }
        catch (Exception ex)
        {

            throw;
        }
    }

    public async Task<List<string>> GetAllTracks(LibrarySource source)
    {
        return await _readService.GetAllTrackIds(source);
    }

    public Task RemoveTracks(LibrarySource source, List<string> id)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAlbum(LibrarySource source, ItemUpdates updates)
    {
        throw new NotImplementedException();
    }

    public Task UpdateArtist(ItemUpdates updates)
    {
        throw new NotImplementedException();
    }

    public Task UpdateTrack(LibrarySource source, ItemUpdates updates)
    {
        throw new NotImplementedException();
    }

    private async Task AddSource(FullLibrary library, LibrarySource source)
    {
        var albumsData = await _readService.GetAlbums(source);
        var discsData = await _readService.GetDiscs(source);
        var tracksData = await _readService.GetTracks(source);

        var albums = albumsData.Select(a => _mapper.MapAlbum(a, discsData.Where(d => d.AlbumId == a.Id).ToList())).ToList();
        var albumTracks = tracksData.Select(t => _mapper.MapAlbumTrack(t)).ToList();
        var tracks = tracksData.Select(t => _mapper.MapTrack(t)).ToList();

        library.Albums.AddRange(albums);
        library.Tracks.AddRange(tracks);
        library.AlbumTracks.AddRange(albumTracks);
    }
}
