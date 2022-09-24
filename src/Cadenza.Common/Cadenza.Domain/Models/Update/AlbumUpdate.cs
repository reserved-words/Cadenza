using Cadenza.Domain.Enums;
using Cadenza.Domain.Models.Album;

namespace Cadenza.Domain.Models.Update;

public class AlbumUpdate : ItemUpdate<AlbumInfo>
{
    public AlbumUpdate()
        : base() { }

    public AlbumUpdate(AlbumInfo album)
        : base(LibraryItemType.Album, album.Id, album.Title, album) { }
}