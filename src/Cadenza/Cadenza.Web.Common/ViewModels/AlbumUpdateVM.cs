namespace Cadenza.Web.Common.ViewModels;

public class AlbumUpdateVM : ItemUpdateVM<AlbumDetailsVM>
{
    public AlbumUpdateVM()
        : base() { }

    public AlbumUpdateVM(AlbumDetailsVM album)
        : base(LibraryItemType.Album, album.Id, album) { }
}