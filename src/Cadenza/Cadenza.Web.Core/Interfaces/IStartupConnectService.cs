using Cadenza.Web.Common.Tasks;

namespace Cadenza.Web.Core.Interfaces
{
    public interface IStartupConnectService
    {
        TaskGroup GetStartupTasks();
    }
}
