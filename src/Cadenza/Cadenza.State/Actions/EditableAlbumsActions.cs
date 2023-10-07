namespace Cadenza.State.Actions;

public record FetchEditableAlbumTracksRequest(int AlbumId);
public record FetchEditableAlbumTracksResultAction(List<AlbumTrackVM> Tracks);
public record ResetEditableAlbumTracksRequest();