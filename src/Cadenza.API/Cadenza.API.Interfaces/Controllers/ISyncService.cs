﻿namespace Cadenza.API.Interfaces.Controllers;

public interface ISyncService
{
    Task AddTrack(LibrarySource source, TrackFull track);
    Task<List<string>> GetAllTracks(LibrarySource source);
    Task<List<string>> GetTracksByArtist(LibrarySource source, string artistId);
    Task<List<string>> GetTracksByAlbum(LibrarySource source, string albumId);
    Task<List<EditedItem>> GetUpdates(LibrarySource source);
    Task MarkUpdated(LibrarySource source, EditedItem update);
    Task RemoveTracks(LibrarySource source, List<string> ids);
}
