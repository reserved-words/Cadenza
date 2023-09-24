namespace Cadenza.State.Effects;

public class ApplicationEffects
{
    private readonly IProgressDialogService _dialogService;
    private readonly IStartupTaskService _connectService;

    public ApplicationEffects(IProgressDialogService dialogService, IStartupTaskService connectService)
    {
        _dialogService = dialogService;
        _connectService = connectService;
    }


    [EffectMethod]
    public async Task HandleApplicationStartRequest(ApplicationStartRequest action, IDispatcher dispatcher)
    {
        var success = await _dialogService.Run(() => _connectService.GetStartupTasks());
        dispatcher.Dispatch(new ApplicationStartedAction(success));
    }
}
