namespace Cadenza.Library;

internal interface IComplexCacher
{
    void AddArtist(ArtistFull cached);
    void AddAlbumArtists(ICollection<Artist> cached);
}