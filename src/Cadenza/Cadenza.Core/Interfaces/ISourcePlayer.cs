using Cadenza.Domain;

namespace Cadenza.Core.Interfaces;

public interface ISourcePlayer : IAudioPlayer
{
    public LibrarySource Source { get; }
}
