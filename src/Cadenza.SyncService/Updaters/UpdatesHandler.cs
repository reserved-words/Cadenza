using Cadenza.Common.Domain.Enums;
using Cadenza.Common.Domain.Model.Updates;

namespace Cadenza.SyncService.Updaters;

internal class UpdatesHandler : IService
{
    private readonly IDatabaseRepository _database;
    private readonly IEnumerable<ISourceRepository> _sources;

    public UpdatesHandler(IDatabaseRepository database, IEnumerable<ISourceRepository> spurces)
    {
        _database = database;
        _sources = spurces;
    }

    public async Task Run()
    {
        foreach (var repository in _sources)
        {
            await ProcessUpdates(repository, repository.Source);
        }
    }

    private async Task ProcessUpdates(ISourceRepository repository, LibrarySource source)
    {
        var updates = await _database.GetUpdates(source);

        await ProcessTrackUpdates(repository, source, updates.Where(u => u.Type == LibraryItemType.Track));
        await ProcessAlbumUpdates(repository, source, updates.Where(u => u.Type == LibraryItemType.Album));
        await ProcessArtistUpdates(repository, source, updates.Where(u => u.Type == LibraryItemType.Artist));
    }

    private async Task ProcessArtistUpdates(ISourceRepository repository, LibrarySource source, IEnumerable<ItemUpdates> updates)
    {
        foreach (var update in updates)
        {
            var tracks = await _database.GetTracksByArtist(source, update.Id);
            await repository.UpdateTracks(tracks, update.Updates);
            await MarkUpdated(source, update);
        }
    }

    private async Task ProcessAlbumUpdates(ISourceRepository repository, LibrarySource source, IEnumerable<ItemUpdates> updates)
    {
        foreach (var update in updates)
        {
            var tracks = await _database.GetTracksByAlbum(source, update.Id);
            await repository.UpdateTracks(tracks, update.Updates);
            await MarkUpdated(source, update);
        }
    }

    private async Task ProcessTrackUpdates(ISourceRepository repository, LibrarySource source, IEnumerable<ItemUpdates> updates)
    {
        foreach (var update in updates)
        {
            await repository.UpdateTracks(new List<string> { update.Id }, update.Updates);
            await MarkUpdated(source, update);
        }
    }

    private async Task MarkUpdated(LibrarySource source, ItemUpdates update)
    {
        await _database.MarkUpdated(source, update);
    }
}