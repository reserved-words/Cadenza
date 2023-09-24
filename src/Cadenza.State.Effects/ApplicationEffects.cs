namespace Cadenza.State.Effects;

public class ApplicationEffects
{
    private readonly IStartupDialogService _dialogService;
    private readonly IStartupTaskService _connectService;

    public ApplicationEffects(IStartupDialogService dialogService, IStartupTaskService connectService)
    {
        _dialogService = dialogService;
        _connectService = connectService;
    }


    [EffectMethod]
    public async Task HandleApplicationStartRequest(ApplicationStartRequest action, IDispatcher dispatcher)
    {
        var tasks = _connectService.GetStartupTasks();
        var success = await _dialogService.Run(tasks);
        dispatcher.Dispatch(new ApplicationStartedAction(success));
    }
}
