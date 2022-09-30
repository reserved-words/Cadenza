namespace Cadenza.Web.Common.Interfaces;

public interface ISourcePlayer : IAudioPlayer
{
    public LibrarySource Source { get; }
}
