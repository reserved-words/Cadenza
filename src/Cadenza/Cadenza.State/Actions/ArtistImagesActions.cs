using Cadenza.Common.Domain.Model.Library;

namespace Cadenza.State.Actions;

public record FetchArtistImageRequest(ArtistDetails Artist);
public record FetchArtistImageResultAction(int ArtistId, string Result);