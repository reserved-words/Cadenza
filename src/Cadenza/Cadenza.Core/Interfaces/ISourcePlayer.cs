using Cadenza.Domain.Enums;

namespace Cadenza.Core.Interfaces;

public interface ISourcePlayer : IAudioPlayer
{
    public LibrarySource Source { get; }
}
