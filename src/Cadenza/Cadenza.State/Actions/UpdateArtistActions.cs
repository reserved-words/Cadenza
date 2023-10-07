namespace Cadenza.State.Actions;

public record ArtistUpdateRequest(int ArtistId, ArtistDetailsVM OriginalArtist, EditableArtist Update);
public record ArtistUpdatedAction(int ArtistId, ArtistDetailsVM UpdatedArtist);
public record ArtistUpdateFailedAction(int ArtistId);