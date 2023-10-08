using Cadenza.Web.Model;

namespace Cadenza.Web.State.Actions;

public record AlbumUpdateRequest(AlbumDetailsVM OriginalAlbum, AlbumDetailsVM UpdatedAlbum);
public record AlbumUpdatedAction(AlbumDetailsVM UpdatedAlbum);
public record AlbumUpdateFailedAction(int AlbumId);