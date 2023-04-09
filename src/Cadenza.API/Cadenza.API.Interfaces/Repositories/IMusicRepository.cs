namespace Cadenza.API.Interfaces.Repositories;

public interface IMusicRepository
{
    Task<FullLibrary> Get(LibrarySource? source);
    Task<List<string>> GetAllTracks(LibrarySource source);
    Task RemoveTracks(LibrarySource source, List<string> id);
    Task UpdateArtist(ItemUpdates updates);
    Task UpdateAlbum(LibrarySource source, ItemUpdates updates);
    Task UpdateTrack(LibrarySource source, ItemUpdates updates);
    Task AddTrack(LibrarySource source, SyncTrack track);
}
