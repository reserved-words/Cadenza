namespace Cadenza.Web.Core.Coordinators;

internal class SearchCoordinator
{
    private readonly IMessenger _messenger;


    public SearchCoordinator(IMessenger messenger)
    {
        _messenger = messenger;

        // Leaving these events here so that build breaks when I remove these event args - 
        // Need to sort doing via state
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
        // _messenger.Subscribe<T>(async (s, e) => await Populate());
    }
}

