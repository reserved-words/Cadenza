namespace Cadenza.Local.API;

public interface IPlaylistService
{
    Task<ICollection<Track>> All();
}