namespace Cadenza.Core.Player;

public interface ITrackTimerController
{
    void OnPlay(int totalSeconds);
    void OnPause(int secondsPlayed);
    void OnResume(int secondsPlayed);
    void OnStop(int secondsPlayed);
}