namespace Cadenza.Web.Common.Interfaces.Store;

public interface ILongRunningTaskService
{
    Task RunTasks(TaskGroup taskGroup);
}
