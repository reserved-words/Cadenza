using Cadenza.Domain.Models.Album;

namespace Cadenza.Web.Common.Interfaces;

public interface IArtworkFetcher
{
    Task<string> GetArtworkUrl(Album album, string trackId = null);
}