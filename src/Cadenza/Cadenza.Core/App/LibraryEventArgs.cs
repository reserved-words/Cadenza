namespace Cadenza.Core;

public delegate Task LibraryEventHandler(object sender, LibraryEventArgs e);

public class LibraryEventArgs : EventArgs
{
}
