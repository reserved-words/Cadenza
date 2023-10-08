namespace Cadenza.API.Interfaces.Repositories;

public interface IMusicRepository
{
    Task<FullLibraryDTO> Get();
    Task<List<string>> GetAllTracks(LibrarySource source);
    Task<List<string>> GetAlbumTrackSourceIds(int albumId);
    Task<List<string>> GetArtistTrackSourceIds(int artistId);
    Task<string> GetTrackIdFromSource(int trackId);
    Task RemoveTrack(int id);
    Task RemoveTracks(List<string> idsFromSource);
    Task UpdateArtist(ItemUpdateRequestDTO request);
    Task UpdateAlbum(ItemUpdateRequestDTO request);
    Task UpdateTrack(ItemUpdateRequestDTO request);
    Task AddTrack(LibrarySource source, SyncTrackDTO track);
}
