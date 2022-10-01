namespace Cadenza.Web.Common.Interfaces;

public interface IPlayer
{
    Task Play(PlayTrack track);
    Task Pause();
    Task Resume();
    Task Stop();
}
