namespace Cadenza.State.Actions;

public record FetchArtistImageRequest(ArtistDetailsVM Artist);
public record FetchArtistImageResultAction(int ArtistId, string Result);