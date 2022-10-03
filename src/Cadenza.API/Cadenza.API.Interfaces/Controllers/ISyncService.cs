﻿namespace Cadenza.API.Interfaces.Controllers;

public interface ISyncService
{
    Task AddTrack(LibrarySource source, TrackFull track);
    Task<List<string>> GetAllTracks(LibrarySource source);
    Task<List<string>> GetTracksByArtist(LibrarySource source, string artistId);
    Task<List<string>> GetTracksByAlbum(LibrarySource source, string albumId);
    Task<List<ItemUpdates>> GetUpdates(LibrarySource source);
    Task MarkUpdated(LibrarySource source, ItemUpdates update);
    Task RemoveTracks(LibrarySource source, List<string> ids);
}