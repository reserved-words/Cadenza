namespace Cadenza.State.Actions;

public record AlbumUpdateRequest(AlbumDetailsVM OriginalAlbum, EditableAlbum Update);
public record AlbumUpdatedAction(AlbumDetailsVM UpdatedAlbum);
public record AlbumUpdateFailedAction(int AlbumId);