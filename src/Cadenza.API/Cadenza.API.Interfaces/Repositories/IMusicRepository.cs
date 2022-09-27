using Cadenza.Domain.Model;
using Cadenza.Domain.Model.Track;
using Cadenza.Domain.Model.Updates;

namespace Cadenza.API.Interfaces.Repositories;

public interface IMusicRepository
{
    Task<FullLibrary> Get(LibrarySource? source);
    Task RemoveTracks(LibrarySource source, List<string> id);
    Task UpdateArtist(ItemUpdates updates);
    Task UpdateAlbum(LibrarySource source, ItemUpdates updates);
    Task UpdateTrack(LibrarySource source, ItemUpdates updates);
    Task AddTrack(LibrarySource source, TrackFull track);
}
