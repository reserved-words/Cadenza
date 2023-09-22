using Cadenza.Common.Domain.Model.Track;

namespace Cadenza.State.Actions;

public record FetchTrackRequest(int TrackId, bool IsLastTrackInPlaylist);

public record UpdateCurrentTrackAction(int Id, TrackFull FullTrack, bool IsLastTrackInPlaylist);
