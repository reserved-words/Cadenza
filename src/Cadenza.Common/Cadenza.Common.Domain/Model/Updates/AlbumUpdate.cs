using Cadenza.Common.Domain.Model.Library;

namespace Cadenza.Common.Domain.Model.Updates;

public class AlbumUpdate : ItemUpdate<AlbumDetails>
{
    public AlbumUpdate()
        : base() { }

    public AlbumUpdate(AlbumDetails album)
        : base(LibraryItemType.Album, album.Id, album) { }
}