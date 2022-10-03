namespace Cadenza.Web.Common.Interfaces;

public interface IMessenger
{
    void Subscribe<T>(Func<object, T, Task> handler) where T : EventArgs;
    void Subscribe<T>(Func<object, T, Task> handler, out Guid id) where T : EventArgs;
    void Unsubscribe<T>(Guid id) where T : EventArgs;
    Task Send<T>(object sender, T message) where T : EventArgs;
}