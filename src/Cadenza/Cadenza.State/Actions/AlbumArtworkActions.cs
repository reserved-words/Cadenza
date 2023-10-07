namespace Cadenza.State.Actions;

public record FetchAlbumArtworkRequest(int Id, string ArtworkBase64);
public record FetchAlbumArtworkResultAction(int AlbumId, string Result);