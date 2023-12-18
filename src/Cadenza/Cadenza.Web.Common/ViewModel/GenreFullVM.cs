namespace Cadenza.Web.Common.ViewModel;

public record class GenreFullVM(string Genre, GroupingVM Grouping, IReadOnlyCollection<ArtistVM> Artists);
