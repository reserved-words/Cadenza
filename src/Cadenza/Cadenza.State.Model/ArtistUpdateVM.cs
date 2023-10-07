namespace Cadenza.State.Model;

public record ArtistUpdateVM : ItemUpdateVM<ArtistDetailsVM>
{
    public ArtistUpdateVM(ArtistDetailsVM artist)
        : base(LibraryItemType.Artist, artist.Id, artist) { }
}
