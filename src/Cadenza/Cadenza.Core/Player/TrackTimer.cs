using System.Timers;
using Timer = System.Timers.Timer;

namespace Cadenza.Core;

public class TrackTimer : ITrackTimerController, ITrackProgressedConsumer, ITrackFinishedConsumer
{
    public event TrackFinishedEventHandler TrackFinished;
    public event TrackProgressedEventHandler TrackProgressed;

    private readonly Timer _trackProgressTimer = new();

    private int _trackProgressSeconds;
    private int _trackTotalSeconds;

    public TrackTimer()
    {
        _trackProgressTimer.Interval = 1000;
        _trackProgressTimer.AutoReset = true;
    }

    public void OnPlay(int totalSeconds)
    {
        _trackProgressSeconds = 0;
        _trackTotalSeconds = totalSeconds;

        RaiseTrackProgressed();

        _trackProgressTimer.Elapsed += OnTrackProgressed;
        _trackProgressTimer.Start();
    }

    public void OnPause(int secondsPlayed)
    {
        _trackProgressTimer.Stop();
        _trackProgressSeconds = secondsPlayed;
    }

    public void OnResume(int secondsPlayed)
    {
        _trackProgressSeconds = secondsPlayed; 
        _trackProgressTimer.Start();
    }

    public void OnStop(int secondsPlayed)
    {
        _trackProgressTimer.Stop();
    }

    public void OnSetTrack(int totalSeconds)
    {
        _trackTotalSeconds = totalSeconds;
    }

    private void OnTrackProgressed(object sender, ElapsedEventArgs e)
    {
        if (_trackTotalSeconds == 0)
            return;

        if (_trackProgressSeconds > _trackTotalSeconds)
        {
            _trackProgressTimer.Elapsed -= OnTrackProgressed;
            RaiseTrackFinished();
        }
        else
        {
            _trackProgressSeconds++;
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
}
