using Cadenza.Core.Tasks;

namespace Cadenza
{
    public interface IStartupConnectService
    {
        TaskGroup GetStartupTasks();
    }
}
