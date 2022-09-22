using Cadenza.API.Common.Model;

namespace Cadenza.API.Interfaces;

public interface IUpdateService
{
    Task UpdateAlbum(AlbumUpdate update);
    Task UpdateArtist(ArtistUpdate update);
    Task UpdateTrack(TrackUpdate update);
    Task<FileUpdateQueue> GetUpdates();
}
