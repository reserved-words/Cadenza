namespace Cadenza.Local.API.Interfaces;

public interface IApiUpdateService
{
    Task UpdateArtist(ArtistUpdate update);
    Task<FileUpdateQueue> GetUpdates();
}
