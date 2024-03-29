﻿namespace Cadenza.Database.Interfaces;

public interface ISyncRepository
{
    Task<List<string>> GetAllTracks(LibrarySource source);
    Task<List<string>> GetAlbumTrackSourceIds(int albumId);
    Task<List<string>> GetArtistTrackSourceIds(int artistId);
    Task<string> GetTrackIdFromSource(int trackId);
}
