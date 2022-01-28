namespace Cadenza.Core;

public delegate Task LibraryEventHandler(object sender, LibraryEventArgs e);

public class LibraryEventArgs : EventArgs
{
}

public delegate Task SourceEventHandler(object sender, SourceEventArgs e);

public class SourceEventArgs : EventArgs
{
    public SourceEventArgs(LibrarySource source, string error)
    {
        Source = source;
        Error = error;
    }

    public LibrarySource Source { get; set; }
    public string Error { get; set; }
}