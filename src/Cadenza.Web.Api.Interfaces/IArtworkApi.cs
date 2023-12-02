namespace Cadenza.Web.Api.Interfaces;

public interface IArtworkApi
{
    string GetArtistImageUrl(int? artistId);
    string GetAlbumArtworkUrl(int? albumId);
    Task<string> FindAlbumArtworkUrl(string artist, string title);
}
