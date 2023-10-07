using System.Collections.ObjectModel;

namespace Cadenza.Web.Components.Forms.Artist;

public class EditArtistBase : FormBase<ArtistDetailsVM>
{
    [Inject] public IState<GroupingsState> GroupingsState { get; set; }
    [Inject] public IDispatcher Dispatcher { get; set; }

    protected IReadOnlyCollection<GroupingVM> Groupings => GroupingsState.Value.Groupings;
    protected EditableArtist EditableItem { get; set; }

    protected override void OnParametersSet()
    {
        EditableItem = new EditableArtist
        {
            Id = Model.Id,
            Name = Model.Name,
            Grouping = Model.Grouping,
            Genre = Model.Genre,
            Country = Model.Country,
            State = Model.State,
            City = Model.City,
            ImageBase64 = Model.ImageBase64,
            Tags = Model.Tags.ToList()
        };
    }

    protected override async Task OnInitializedAsync()
    {
        SubscribeToAction<ArtistUpdatedAction>(OnArtistUpdated);
        await base.OnInitializedAsync();
    }
    protected void OnSubmit()
    {
        var updatedArtist = Model with
        {
            Grouping = EditableItem.Grouping,
            Genre = EditableItem.Genre,
            ImageBase64 = EditableItem.ImageBase64,
            Country = EditableItem.Country,
            State = EditableItem.State,
            City = EditableItem.City,
            Tags = new ReadOnlyCollection<string>(EditableItem.Tags.ToList())
        };

        Dispatcher.Dispatch(new ArtistUpdateRequest(Model, updatedArtist));
    }

    private void OnArtistUpdated(ArtistUpdatedAction action)
    {
        Submit();
    }

    protected void OnCancel()
    {
        Cancel();
    }
}
