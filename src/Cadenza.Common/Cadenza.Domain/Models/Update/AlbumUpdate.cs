namespace Cadenza.Domain;

public class AlbumUpdate : ItemUpdate<AlbumInfo>
{
    public AlbumUpdate()
        : base() { }

    public AlbumUpdate(AlbumInfo album)
        : base(LibraryItemType.Album, album.Id, album.Title, album) { }
}