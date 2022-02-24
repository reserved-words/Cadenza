using Cadenza.Core.App;

namespace Cadenza.Core.Interop;

public interface INavigation
{
    Task OpenNewTab(string url);
}
