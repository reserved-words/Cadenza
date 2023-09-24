using Cadenza.Web.Common.Tasks;

namespace Cadenza.State.Interfaces;

public interface IConnectionState
{
    string Title { get; }
    string Message { get; }
    TaskState State { get; }
}
