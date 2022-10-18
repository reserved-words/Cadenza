using Cadenza.Common.Domain.Model.Results;

namespace Cadenza.API.Interfaces.Controllers;

public interface IWebInfoService
{
    Task<AlbumArtworkResult> AlbumArtworkUrl(string artist, string title);
    Task<ArtistImageResult> ArtistImageUrl(string name);
}