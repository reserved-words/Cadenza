using Cadenza.Domain;

namespace Cadenza.Common;

public interface ISourcePlayer : IAudioPlayer
{
    public LibrarySource Source { get; }
}
