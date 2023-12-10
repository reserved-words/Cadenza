namespace Cadenza.Web.Common.ViewModel;

public record UpdateAlbumVM
{
    public int Id { get; init; }
    public string ArtistName { get; init; }
    public string Title { get; init; }
    public ReleaseType ReleaseType { get; init; }
    public string Year { get; init; }
    public string ArtworkBase64 { get; init; }
    public int DiscCount { get; set; }
    public IReadOnlyCollection<string> Tags { get; init; }
}