﻿namespace Cadenza.Local.API;

public interface ILibraryService
{
    Task<FullTrack> GetTrack(string artworkUrlFormat, string id);
    Task<PlayingTrack> GetTrackSummary(string artworkUrlFormat, string id);

    Task<ICollection<ArtistInfo>> GetArtists();
    Task<ICollection<AlbumInfo>> GetAlbums(string artworkUrlFormat);
    Task<ICollection<TrackInfo>> GetTracks();
    Task<ICollection<AlbumTrackLink>> GetAlbumTrackLinks();

    Task<(byte[] Bytes, string Type)> GetArtwork(string id);

    Task<ICollection<string>> GetAllTracks();
    Task<ICollection<string>> GetArtistTracks(string id);
    Task<ICollection<string>> GetAlbumTracks(string id);
}
