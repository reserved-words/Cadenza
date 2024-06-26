﻿using System.Text.Json;
using IStore = Fluxor.IStore;

namespace Cadenza.Web.Actions.Middleware;
public class LoggingMiddleware : Fluxor.Middleware
{
    private IStore Store;

    public override Task InitializeAsync(IDispatcher dispatcher, IStore store)
    {
        Store = store;
        Console.WriteLine(nameof(InitializeAsync));
        return Task.CompletedTask;
    }

    public override void AfterInitializeAllMiddlewares()
    {
        Console.WriteLine(nameof(AfterInitializeAllMiddlewares));
    }

    public override bool MayDispatchAction(object action)
    {
        Console.WriteLine(nameof(MayDispatchAction) + ObjectInfo(action));
        return true;
    }

    public override void BeforeDispatch(object action)
    {
        Console.WriteLine(nameof(BeforeDispatch) + ObjectInfo(action));
    }

    public override void AfterDispatch(object action)
    {
        Console.WriteLine(nameof(AfterDispatch) + ObjectInfo(action));
        Console.WriteLine();
    }

    private string ObjectInfo(object obj) => ": " + obj.GetType().Name + " " + JsonSerializer.Serialize(obj);
}
