using Cadenza.Domain;

namespace Cadenza.API.Common.Repositories;

public interface IMusicRepository
{
    Task<FullLibrary> Get();
    Task UpdateArtist(ArtistInfo artist);
    Task UpdateAlbum(AlbumInfo album);
    Task UpdateTrack(TrackInfo track);
}