using Cadenza.Common.Domain.Model.Artist;

namespace Cadenza.State.Actions;

public record FetchViewGenreRequest(string Genre);

public record FetchViewGenreResult(string Genre, List<Artist> Artists);
