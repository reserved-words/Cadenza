namespace Cadenza.Player;

public interface IPlayer
{
    Task Play(PlaylistTrackViewModel track);
    Task<int> Pause();
    Task<int> Resume();
    Task<int> Stop();
}