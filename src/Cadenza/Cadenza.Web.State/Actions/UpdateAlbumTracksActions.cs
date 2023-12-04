using Cadenza.Web.Common.ViewModel;

namespace Cadenza.Web.State.Actions;

public record AlbumTracksUpdateRequest(int AlbumId, IReadOnlyCollection<AlbumDiscVM> OriginalTracks, IReadOnlyCollection<AlbumDiscVM> UpdatedTracks);
public record AlbumTracksUpdatedAction(int AlbumId, IReadOnlyCollection<AlbumDiscVM> UpdatedTracks);
public record AlbumTracksUpdateFailedAction(int AlbumId);