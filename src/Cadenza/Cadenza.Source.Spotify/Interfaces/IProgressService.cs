using Cadenza.Core.Model;

namespace Cadenza.Source.Spotify.Interfaces;

public interface IProgressService
{
    Task<TrackProgress> GetProgress();
}
