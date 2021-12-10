﻿namespace Cadenza.Local.API;

public interface ILibraryService
{
    Task<ArtistFull> GetAlbumArtist(string artistId);
    Task<ICollection<Artist>> GetAlbumArtists();
    Task<TrackFull> GetTrack(string id);
    Task<TrackInfo> GetTrackSummary(string id);

    Task<ICollection<ArtistInfo>> GetArtists();
    Task<ICollection<AlbumInfo>> GetAlbums(string artworkUrlFormat);
    Task<ICollection<TrackInfo>> GetTracks();
    Task<ICollection<AlbumTrackLink>> GetAlbumTrackLinks();

    Task<(byte[] Bytes, string Type)> GetArtwork(string id);
    Task<ICollection<string>> GetArtistTracks(string id);
    Task<ICollection<string>> GetAlbumTracks(string id);
}
