namespace Cadenza.API.Database.Interfaces;

internal interface IDataAccess
{
    Task<List<JsonArtist>> GetArtists();
    Task<List<JsonAlbum>> GetAlbums(LibrarySource source);
    Task<List<JsonTrack>> GetTracks(LibrarySource source);
    Task<List<JsonAlbumTrack>> GetAlbumTracks(LibrarySource source);
    Task<JsonItems> GetAll(LibrarySource? source);
    Task<List<ItemUpdates>> GetUpdates(LibrarySource source);

    Task UpdateAlbum(LibrarySource source, string id, Action<JsonAlbum> update);
    Task UpdateArtist(string id, Action<JsonArtist> update);
    Task UpdateTrack(LibrarySource source, string id, Action<JsonTrack> update);
    Task UpdateLibrary(LibrarySource source, Action<JsonItems> update);
    Task UpdateUpdates(LibrarySource source, Action<List<ItemUpdates>> update);
}
