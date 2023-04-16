namespace Cadenza.Common.Domain.Model.Update;

public class AlbumUpdate : ItemUpdate<AlbumInfo>
{
    public AlbumUpdate()
        : base() { }

    public AlbumUpdate(AlbumInfo album)
        : base(LibraryItemType.Album, album.Id.ToString(), album) { }
}