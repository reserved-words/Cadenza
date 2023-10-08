namespace Cadenza.Web.State.Actions;

public record FetchViewGenreRequest(string Genre);

public record FetchViewGenreResult(string Genre, IReadOnlyCollection<ArtistVM> Artists);
