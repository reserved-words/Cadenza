namespace Cadenza.Player;

public interface ITrackTimerController
{
    void OnPlay();
    void OnPause(int secondsPlayed);
    void OnResume(int secondsPlayed);
    void OnStop(int secondsPlayed);
    void OnSetTrack(int totalSeconds);
}