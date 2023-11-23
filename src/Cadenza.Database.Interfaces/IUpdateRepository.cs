namespace Cadenza.Database.Interfaces;

public interface IUpdateRepository
{
    Task RemoveTrack(int id);
    Task RemoveTracks(List<string> idsFromSource);
    Task UpdateArtist(ItemUpdateRequestDTO request);
    Task UpdateAlbum(ItemUpdateRequestDTO request);
    Task UpdateTrack(ItemUpdateRequestDTO request);
    Task AddTrack(LibrarySource source, SyncTrackDTO track);
}
