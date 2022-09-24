namespace Cadenza.Web.Common.Interfaces;

public interface IArtworkFetcher
{
    Task<string> GetAlbumArtwork(string id);
    Task<string> GetTrackArtwork(string id);
}