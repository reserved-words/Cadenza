using Cadenza.Core.Model;

namespace Cadenza.Source.Spotify.Interfaces;

public interface IProgressApi
{
    Task<TrackProgress> GetProgress(string accessToken);
}
