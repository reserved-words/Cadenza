namespace Cadenza.API.Interfaces.LastFm;

public interface IInfoService
{
    Task<string> AlbumArtworkUrl(string artist, string title);
}
