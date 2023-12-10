using Cadenza.Web.Common.ViewModel;

namespace Cadenza.Web.State.Actions;

public record AlbumUpdateRequest(int AlbumId, AlbumDetailsVM UpdatedAlbum, IReadOnlyCollection<AlbumTrackVM> UpdatedAlbumTracks, IReadOnlyCollection<int> RemovedTracks);
public record AlbumUpdatedAction(int AlbumId, AlbumDetailsVM UpdatedAlbum);
public record AlbumTracksUpdatedAction(int AlbumId, IReadOnlyCollection<AlbumTrackVM> UpdatedAlbumTracks, IReadOnlyCollection<int> RemovedTracks);
public record AlbumUpdateFailedAction(int AlbumId);