using Cadenza.API.Database.Model;
using Cadenza.Domain;

namespace Cadenza.API.Database.Interfaces;

internal interface IDataAccess
{
    Task<List<JsonArtist>> GetArtists();
    Task<List<JsonAlbum>> GetAlbums(LibrarySource source);
    Task<List<JsonTrack>> GetTracks(LibrarySource source);
    Task<List<JsonAlbumTrackLink>> GetAlbumTrackLinks(LibrarySource source);
    Task<JsonItems> GetAll(LibrarySource source);
    Task<JsonUpdateHistory> GetUpdateHistory(LibrarySource source);

    Task SaveArtists(List<JsonArtist> artists);
    Task SaveAlbums(List<JsonAlbum> albums, LibrarySource source);
    Task SaveTracks(List<JsonTrack> tracks, LibrarySource source);
    Task SaveAlbumTrackLinks(List<JsonAlbumTrackLink> albumTrackLinks, LibrarySource source);
    Task SaveAll(JsonItems items, LibrarySource source);
    Task SaveUpdateHistory(JsonUpdateHistory history, LibrarySource source);
}