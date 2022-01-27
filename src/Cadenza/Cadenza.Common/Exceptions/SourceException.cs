namespace Cadenza.Common;

public class SourceException : Exception
{
    public SourceException(SourceError error, string message)
        : base(message)
    {
        Error = error;
    }

    public SourceError Error { get; }
}

public enum SourceError
{
    PlayFailure
}
