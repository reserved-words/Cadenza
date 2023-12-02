namespace Cadenza.Web.Common.ViewModel;

public record RecentAlbumVM(int Id, string Title, string ArtistName, string ImageUrl)
{
    public override string ToString() => $"{Title} by {ArtistName}";
}