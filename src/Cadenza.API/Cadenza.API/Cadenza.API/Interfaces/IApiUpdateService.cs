namespace Cadenza.API.Interfaces;

public interface IApiUpdateService
{
    Task UpdateArtist(ArtistUpdate update);
}
