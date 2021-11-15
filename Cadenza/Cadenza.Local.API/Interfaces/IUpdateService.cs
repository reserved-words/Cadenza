namespace Cadenza.Local.API;

public interface IUpdateService
{
    Task<bool> UpdateAlbum(AlbumUpdate album);
    Task<bool> UpdateArtist(ArtistUpdate artist);
    Task<FileUpdateQueue> GetQueue();
    Task<bool> UpdateTrack(TrackUpdate track);
    Task Unqueue(MetaDataUpdate update);
}
