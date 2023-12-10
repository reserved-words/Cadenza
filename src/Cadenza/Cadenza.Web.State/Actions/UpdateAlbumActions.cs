using Cadenza.Web.Common.ViewModel;

namespace Cadenza.Web.State.Actions;

public record AlbumUpdateRequest(int AlbumId, UpdateAlbumVM UpdatedAlbum, IReadOnlyCollection<UpdateAlbumTrackVM> UpdatedAlbumTracks, IReadOnlyCollection<int> RemovedTracks);
public record AlbumUpdatedAction(int AlbumId, UpdateAlbumVM UpdatedAlbum);
public record AlbumTracksUpdatedAction(int AlbumId, IReadOnlyCollection<UpdateAlbumTrackVM> UpdatedAlbumTracks, IReadOnlyCollection<int> RemovedTracks);
public record AlbumUpdateFailedAction(int AlbumId);