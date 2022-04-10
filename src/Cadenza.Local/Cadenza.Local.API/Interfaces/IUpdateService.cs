using Cadenza.Local.Common.Model;

namespace Cadenza.Local.API.Interfaces;

public interface IUpdateService
{
    Task UpdateAlbum(AlbumUpdate update);
    Task UpdateArtist(ArtistUpdate update);
    Task UpdateTrack(TrackUpdate update);
    Task<FileUpdateQueue> GetUpdates();
}
