﻿namespace Cadenza.API.Database;

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

    public async Task UpdateAlbum(LibrarySource source, EditedItem updates)
    {
        await _dataAccess.UpdateAlbum(source, updates.Id, album =>
        {
            _itemUpdater.UpdateAlbum(album, updates.Properties);
        });
    }

    public async Task UpdateArtist(EditedItem updates)
    {
        await _dataAccess.UpdateArtist(updates.Id, artist =>
        {
            _itemUpdater.UpdateArtist(artist, updates.Properties);
        });
    }

    public async Task UpdateTrack(LibrarySource source, EditedItem updates)
    {
        await _dataAccess.UpdateTrack(source, updates.Id, track =>
        {
            _itemUpdater.UpdateTrack(track, updates.Properties);
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
}
