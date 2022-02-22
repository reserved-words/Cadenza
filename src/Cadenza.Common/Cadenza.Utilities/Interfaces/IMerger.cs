namespace Cadenza.Utilities;

public interface IMerger
{
    void MergeArtist(ArtistInfo existing, ArtistInfo update, MergeMode mode);
    void MergeAlbum(AlbumInfo existing, AlbumInfo update, MergeMode mode);
    void MergeTrack(TrackInfo existing, TrackInfo existingTrack, MergeMode mode);
    void MergeAlbumTrackLink(AlbumTrackLink existing, AlbumTrackLink update, MergeMode mode);
    void MergePlaylist(Playlist existing, Playlist update, MergeMode mode);
    void MergePlaylistTrackLink(PlaylistTrackLink existing, PlaylistTrackLink update, MergeMode mode);
}
