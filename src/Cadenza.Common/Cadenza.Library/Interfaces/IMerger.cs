﻿namespace Cadenza.Library;

public interface IMerger
{
    void MergeArtist(ArtistInfo artist, ArtistInfo update, MergeMode mode);
    void MergeAlbum(AlbumInfo album, AlbumInfo update, MergeMode mode);
    void MergeTrack(TrackInfo newTrack, TrackInfo existingTrack, MergeMode mode);
    void MergeAlbumTrackLink(AlbumTrackLink existing, AlbumTrackLink update, MergeMode mode);

    void MergeTrackArtist(TrackLinks trackLinks, string artistId);
    void MergeAlbumArtist(AlbumLinks albumLinks, string artistId);
    void MergeArtistTrack(ArtistLinks artistLinks, string trackId);
    void MergeArtistAlbum(ArtistLinks artistLinks, string albumId);
    void MergeTrackAlbum(TrackLinks trackLinks, string albumId, AlbumTrackPosition position);
    void MergeAlbumTrack(AlbumLinks albumLinks, string trackId, AlbumTrackPosition position);
}
