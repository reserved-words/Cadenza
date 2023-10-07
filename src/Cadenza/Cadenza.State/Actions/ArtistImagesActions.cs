namespace Cadenza.State.Actions;

public record FetchArtistImageRequest(int ArtistId, string ImageBase64);
public record FetchArtistImageResultAction(int ArtistId, string Result);