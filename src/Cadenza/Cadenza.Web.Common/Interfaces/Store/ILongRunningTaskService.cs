namespace Cadenza.Web.Common.Interfaces.Store;

public interface ILongRunningTaskService
{
    Task RunTasks(List<SubTask> tasks);
}
