namespace Cadenza.API.Interfaces.Repositories;

public interface IMusicRepository
{
    Task<FullLibrary> Get(LibrarySource? source);
    Task<List<string>> GetAllTracks(LibrarySource source);
    Task RemoveTrack(int id);
    Task RemoveTracks(List<string> idsFromSource);
    Task UpdateArtist(ItemUpdateRequest request);
    Task UpdateAlbum(LibrarySource source, ItemUpdateRequest request);
    Task UpdateTrack(LibrarySource source, ItemUpdateRequest request);
    Task AddTrack(LibrarySource source, SyncTrack track);
}
