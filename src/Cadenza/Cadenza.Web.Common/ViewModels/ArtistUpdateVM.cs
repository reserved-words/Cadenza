namespace Cadenza.Web.Common.ViewModels;

public class ArtistUpdateVM : ItemUpdateVM<ArtistDetailsVM>
{
    public ArtistUpdateVM()
        : base() { }

    public ArtistUpdateVM(ArtistDetailsVM artist)
        : base(LibraryItemType.Artist, artist.Id, artist) { }
}
