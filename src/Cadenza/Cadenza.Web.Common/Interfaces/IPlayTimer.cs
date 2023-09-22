namespace Cadenza.Web.Common.Interfaces;

public interface IPlayTimer
{
    void OnPlay(int totalSeconds);
    void OnPause();
    void OnResume();
    void OnStop();
}