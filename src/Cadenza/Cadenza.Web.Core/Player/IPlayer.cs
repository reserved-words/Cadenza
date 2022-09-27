using Cadenza.Domain.Model;
using Cadenza.Web.Common.Model;

namespace Cadenza.Web.Core.Player;

public interface IPlayer
{
    Task<TrackProgress> Play(PlayTrack track);
    Task<TrackProgress> Pause();
    Task<TrackProgress> Resume();
    Task Stop();
}
