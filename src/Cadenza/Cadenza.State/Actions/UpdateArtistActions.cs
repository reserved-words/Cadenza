namespace Cadenza.State.Actions;

public record ArtistUpdateRequest(int ArtistId, ArtistUpdateVM Update);
public record ArtistUpdatedAction(int ArtistId, ArtistUpdateVM Update);
public record ArtistUpdateFailedAction(int ArtistId);