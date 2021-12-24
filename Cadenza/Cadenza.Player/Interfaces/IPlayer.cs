namespace Cadenza.Player;

public interface IPlayer
{
    Task Play(PlayingTrack track);
    Task<int> Pause();
    Task<int> Resume();
    Task<int> Stop();
}