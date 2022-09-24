using Cadenza.API.Common.Model;
using Cadenza.Domain.Models.Album;
using Cadenza.Domain.Models.Artist;
using Cadenza.Domain.Models.Track;

namespace Cadenza.API.Common.Repositories;

public interface IMusicRepository
{
    Task<FullLibrary> Get();
    Task UpdateArtist(ArtistInfo artist);
    Task UpdateAlbum(AlbumInfo album);
    Task UpdateTrack(TrackInfo track);
}