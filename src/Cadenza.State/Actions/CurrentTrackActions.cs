using Cadenza.Common.Domain.Model;
using Cadenza.Common.Domain.Model.Track;

namespace Cadenza.State.Actions;

public record FetchFullTrackRequest(PlayTrack PlayTrack, bool IsLastTrackInPlaylist);

public record UpdateCurrentTrackAction(PlayTrack PlayTrack, TrackFull FullTrack, bool IsLastTrackInPlaylist);
