namespace Cadenza.Web.Core.Utilities;

public class Messenger : IMessenger
{
    private readonly Dictionary<Type, Dictionary<Guid, Func<object, EventArgs, Task>>> _handlers = new();

    public async Task Send<T>(object sender, T message) where T : EventArgs
    {
        if (!_handlers.TryGetValue(typeof(T), out var handlers))
            return;

        foreach (var handler in handlers.Values)
        {
            // TODO: Can these be done concurrently?
            await handler(sender, message);
        }
    }

    public void Subscribe<T>(Func<object, T, Task> handler) where T : EventArgs
    {
        Subscribe(handler, out Guid id);
    }

    public void Subscribe<T>(Func<object, T, Task> handler, out Guid id) where T : EventArgs
    {
        AddKey<T>();

        id = Guid.NewGuid();

        _handlers[typeof(T)].Add(id, (sender, args) => handler(sender, args as T));
    }

    public void Unsubscribe<T>(Guid id) where T : EventArgs
    {
        _handlers[typeof(T)].Remove(id);
    }

    private void AddKey<T>() where T : EventArgs
    {
        if (_handlers.ContainsKey(typeof(T)))
            return;

        _handlers.Add(typeof(T), new Dictionary<Guid, Func<object, EventArgs, Task>>());
    }
}

