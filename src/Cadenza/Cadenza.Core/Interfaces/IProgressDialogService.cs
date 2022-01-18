namespace Cadenza.Core;

public interface IProgressDialogService
{
    Task<bool> Run(TaskGroup tasks, bool autoStart, string startPromptText = null);
}
