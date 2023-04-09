namespace Cadenza.Web.Common.Interfaces;

public interface IArtworkFetcher
{
    string GetArtistImageSrc(ArtistInfo artist);
    string GetAlbumArtworkSrc(Album album);
}
