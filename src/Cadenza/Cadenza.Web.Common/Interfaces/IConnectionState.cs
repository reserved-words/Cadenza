namespace Cadenza.Web.Common.Interfaces;

public interface IConnectionState
{
    string Title { get; }
    string Message { get; }
    TaskState State { get; }
}
