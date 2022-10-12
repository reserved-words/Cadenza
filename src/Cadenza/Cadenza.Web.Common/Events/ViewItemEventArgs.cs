namespace Cadenza.Web.Common.Events;

public class ViewItemEventArgs : EventArgs
{
    public ViewItem Item { get; set; }
}

public class ViewTabEventArgs : EventArgs
{
    public ViewTabEventArgs(Tab tab)
    {
        Tab = tab;
    }

    public Tab Tab { get; private set; }
}