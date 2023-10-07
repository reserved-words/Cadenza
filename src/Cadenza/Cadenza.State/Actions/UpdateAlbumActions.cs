namespace Cadenza.State.Actions;

public record AlbumUpdateRequest(int AlbumId, AlbumDetailsVM OriginalAlbum, EditableAlbum Update);
public record AlbumUpdatedAction(int AlbumId, AlbumDetailsVM UpdatedAlbum);
public record AlbumUpdateFailedAction(int AlbumId);