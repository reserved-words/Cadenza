using Cadenza.Core.Model;

namespace Cadenza.Core;

public delegate Task LibraryEventHandler(object sender, LibraryEventArgs e);

public class LibraryEventArgs : EventArgs
{
}

public delegate Task ItemEventHandler(object sender, ItemEventArgs e);

public class ItemEventArgs : EventArgs
{
    public ViewItem Item { get; set; }
}
