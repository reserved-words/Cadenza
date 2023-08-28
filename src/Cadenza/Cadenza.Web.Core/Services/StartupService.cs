using Cadenza.Web.Common.Interfaces.Startup;

namespace Cadenza.Web.Core.Services;

public class StartupService : IStartupService
{
    private readonly IProgressDialogService _dialogService;
    private readonly IStartupTaskService _connectService;

    public StartupService(IProgressDialogService dialogService, IStartupTaskService connectService, IMessenger messenger)
    {
        _dialogService = dialogService;
        _connectService = connectService;
    }

    public async Task<bool> Startup()
    {
        return await _dialogService.Run(() => _connectService.GetStartupTasks(), "Connecting Services", true);
    }
}
