namespace Cadenza.Library;

public interface ICacheReader
{
    ArtistInfo GetArtist(string artistId);
    AlbumInfo GetAlbum(string albumId);
    TrackInfo GetTrack(string trackId);

    ArtistInfo GetTrackArtist(string trackId);
    AlbumInfo GetTrackAlbum(string trackId);
    ArtistInfo GetAlbumArtist(string albumId);

    AlbumTrackPosition GetAlbumPosition(string trackId);

    ICollection<AlbumInfo> GetArtistAlbums(string artistId);
    ICollection<TrackInfo> GetArtistTracks(string artistId);
    ICollection<AlbumTrack> GetAlbumTracks(string albumId);

    ICollection<ArtistInfo> GetAlbumArtists();
    ICollection<TrackInfo> GetAllTracks();
}