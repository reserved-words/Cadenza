using Cadenza.Core.Model;

namespace Cadenza.Source.Spotify.Interfaces;

internal interface IProgressService
{
    Task<TrackProgress> GetProgress();
}
