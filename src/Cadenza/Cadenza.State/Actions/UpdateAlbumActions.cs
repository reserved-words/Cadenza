namespace Cadenza.State.Actions;

public record AlbumUpdateRequest(int AlbumId, AlbumUpdateVM Update);
public record AlbumUpdatedAction(int AlbumId, AlbumUpdateVM Update);
public record AlbumUpdateFailedAction(int AlbumId);