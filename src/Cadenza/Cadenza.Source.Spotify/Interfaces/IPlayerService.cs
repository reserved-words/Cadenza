using Cadenza.Core.Model;

namespace Cadenza.Source.Spotify.Interfaces;

internal interface IPlayerService
{
    Task<TrackProgress> Pause();
    Task Play(string trackId);
    Task<TrackProgress> Resume();
    Task<TrackProgress> Stop();
}
