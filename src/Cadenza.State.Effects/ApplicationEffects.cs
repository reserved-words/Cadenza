namespace Cadenza.State.Effects;

public class ApplicationEffects
{
    private readonly IStartupDialogService _dialogService;

    public ApplicationEffects(IStartupDialogService dialogService)
    {
        _dialogService = dialogService;
    }


    [EffectMethod(typeof(ApplicationStartRequest))]
    public async Task HandleApplicationStartRequest(IDispatcher dispatcher)
    {
        var success = await _dialogService.Run();
        dispatcher.Dispatch(new ApplicationStartedAction(success));
    }
}
