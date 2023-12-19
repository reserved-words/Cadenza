using Cadenza.Web.Common.ViewModel;

namespace Cadenza.Web.State.Actions;

public record FetchViewGenreRequest(string Id);

public record FetchViewGenreResult(GenreFullVM Genre);
