using Cadenza.Web.Common.ViewModel;

namespace Cadenza.Web.State.Actions;

public record FetchTrackRequest(int TrackId, bool IsLastTrackInPlaylist);

public record UpdateCurrentTrackAction(int Id, TrackFullVM FullTrack, bool IsLastTrackInPlaylist);
