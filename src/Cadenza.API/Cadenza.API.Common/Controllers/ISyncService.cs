using Cadenza.Domain.Enums;
using Cadenza.Domain.Models;
using Cadenza.Domain.Models.Track;

namespace Cadenza.API.Common.Controllers;

public interface ISyncService
{
    Task AddTrack(LibrarySource source, TrackFull track);
    Task<List<string>> GetAllTracks(LibrarySource source);
    Task<List<string>> GetTracksByArtist(LibrarySource source, string artistId);
    Task<List<string>> GetTracksByAlbum(LibrarySource source, string albumId);
    Task<List<ItemUpdates>> GetUpdates(LibrarySource source);
    Task MarkUpdated(LibrarySource source, LibraryItemType itemType, string id);
    Task RemoveTrack(LibrarySource source, string id);
}
