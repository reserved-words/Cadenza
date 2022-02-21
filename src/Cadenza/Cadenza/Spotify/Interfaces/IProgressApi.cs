using Cadenza.Common;

namespace Cadenza.Source.Spotify.Player;

public interface IProgressApi
{
    Task<TrackProgress> GetProgress(string accessToken);
}
