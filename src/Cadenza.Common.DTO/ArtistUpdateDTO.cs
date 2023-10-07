namespace Cadenza.Common.DTO;

public class ArtistUpdateDTO : ItemUpdateDTO<ArtistDetailsDTO>
{
    public ArtistUpdateDTO()
        : base() { }

    public ArtistUpdateDTO(ArtistDetailsDTO artist)
        : base(LibraryItemType.Artist, artist.Id, artist) { }
}
