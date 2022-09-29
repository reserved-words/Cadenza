using Cadenza.Common.Domain.Model;

namespace Cadenza.Web.Core.Interfaces;

internal interface IPlayer
{
    Task<TrackProgress> Play(PlayTrack track);
    Task<TrackProgress> Pause();
    Task<TrackProgress> Resume();
    Task Stop();
}
