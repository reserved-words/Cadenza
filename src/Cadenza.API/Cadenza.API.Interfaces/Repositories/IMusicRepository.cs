namespace Cadenza.API.Interfaces.Repositories;

public interface IMusicRepository
{
    Task<FullLibrary> Get();
    Task<List<string>> GetAllTracks(LibrarySource source);
    Task<List<string>> GetAlbumTrackSourceIds(int albumId);
    Task<List<string>> GetArtistTrackSourceIds(int artistId);
    Task<string> GetTrackIdFromSource(int trackId);
    Task RemoveTrack(int id);
    Task RemoveTracks(List<string> idsFromSource);
    Task UpdateArtist(ItemUpdateRequest request);
    Task UpdateAlbum(ItemUpdateRequest request);
    Task UpdateTrack(ItemUpdateRequest request);
    Task AddTrack(LibrarySource source, SyncTrack track);
}
