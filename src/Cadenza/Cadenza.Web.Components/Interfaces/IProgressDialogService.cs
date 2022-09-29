using Cadenza.Web.Common.Tasks;

namespace Cadenza.Web.Components.Interfaces;

public interface IProgressDialogService
{
    Task<bool> Run(Func<TaskGroup> taskGroupFactory, string title, bool autoStart, string startPromptText = null);
}
