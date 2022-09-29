using Cadenza.Web.Common.Model;

namespace Cadenza.Web.Common.Events;

public delegate Task LibraryEventHandler(object sender, LibraryEventArgs e);

public class LibraryEventArgs : EventArgs
{
}