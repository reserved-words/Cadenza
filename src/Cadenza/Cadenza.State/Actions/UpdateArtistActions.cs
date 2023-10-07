namespace Cadenza.State.Actions;

public record ArtistUpdateRequest(ArtistDetailsVM OriginalArtist, EditableArtist Update);
public record ArtistUpdatedAction(ArtistDetailsVM UpdatedArtist);
public record ArtistUpdateFailedAction(int ArtistId);