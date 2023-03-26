﻿using Cadenza.API.JsonLibrary.Interfaces;
using Cadenza.API.JsonLibrary.Interfaces.Updaters;

namespace Cadenza.API.JsonLibrary;

internal class MusicRepository : IMusicRepository
{
    private readonly IDataAccess _dataAccess;
    private readonly IItemUpdater _itemUpdater;
    private readonly ILibraryUpdater _libraryUpdater;

    public MusicRepository(IDataAccess dataAccess, ILibraryUpdater libraryUpdater, IItemUpdater itemUpdater)
    {
        _dataAccess = dataAccess;
        _libraryUpdater = libraryUpdater;
        _itemUpdater = itemUpdater;
    }

    public async Task<FullLibrary> Get(LibrarySource? source)
    {
        return await _dataAccess.GetAll(source);
    }

    public async Task UpdateAlbum(LibrarySource source, ItemUpdates updates)
    {
        await _dataAccess.UpdateAlbum(source, updates.Id, album =>
        {
            _itemUpdater.UpdateAlbum(album, updates.Updates);
        });
    }

    public async Task UpdateArtist(ItemUpdates updates)
    {
        await _dataAccess.UpdateArtist(updates.Id, artist =>
        {
            _itemUpdater.UpdateArtist(artist, updates.Updates);
        });
    }

    public async Task UpdateTrack(LibrarySource source, ItemUpdates updates)
    {
        await _dataAccess.UpdateTrack(source, updates.Id, track =>
        {
            _itemUpdater.UpdateTrack(track, updates.Updates);
        });
    }

    public async Task RemoveTracks(LibrarySource source, List<string> trackIds)
    {
        await _dataAccess.UpdateLibrary(source, library =>
        {
            _libraryUpdater.RemoveTracks(library, trackIds);
        });
    }

    public async Task AddTrack(LibrarySource source, TrackFull track)
    {
        await _dataAccess.UpdateLibrary(source, library =>
        {
            _libraryUpdater.AddTrack(library, track);
        });
    }

    public async Task<List<string>> GetAllTracks(LibrarySource source)
    {
        var library = await _dataAccess.GetAll(source);
        return library.Tracks.Where(t => t.Source == source).Select(t => t.Id).ToList();
    }
}
