namespace Cadenza.API.Database.Interfaces;

internal interface IDataAccess
{
    Task<List<ArtistInfo>> GetArtists();
    Task<List<AlbumInfo>> GetAlbums(LibrarySource source);
    Task<List<TrackInfo>> GetTracks(LibrarySource source);
    Task<List<AlbumTrackLink>> GetAlbumTracks(LibrarySource source);
    Task<FullLibrary> GetAll(LibrarySource? source);
    Task<List<EditedItem>> GetUpdates(LibrarySource source);

    Task UpdateAlbum(LibrarySource source, string id, Action<AlbumInfo> update);
    Task UpdateArtist(string id, Action<ArtistInfo> update);
    Task UpdateTrack(LibrarySource source, string id, Action<TrackInfo> update);
    Task UpdateLibrary(LibrarySource source, Action<FullLibrary> update);
    Task UpdateUpdates(LibrarySource source, Action<List<EditedItem>> update);
}
