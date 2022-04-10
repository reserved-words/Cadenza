using Cadenza.Local.API.Interfaces;

namespace Cadenza.Local.API;

public class ExternalSourceService : IExternalSourceService
{
    private readonly IDataAccess _dataAccess;
    private readonly IAlbumConverter _albumConverter;
    private readonly IArtistConverter _artistConverter;
    private readonly ITrackConverter _trackConverter;
    private readonly IAlbumTrackLinkConverter _albumTrackLinkConverter;

    public ExternalSourceService(IDataAccess dataAccess, IArtistConverter artistConverter,
        IAlbumConverter albumConverter, ITrackConverter trackConverter, IAlbumTrackLinkConverter albumTrackLinkConverter)
    {
        _dataAccess = dataAccess;
        _artistConverter = artistConverter;
        _albumConverter = albumConverter;
        _trackConverter = trackConverter;
        _albumTrackLinkConverter = albumTrackLinkConverter;
    }

    public async Task AddLibrary(FullLibrary library)
    {
        try
        {
            // todo - in the sync service, regularly remove artists with no albums

            var jsonLibrary = await _dataAccess.GetAll(LibrarySource.Spotify); // todo - get the source properly

            foreach (var artist in library.Artists)
            {
                var jsonArtist = jsonLibrary.Artists.SingleOrDefault(a => a.Id == artist.Id);
                if (jsonArtist == null)
                {
                    jsonArtist = _artistConverter.ToJsonModel(artist);
                    jsonLibrary.Artists.Add(jsonArtist);
                }
            }

            foreach (var album in library.Albums)
            {
                var jsonAlbum = jsonLibrary.Albums.SingleOrDefault(a => a.Id == album.Id);
                if (jsonAlbum == null)
                {
                    jsonAlbum = _albumConverter.ToJsonModel(album);
                    jsonLibrary.Albums.Add(jsonAlbum);
                }
            }

            foreach (var track in library.Tracks)
            {
                var jsonTrack = jsonLibrary.Tracks.SingleOrDefault(a => a.Path == track.Id);
                if (jsonTrack == null)
                {
                    jsonTrack = _trackConverter.ToJsonModel(track);
                    jsonLibrary.Tracks.Add(jsonTrack);
                }
            }

            foreach (var albumTrack in library.AlbumTrackLinks)
            {
                var jsonAlbumTrack = jsonLibrary.AlbumTrackLinks.SingleOrDefault(a => a.TrackPath == albumTrack.TrackId);
                if (jsonAlbumTrack == null)
                {
                    // todo - should use converter but need to sort out track ID / path issue
                    jsonAlbumTrack = new JsonAlbumTrackLink
                    {
                        AlbumId = albumTrack.AlbumId,
                        TrackPath = albumTrack.TrackId,
                        TrackNo = albumTrack.Position.TrackNo,
                        DiscNo = albumTrack.Position.DiscNo
                    };
                    jsonLibrary.AlbumTrackLinks.Add(jsonAlbumTrack);
                }
            }

            var albumIdsToKeep = library.Albums.Select(a => a.Id).ToList();
            var trackPathsToKeep = library.Tracks.Select(a => a.Id).ToList();

            jsonLibrary.Albums = jsonLibrary.Albums.Where(a => albumIdsToKeep.Contains(a.Id)).ToList();
            jsonLibrary.Tracks = jsonLibrary.Tracks.Where(a => trackPathsToKeep.Contains(a.Path)).ToList();
            jsonLibrary.AlbumTrackLinks = jsonLibrary.AlbumTrackLinks.Where(a => trackPathsToKeep.Contains(a.TrackPath)).ToList();

            await _dataAccess.SaveAll(jsonLibrary, LibrarySource.Spotify);
        }
        catch (Exception ex)
        {

            throw;
        }
    }
}
