namespace Cadenza.Library;

public interface ICacheReader
{
    ArtistInfo GetArtist(string artistId);
    TrackInfo GetTrack(string trackId);

    ArtistInfo GetTrackArtist(string trackId);
    AlbumInfo GetTrackAlbum(string trackId);

    AlbumTrackPosition GetAlbumPosition(string trackId);

    ICollection<AlbumInfo> GetArtistAlbums(string artistId);
    ICollection<AlbumTrack> GetAlbumTracks(string albumId);

    ICollection<ArtistInfo> GetAlbumArtists();
    ICollection<TrackInfo> GetAllTracks();
}