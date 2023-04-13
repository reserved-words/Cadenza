namespace Cadenza.API.Interfaces.Repositories;

public interface IMusicRepository
{
    Task<FullLibrary> Get(LibrarySource? source);
    Task<List<string>> GetAllTracks(LibrarySource source);
    Task RemoveTracks(LibrarySource source, List<string> id);
    Task UpdateArtist(ItemUpdateRequest request);
    Task UpdateAlbum(LibrarySource source, ItemUpdateRequest request);
    Task UpdateTrack(LibrarySource source, ItemUpdateRequest request);
    Task AddTrack(LibrarySource source, SyncTrack track);
}
