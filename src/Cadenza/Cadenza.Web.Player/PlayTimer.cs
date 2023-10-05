using System.Timers;
using Timer = System.Timers.Timer;

namespace Cadenza.Web.Player;

internal class PlayTimer : IPlayTimer, IDisposable
{
    private Timer _timer = new(1000);

    private readonly IDispatcher _dispatcher;

    public PlayTimer(IDispatcher dispatcher)
    {
        _dispatcher = dispatcher;
        _timer.Elapsed += OnTimerElapsed;
    }

    public void OnPlay(int totalSeconds)
    {
        _dispatcher.Dispatch(new PlayProgressResetAction(totalSeconds));
        StartTimer();
    }

    public void OnPause()
    {
        StopTimer();
    }

    public void OnResume()
    {
        StartTimer();
    }

    public void OnStop()
    {
        StopTimer();
    }

    private void StartTimer()
    {
        _timer.Start();
    }

    private void StopTimer()
    {
        _timer.Stop();
    }

    private void OnTimerElapsed(object sender, ElapsedEventArgs e)
    {
        _dispatcher.Dispatch(new PlayProgressIncrementAction());
    }

    public void Dispose()
    {
        _timer.Dispose();
    }
}
