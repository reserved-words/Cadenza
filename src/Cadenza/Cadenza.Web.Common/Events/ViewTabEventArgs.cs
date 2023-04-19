namespace Cadenza.Web.Common.Events;

public class ViewTabEventArgs : EventArgs
{
    public ViewTabEventArgs(Tab tab)
    {
        Tab = tab;
    }

    public Tab Tab { get; private set; }
}