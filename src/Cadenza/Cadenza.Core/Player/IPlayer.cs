using Cadenza.Core.Model;
using Cadenza.Domain.Models;

namespace Cadenza.Core.Player;

public interface IPlayer
{
    Task<TrackProgress> Play(PlayTrack track);
    Task<TrackProgress> Pause();
    Task<TrackProgress> Resume();
    Task Stop();
}
