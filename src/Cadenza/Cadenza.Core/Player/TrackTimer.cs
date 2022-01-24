using System.Timers;
using Timer = System.Timers.Timer;

namespace Cadenza.Core;

public class TrackTimer : ITrackTimerController, ITrackProgressedConsumer, ITrackFinishedConsumer
{
    public event TrackFinishedEventHandler TrackFinished;
    public event TrackProgressedEventHandler TrackProgressed;

    private readonly Timer _trackProgressTimer = new Timer();

    private int _trackProgressSeconds;
    private int _trackTotalSeconds;

    public TrackTimer()
    {
        _trackProgressTimer.Interval = 1000;
        _trackProgressTimer.AutoReset = true;
        _trackProgressTimer.Elapsed += OnTrackProgressed;
    }

    public void OnPlay()
    {
        StopTimer(0);
        RaiseTrackProgressed();
        _trackProgressTimer.Start();
    }

    public void OnPause(int secondsPlayed)
    {
        StopTimer(secondsPlayed);
        RaiseTrackProgressed();
    }

    public void OnResume(int secondsPlayed)
    {
        StopTimer(secondsPlayed);
        RaiseTrackProgressed();
        _trackProgressTimer.Start();
    }

    public void OnStop(int secondsPlayed)
    {
        StopTimer(0);
        RaiseTrackProgressed();
    }

    public void OnSetTrack(int totalSeconds)
    {
        _trackTotalSeconds = totalSeconds;
    }

    private void OnTrackProgressed(object sender, ElapsedEventArgs e)
    {
        if (_trackTotalSeconds == 0)
            return;

        _trackProgressSeconds++;

        if (_trackProgressSeconds >= _trackTotalSeconds)
        {
            RaiseTrackFinished();
        }
        else
        {
            RaiseTrackProgressed();
        }
    }

    private void RaiseTrackFinished()
    {
        TrackFinished?.Invoke(this, new TrackFinishedEventArgs());
    }

    private void RaiseTrackProgressed()
    {
        TrackProgressed?.Invoke(this, new TrackProgressedEventArgs(_trackTotalSeconds, _trackProgressSeconds));
    }

    private void StopTimer(int secondsPlayed)
    {
        _trackProgressTimer.Stop();
        _trackProgressSeconds = secondsPlayed;
    }
}
