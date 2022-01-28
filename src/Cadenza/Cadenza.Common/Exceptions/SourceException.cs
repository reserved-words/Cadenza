using Cadenza.Domain;

namespace Cadenza.Common;

public class SourceException : Exception
{
    public SourceException(LibrarySource source, SourceError error, string message)
        : base(message)
    {
        Source = source;
        Error = error;
    }

    public LibrarySource Source { get; set; }
    public SourceError Error { get; }
}

public enum SourceError
{
    PlayFailure
}
