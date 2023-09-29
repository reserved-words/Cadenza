using Cadenza.Common.Domain.Model.Library;

namespace Cadenza.Web.Common.Interfaces;

public interface IArtworkFetcher
{
    string GetArtistImageSrc(ArtistDetails artist);
    string GetAlbumArtworkSrc(Album album);
}
