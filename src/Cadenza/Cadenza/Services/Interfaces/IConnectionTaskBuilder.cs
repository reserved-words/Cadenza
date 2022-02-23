using Cadenza.Core.Tasks;

namespace Cadenza
{
    public interface IConnectionTaskBuilder
    {
        SubTask GetConnectionTask();
    }
}
