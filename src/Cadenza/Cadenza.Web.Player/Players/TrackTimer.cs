using Cadenza.State.Actions;
using Cadenza.State.Store;
using Fluxor;
using System.Timers;
using Timer = System.Timers.Timer;

namespace Cadenza.Web.Player.Players;

internal class TrackTimer : ITrackTimerController
{
    private readonly IDispatcher _dispatcher;
    private readonly IMessenger _messenger;
    private readonly IState<CurrentTrackState> _currentTrackState;

    public TrackTimer(IMessenger messenger, IDispatcher dispatcher, IState<CurrentTrackState> currentTrackState)
    {
        _messenger = messenger;
        _dispatcher = dispatcher;
        _currentTrackState = currentTrackState;
    }

    private CurrentTrackTimer _current;

    public void OnPlay(int totalSeconds)
    {
        _current = new CurrentTrackTimer(totalSeconds, OnTrackProgressed);
        UpdateProgress(_current.TotalSeconds, 0);
        _current.Start(0);
    }

    public void OnPause(int secondsPlayed)
    {
        _current.Stop();
    }

    public void OnResume(int secondsPlayed)
    {
        _current.Start(secondsPlayed);
    }

    public void OnStop(int secondsPlayed)
    {
        _current.Stop();
    }

    private void OnTrackProgressed(object sender, ElapsedEventArgs e)
    {
        UpdateProgress(_current.TotalSeconds, _current.ProgressSeconds);
    }

    private async void UpdateProgress(int totalSeconds, int progressSeconds)
    {
        if (progressSeconds > totalSeconds)
        {
            _dispatcher.Dispatch(new TrackEndedAction(_currentTrackState.Value.IsLastInPlaylist));
        }
        else
        {
            await _messenger.Send(this, new TrackProgressedEventArgs(totalSeconds, progressSeconds));
        }
    }

    internal class CurrentTrackTimer : IDisposable
    {
        private const int TickFrequencySeconds = 1;

        private readonly Timer _timer;
        private readonly ElapsedEventHandler _handler;
        
        public int TotalSeconds { get; }
        public int ProgressSeconds { get; private set; }

        public CurrentTrackTimer(int totalSeconds, ElapsedEventHandler handler)
        {
            _handler = handler;
            _timer = new Timer(TickFrequencySeconds * 1000);
            TotalSeconds = totalSeconds;
        }

        public void Start(int progressSeconds)
        {
            ProgressSeconds = progressSeconds;
            _timer.Elapsed += OnElapsed;
            _timer.Start();
        }

        public void Stop()
        {
            _timer.Stop();
            _timer.Elapsed -= OnElapsed;
        }

        private void OnElapsed(object sender, ElapsedEventArgs e)
        {
            ProgressSeconds += TickFrequencySeconds;
            _handler(sender, e);
        }

        public void Dispose() => _timer.Dispose();
    }
}
