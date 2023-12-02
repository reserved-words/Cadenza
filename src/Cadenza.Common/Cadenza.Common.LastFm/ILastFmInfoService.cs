namespace Cadenza.Common.LastFm;

public interface ILastFmInfoService
{
    Task<string> AlbumArtworkUrl(string artist, string title);
}
