namespace Cadenza.Web.Common.ViewModel;

public record class GenreFullVM(string Genre, string Grouping, IReadOnlyCollection<ArtistVM> Artists);
