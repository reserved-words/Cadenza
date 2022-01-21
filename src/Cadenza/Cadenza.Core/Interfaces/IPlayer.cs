namespace Cadenza.Core;

public interface IPlayer
{
    Task Play(TrackSummary track);
    Task<int> Pause();
    Task<int> Resume();
    Task<int> Stop();
}