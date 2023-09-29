using Cadenza.State.Actions;
using Cadenza.State.Store;
using Fluxor;
using Fluxor.Blazor.Web.Components;

namespace Cadenza.Components.Sidebar;

public class SearchBase : FluxorComponent
{
    [Inject] public IDispatcher Dispatcher { get; set; }
    [Inject] public IState<SearchItemsState> SearchItemsState { get; set; }

    protected bool IsLoading => SearchItemsState.Value.IsLoading;

    private PlayerItem _result;

    protected PlayerItem Result
    {
        get { return _result; }
        set
        {
            _result = value;

            if (_result != null)
            {
                Dispatcher.Dispatch(new ViewItemRequest(Result.Type, Result.Id, Result.Name));
            }

            _result = null;
        }
    }

    protected Task<IEnumerable<PlayerItem>> Search(string value)
    {
        if (value.IsCommon())
            return null;

        var results = SearchItemsState.Value.Items
            .Where(x => x.Name != null && x.Name.Contains(value, StringComparison.InvariantCultureIgnoreCase))
            .OrderBy(x => x.Type)
            .ThenBy(x => x.Name)
            .AsEnumerable();

        return Task.FromResult(results);
    }
}

