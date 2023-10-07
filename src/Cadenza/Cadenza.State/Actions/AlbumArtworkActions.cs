namespace Cadenza.State.Actions;

public record FetchAlbumArtworkRequest(AlbumVM Album);
public record FetchAlbumArtworkResultAction(int AlbumId, string Result);