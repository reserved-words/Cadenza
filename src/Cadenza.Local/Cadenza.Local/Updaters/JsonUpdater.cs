//using Cadenza.Library;

//namespace Cadenza.Local;

//public class JsonUpdater : ISourceUpdater
//{
//    private readonly IBase64Converter _base64Converter;
//    private readonly IDataAccess _dataAccess;
//    private readonly IJsonMerger _merger;

//    public JsonUpdater(IDataAccess dataAccess, IJsonMerger merger, IBase64Converter base64Converter)
//    {
//        _dataAccess = dataAccess;
//        _merger = merger;
//        _base64Converter = base64Converter;
//    }

//    public async Task<bool> Update(AlbumInfo album, List<ItemPropertyUpdate> updates)
//    {
//        var albums = await _dataAccess.GetAlbums();
//        var existingAlbum = albums.Single(a => a.Id == album.Id);
//        var position = albums.IndexOf(existingAlbum);
//        var artists = _dataAccess.GetArtists();
//        var updatedAlbum = _merger.Update(existingAlbum, album, artists);
//        albums.Remove(existingAlbum);
//        albums.Insert(position, updatedAlbum);
//        await _dataAccess.SaveAlbums(albums);
//        return true;
//    }

//    public async Task<bool> Update(ArtistInfo artist, List<ItemPropertyUpdate> updates)
//    {
//        var artists = await _dataAccess.GetArtists();
//        var existingArtist = artists.Single(a => a.Id == artist.Id);
//        var position = artists.IndexOf(existingArtist);
//        var updatedArtist = _merger.Update(existingArtist, artist);
//        artists.Remove(existingArtist);
//        artists.Insert(position, updatedArtist);
//        await _dataAccess.SaveArtists(artists);
//        return true;
//    }

//    public async Task<bool> Update(TrackInfo track, List<ItemPropertyUpdate> updates)
//    {
//        var path = _base64Converter.FromBase64(track.Id);
//        var tracks = await _dataAccess.GetTracks();
//        var artists = await _dataAccess.GetArtists();
//        var existingTrack = tracks.Single(t => t.Path == path);
//        var position = tracks.IndexOf(existingTrack);
//        var updatedTrack = _merger.Update(existingTrack, track, artists);
//        tracks.Remove(existingTrack);
//        tracks.Insert(position, updatedTrack);
//        await _dataAccess.SaveTracks(tracks);
//        return true;
//    }
//}