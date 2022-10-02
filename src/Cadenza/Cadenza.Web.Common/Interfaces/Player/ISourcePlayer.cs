namespace Cadenza.Web.Common.Interfaces.Player;

public interface ISourcePlayer : IAudioPlayer
{
    public LibrarySource Source { get; }
}
