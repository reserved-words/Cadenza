namespace Cadenza.Web.Core.Interfaces;

internal interface IUtilityPlayer
{
    Task OnPlay(TrackProgress progress);
    Task OnPause(TrackProgress progress);
    Task OnResume(TrackProgress progress);
    Task OnStop(TrackProgress progress);
}