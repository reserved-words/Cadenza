using Cadenza.Domain.Enums;
using Cadenza.Domain.Models.Album;
using Cadenza.Domain.Models.Artist;
using Cadenza.Domain.Models.Track;

namespace Cadenza.Utilities;

public interface IMerger
{
    void MergeArtist(ArtistInfo artist, ArtistInfo update, MergeMode mode);
    void MergeAlbum(AlbumInfo album, AlbumInfo update, MergeMode mode);
    void MergeTrack(TrackInfo newTrack, TrackInfo existingTrack, MergeMode mode);
    void MergeAlbumTrackLink(AlbumTrackLink existing, AlbumTrackLink update, MergeMode mode);

    void MergeArtistTrack(ArtistLinks artistLinks, string trackId);
    void MergeArtistAlbum(ArtistLinks artistLinks, string albumId);
}
