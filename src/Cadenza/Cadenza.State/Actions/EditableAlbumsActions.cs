using Cadenza.Common.Domain.Model.Library;

namespace Cadenza.State.Actions;

public record FetchEditableAlbumTracksRequest(int AlbumId);
public record FetchEditableAlbumTracksResultAction(List<AlbumTrack> Tracks);
public record ResetEditableAlbumTracksRequest();