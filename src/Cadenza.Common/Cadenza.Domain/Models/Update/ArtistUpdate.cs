using Cadenza.Domain.Enums;
using Cadenza.Domain.Models.Artist;

namespace Cadenza.Domain.Models.Update;

public class ArtistUpdate : ItemUpdate<ArtistInfo>
{
    public ArtistUpdate()
        : base() { }

    public ArtistUpdate(ArtistInfo artist)
        : base(LibraryItemType.Artist, artist.Id, artist.Name, artist) { }
}
