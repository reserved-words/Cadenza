﻿namespace Cadenza.SyncService.Interfaces;

internal interface IDatabaseRepository
{
    Task AddTrack(LibrarySource source, SyncTrack track);
    Task<List<string>> GetAllTracks(LibrarySource source);
    Task<List<SyncTrackRemovalRequest>> GetRemovalRequests(LibrarySource source);
    Task MarkRemovalErrored(SyncTrackRemovalRequest request);
    Task MarkRemovalDone(SyncTrackRemovalRequest request);
    Task<List<string>> GetTracksByArtist(LibrarySource source, int artistId);
    Task<List<string>> GetTracksByAlbum(LibrarySource source, int albumId);
    Task<SyncSourceTrack> GetTrackIdFromSource(int trackId);
    Task<List<ItemUpdateRequest>> GetUpdateRequests(LibrarySource source);
    Task MarkUpdateErrored(LibrarySource source, ItemUpdateRequest request);
    Task MarkUpdateDone(LibrarySource source, ItemUpdateRequest request);
    Task RemoveTracks(LibrarySource source, List<string> idsFromSource);
}