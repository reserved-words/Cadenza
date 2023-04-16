using Cadenza.Web.Common.Interfaces.Searchbar;

namespace Cadenza.Web.Core.Coordinators;

internal class SearchCoordinator : ISearchCoordinator, ISearchCache
{
    private readonly IMessenger _messenger;
    private readonly ISearchSyncService _syncService;


    public SearchCoordinator(IMessenger messenger, ISearchSyncService syncService)
    {
        _messenger = messenger;
        _syncService = syncService;

        SubscribeToUpdateEvent<ArtistUpdatedEventArgs>();
        SubscribeToUpdateEvent<AlbumUpdatedEventArgs>();
        SubscribeToUpdateEvent<TrackUpdatedEventArgs>();
        SubscribeToUpdateEvent<TrackRemovedEventArgs>();
    }

    public List<PlayerItem> Items { get; set; } = new();

    private void SubscribeToUpdateEvent<T>() where T : EventArgs
    {
        // For now repopulate all items whenever anything is updated
        // Could narrow this down to only repopulating the relevant items
        _messenger.Subscribe<T>(async (s, e) => await Populate());
    }

    public async Task Populate()
    {
        await _messenger.Send(this, new SearchUpdateStartedEventArgs());

        Items.Clear();
        Items = await _syncService.GetSearchItems();

        await _messenger.Send(this, new SearchUpdateCompletedEventArgs());
    }
}

