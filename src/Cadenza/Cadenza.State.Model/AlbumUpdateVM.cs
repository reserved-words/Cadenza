namespace Cadenza.State.Model;

public record AlbumUpdateVM : ItemUpdateVM<AlbumDetailsVM>
{
    public AlbumUpdateVM(AlbumDetailsVM album)
        : base(LibraryItemType.Album, album.Id, album) { }
}