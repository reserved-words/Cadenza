namespace Cadenza.Web.Player.Interfaces;

internal interface IPlayer
{
    Task Play(PlayTrack track);
    Task Pause();
    Task Resume();
    Task Stop();
}
