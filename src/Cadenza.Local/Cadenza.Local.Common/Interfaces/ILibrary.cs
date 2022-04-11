using Cadenza.Domain;

namespace Cadenza.Local.Common.Interfaces;

public interface ILibrary
{
    Task<FullLibrary> Get();
    Task UpdateArtist(ArtistUpdate update);
    Task UpdateAlbum(AlbumUpdate update);
    Task UpdateTrack(TrackUpdate update);
}