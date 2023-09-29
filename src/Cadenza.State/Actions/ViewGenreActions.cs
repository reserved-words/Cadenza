using Cadenza.Common.Domain.Model.Library;

namespace Cadenza.State.Actions;

public record FetchViewGenreRequest(string Genre);

public record FetchViewGenreResult(string Genre, List<Artist> Artists);
