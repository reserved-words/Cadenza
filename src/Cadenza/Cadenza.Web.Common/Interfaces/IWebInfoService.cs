namespace Cadenza.Web.Common.Interfaces;

public interface IWebInfoService
{
    Task<string> GetAlbumArtworkUrl(string artist, string title);
}