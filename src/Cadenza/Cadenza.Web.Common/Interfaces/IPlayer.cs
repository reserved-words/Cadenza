namespace Cadenza.Web.Common.Interfaces;
public interface IPlayer
{
    Task Play(Track track);
    Task Pause();
    Task Resume();
    Task Stop();
}