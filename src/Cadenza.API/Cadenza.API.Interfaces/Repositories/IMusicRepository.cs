namespace Cadenza.API.Interfaces.Repositories;

public interface IMusicRepository
{
    Task<FullLibrary> Get(LibrarySource? source);
    Task<List<string>> GetAllTracks(LibrarySource source);
    Task<List<string>> GetAlbumTrackSourceIds(LibrarySource source, int albumId);
    Task<List<string>> GetArtistTrackSourceIds(LibrarySource source, int artistId);
    Task<string> GetTrackIdFromSource(int trackId);
    Task RemoveTrack(int id);
    Task RemoveTracks(List<string> idsFromSource);
    Task UpdateArtist(ItemUpdateRequest request);
    Task UpdateAlbum(LibrarySource source, ItemUpdateRequest request);
    Task UpdateTrack(LibrarySource source, ItemUpdateRequest request);
    Task AddTrack(LibrarySource source, SyncTrack track);
}
