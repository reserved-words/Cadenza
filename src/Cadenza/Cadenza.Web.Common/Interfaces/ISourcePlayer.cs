using Cadenza.Common.Domain.Enums;

namespace Cadenza.Web.Common.Interfaces;

public interface ISourcePlayer : IAudioPlayer
{
    public LibrarySource Source { get; }
}
