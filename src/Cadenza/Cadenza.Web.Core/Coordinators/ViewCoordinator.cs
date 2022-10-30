using Cadenza.Web.Common.Interfaces.View;

namespace Cadenza.Web.Core.Coordinators;

internal class ViewCoordinator : IViewCoordinator
{
    private readonly IMessenger _messageSender;

    public ViewCoordinator(IMessenger messageSender)
    {
        _messageSender = messageSender;
    }

    public async Task RequestItem(ViewItem item)
    {
        await _messageSender.Send(this, new ViewItemEventArgs { Item = item });
    }
}