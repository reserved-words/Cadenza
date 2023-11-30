namespace Cadenza.Web.Model;

public record RecentAlbumVM(int Id, string Title, string ArtistName, string ImageUrl) 
{
    public override string ToString() => $"{Title} by {ArtistName}";
}