using Cadenza.Common;

namespace Cadenza
{
    public interface IStartupConnectService
    {
        TaskGroup GetStartupTasks();
    }
}
