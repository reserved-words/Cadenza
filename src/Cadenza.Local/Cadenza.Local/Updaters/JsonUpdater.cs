using Cadenza.Library;

namespace Cadenza.Local;

public class JsonUpdater : ILibraryUpdater
{
    private readonly IBase64Converter _base64Converter;
    private readonly IDataAccess _dataAccess;
    private readonly IJsonMerger _merger;

    public JsonUpdater(IDataAccess dataAccess, IJsonMerger merger, IBase64Converter base64Converter)
    {
        _dataAccess = dataAccess;
        _merger = merger;
        _base64Converter = base64Converter;
    }

    public async Task<bool> Update(AlbumInfo album, List<ItemPropertyUpdate> updates)
    {
        var albums = _dataAccess.GetAlbums();
        var existingAlbum = albums.Single(a => a.Id == album.Id);
        var position = albums.IndexOf(existingAlbum);
        var artists = _dataAccess.GetArtists();
        var updatedAlbum = _merger.Update(existingAlbum, album, artists);
        albums.Remove(existingAlbum);
        albums.Insert(position, updatedAlbum);
        _dataAccess.SaveAlbums(albums);
        return true;
    }

    public async Task<bool> Update(ArtistInfo artist, List<ItemPropertyUpdate> updates)
    {
        var artists = _dataAccess.GetArtists();
        var existingArtist = artists.Single(a => a.Id == artist.Id);
        var position = artists.IndexOf(existingArtist);
        var updatedArtist = _merger.Update(existingArtist, artist);
        artists.Remove(existingArtist);
        artists.Insert(position, updatedArtist);
        _dataAccess.SaveArtists(artists);
        return true;
    }

    public async Task<bool> Update(TrackInfo track, List<ItemPropertyUpdate> updates)
    {
        var path = _base64Converter.FromBase64(track.Id);
        var tracks = _dataAccess.GetTracks();
        var artists = _dataAccess.GetArtists();
        var existingTrack = tracks.Single(t => t.Path == path);
        var position = tracks.IndexOf(existingTrack);
        var updatedTrack = _merger.Update(existingTrack, track, artists);
        tracks.Remove(existingTrack);
        tracks.Insert(position, updatedTrack);
        _dataAccess.SaveTracks(tracks);
        return true;
    }
}