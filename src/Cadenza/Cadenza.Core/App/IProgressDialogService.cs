namespace Cadenza.Core;

public interface IProgressDialogService
{
    Task<bool> Run(Func<TaskGroup> taskGroupFactory, bool autoStart, string startPromptText = null);
}
