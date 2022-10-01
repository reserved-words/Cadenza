namespace Cadenza.Web.Common.Events;

public delegate Task ItemEventHandler(object sender, ItemEventArgs e);

public class ItemEventArgs : EventArgs
{
    public ViewItem Item { get; set; }
}
