namespace Cadenza.Common.DTO;

public class AlbumUpdateDTO : ItemUpdateDTO<AlbumDetailsDTO>
{
    public AlbumUpdateDTO()
        : base() { }

    public AlbumUpdateDTO(AlbumDetailsDTO album)
        : base(LibraryItemType.Album, album.Id, album) { }
}