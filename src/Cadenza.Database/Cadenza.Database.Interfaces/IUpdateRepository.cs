namespace Cadenza.Database.Interfaces;

public interface IUpdateRepository
{
    Task RemoveTrack(int id);
    Task RemoveTracks(List<string> idsFromSource);
    Task UpdateArtist(UpdatedArtistPropertiesDTO update);
    Task UpdateAlbum(UpdatedAlbumPropertiesDTO update);
    Task UpdateAlbumTrack(UpdatedAlbumTrackPropertiesDTO update);
    Task UpdateTrack(UpdatedTrackPropertiesDTO update);
    Task AddTrack(LibrarySource source, SyncTrackDTO track);
    Task LoveTrack(string username, int trackId);
    Task UnloveTrack(string username, int trackId);
}
