using Cadenza.Domain.Model.Album;

namespace Cadenza.Web.Common.Interfaces;

public interface IArtworkFetcher
{
    Task<string> GetArtworkUrl(Album album, string trackId = null);
}