namespace Cadenza.Core;

public interface IProgressDialogService
{
    Task<bool> Run(Func<TaskGroup> taskGroupFactory, string title, bool autoStart, string startPromptText = null);
}
