using Cadenza.Web.Model;
using Cadenza.Web.State.Store;

namespace Cadenza.Web.Components.Main.Sidebar;

public class SearchBase : FluxorComponent
{
    [Inject] public IDispatcher Dispatcher { get; set; }
    [Inject] public IState<SearchItemsState> SearchItemsState { get; set; }

    protected bool IsLoading => SearchItemsState.Value.IsLoading;

    private PlayerItemVM _result;

    protected PlayerItemVM Result
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

    protected Task<IEnumerable<PlayerItemVM>> Search(string value)
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

