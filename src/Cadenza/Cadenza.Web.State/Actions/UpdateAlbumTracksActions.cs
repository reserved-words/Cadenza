namespace Cadenza.Web.State.Actions;

public record AlbumTracksUpdateRequest(int AlbumId, IReadOnlyCollection<AlbumTrackVM> OriginalTracks, IReadOnlyCollection<AlbumTrackVM> UpdatedTracks);
public record AlbumTracksUpdatedAction(int AlbumId, IReadOnlyCollection<AlbumTrackVM> UpdatedTracks);
public record AlbumTracksUpdateFailedAction(int AlbumId);