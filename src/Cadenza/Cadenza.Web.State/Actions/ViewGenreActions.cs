using Cadenza.Web.Common.ViewModel;

namespace Cadenza.Web.State.Actions;

public record FetchViewGenreRequest(string Genre, int GroupingId);

public record FetchViewGenreResult(GenreFullVM Genre);
