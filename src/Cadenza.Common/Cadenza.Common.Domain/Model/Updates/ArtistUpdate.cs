using Cadenza.Common.Domain.Model.Library;

namespace Cadenza.Common.Domain.Model.Updates;

public class ArtistUpdate : ItemUpdate<ArtistDetails>
{
    public ArtistUpdate()
        : base() { }

    public ArtistUpdate(ArtistDetails artist)
        : base(LibraryItemType.Artist, artist.Id, artist) { }
}
