namespace Cadenza.Library;

internal interface IComplexCacher
{
    void AddArtist(ArtistFull cached);
    void AddTrack(TrackFull cached);
    void AddAlbumArtists(ICollection<Artist> cached);
    void AddTracks(ICollection<Track> cached);
}