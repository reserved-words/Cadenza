namespace Cadenza.Web.Common.Interfaces;

public interface IArtworkFetcher
{
    string GetArtistImageSrc(int? artistId);
    string GetAlbumArtworkSrc(int? albumId);
}
