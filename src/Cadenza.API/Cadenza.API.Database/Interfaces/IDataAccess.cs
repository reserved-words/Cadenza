using Cadenza.API.Database.Model;
using Cadenza.Domain.Enums;
using Cadenza.Domain.Models;

namespace Cadenza.API.Database.Interfaces;

internal interface IDataAccess
{
    Task<List<JsonArtist>> GetArtists();
    Task<List<JsonAlbum>> GetAlbums(LibrarySource source);
    Task<List<JsonTrack>> GetTracks(LibrarySource source);
    Task<List<JsonAlbumTrackLink>> GetAlbumTrackLinks(LibrarySource source);
    Task<JsonItems> GetAll(LibrarySource source);
    Task<List<ItemUpdates>> GetUpdates(LibrarySource source);

    Task SaveArtists(List<JsonArtist> artists);
    Task SaveAlbums(List<JsonAlbum> albums, LibrarySource source);
    Task SaveTracks(List<JsonTrack> tracks, LibrarySource source);
    Task SaveAlbumTrackLinks(List<JsonAlbumTrackLink> albumTrackLinks, LibrarySource source);
    Task SaveAll(JsonItems items, LibrarySource source);
    Task SaveUpdates(List<ItemUpdates> items, LibrarySource source);
}