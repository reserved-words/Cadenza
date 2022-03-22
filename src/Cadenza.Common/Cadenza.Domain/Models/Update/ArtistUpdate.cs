namespace Cadenza.Domain;

public class ArtistUpdate : ItemUpdate<ArtistInfo>
{
    public ArtistUpdate()
        : base() { }

    public ArtistUpdate(ArtistInfo artist)
        : base(LibraryItemType.Artist, artist.Id, artist.Name, artist) { }
}
