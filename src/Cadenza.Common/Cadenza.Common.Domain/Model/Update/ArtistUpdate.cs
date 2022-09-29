using Cadenza.Common.Domain.Enums;
using Cadenza.Common.Domain.Model.Artist;

namespace Cadenza.Common.Domain.Model.Update;

public class ArtistUpdate : ItemUpdate<ArtistInfo>
{
    public ArtistUpdate()
        : base() { }

    public ArtistUpdate(ArtistInfo artist)
        : base(LibraryItemType.Artist, artist.Id, artist.Name, artist) { }
}
