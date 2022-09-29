using Cadenza.Web.Common.Tasks;

namespace Cadenza.Web.Common.Interfaces;

public interface IStartupConnectService
{
    TaskGroup GetStartupTasks();
}
