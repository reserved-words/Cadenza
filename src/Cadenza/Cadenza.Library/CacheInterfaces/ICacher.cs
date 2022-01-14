using Cadenza.Domain;

namespace Cadenza.Library;

public interface ICacher
{
    void AddArtist(ArtistInfo artist, bool asAlbumArtist, bool forceUpdate);
    void AddAlbum(AlbumInfo album, bool forceUpdate);
    void AddTrack(TrackInfo track, bool forceUpdate);
    void AddAlbumTrack(AlbumTrackLink albumTrackLink);
}