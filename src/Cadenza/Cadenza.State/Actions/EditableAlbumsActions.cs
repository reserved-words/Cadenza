namespace Cadenza.State.Actions;

public record FetchEditableAlbumTracksRequest(int AlbumId);
public record FetchEditableAlbumTracksResultAction(IReadOnlyCollection<AlbumTrackVM> Tracks);
public record ResetEditableAlbumTracksRequest();