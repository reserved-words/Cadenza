using Cadenza.Web.Common.Interfaces.Store;
using Cadenza.Web.Common.Interfaces.View;

namespace Cadenza.Web.Core.Coordinators;

internal class ViewCoordinator : IViewMessenger, IViewController
{
    private readonly IAppStore _store;
    private readonly ITrackRepository _repository;

    public ViewCoordinator(IAppStore appStore, ITrackRepository repository)
    {
        _store = appStore;
        _repository = repository;
    }

    public event ItemEventHandler ItemRequested;

    public async Task RequestItem(ViewItem item)
    {
        await ItemRequested?.Invoke(this, new ItemEventArgs { Item = item });
    }
}