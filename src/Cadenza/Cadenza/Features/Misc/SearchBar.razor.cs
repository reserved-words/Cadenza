﻿namespace Cadenza.Features.Misc;

public partial class SearchBar
{
    [Inject] public IState<ViewState> ViewState { get; set; }
    [Inject] public IDispatcher Dispatcher { get; set; }
    [Inject] public IState<SearchItemsState> SearchItemsState { get; set; }

    protected bool IsLoading => SearchItemsState.Value.IsLoading;
    protected bool Disabled => ViewState.Value.IsNavigationDisabled;

    private SearchItemVM _result;

    protected SearchItemVM Result
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

    protected Task<IEnumerable<SearchItemVM>> Search(string value)
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
