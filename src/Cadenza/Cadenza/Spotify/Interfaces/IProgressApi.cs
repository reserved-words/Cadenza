using Cadenza.Core.Model;

namespace Cadenza.Source.Spotify.Player;

public interface IProgressApi
{
    Task<TrackProgress> GetProgress(string accessToken);
}
