using Cadenza.Local.API.Interfaces;
using Cadenza.Local.Common.Interfaces;
using Cadenza.Local.Common.Interfaces.Converters;
using Cadenza.Local.Common.Model.Json;

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

    public async Task AddLibrary(ExternalSourceLibrary sourceLibrary)
    {
        var library = sourceLibrary.Library;

        var jsonLibrary = await _dataAccess.GetAll(sourceLibrary.Source);

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
            var jsonTrack = jsonLibrary.Tracks.SingleOrDefault(a => a.Id == track.Id);
            if (jsonTrack == null)
            {
                jsonTrack = _trackConverter.ToJsonModel(track);
                jsonLibrary.Tracks.Add(jsonTrack);
            }
        }

        foreach (var albumTrack in library.AlbumTrackLinks)
        {
            var jsonAlbumTrack = jsonLibrary.AlbumTrackLinks.SingleOrDefault(a => a.TrackId == albumTrack.TrackId);
            if (jsonAlbumTrack == null)
            {
                jsonAlbumTrack = _albumTrackLinkConverter.ToJsonModel(albumTrack);
                jsonLibrary.AlbumTrackLinks.Add(jsonAlbumTrack);
            }
        }

        var albumIdsToKeep = library.Albums.Select(a => a.Id).ToList();
        var trackIdsToKeep = library.Tracks.Select(a => a.Id).ToList();

        jsonLibrary.Albums = jsonLibrary.Albums.Where(a => albumIdsToKeep.Contains(a.Id)).ToList();
        jsonLibrary.Tracks = jsonLibrary.Tracks.Where(t => trackIdsToKeep.Contains(t.Id)).ToList();
        jsonLibrary.AlbumTrackLinks = jsonLibrary.AlbumTrackLinks.Where(a => trackIdsToKeep.Contains(a.TrackId)).ToList();

        await _dataAccess.SaveAll(jsonLibrary, sourceLibrary.Source);

        await _dataAccess.SaveUpdateHistory(new JsonUpdateHistory {  ModifiedFilesLastUpdated = DateTime.Now }, sourceLibrary.Source);
    }

    public async Task<DateTime?> GetLastSyncDate(LibrarySource source)
    {
        var updateHistory = await _dataAccess.GetUpdateHistory(source);
        return updateHistory?.ModifiedFilesLastUpdated;
    }
}
