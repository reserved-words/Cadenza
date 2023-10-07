namespace Cadenza.Web.Components.Forms.Artist;

public class EditArtistBase : FormBase<ArtistDetailsVM>
{
    [Inject] public IState<GroupingsState> GroupingsState { get; set; }
    [Inject] public IDispatcher Dispatcher { get; set; }

    public IReadOnlyCollection<GroupingVM> Groupings => GroupingsState.Value.Groupings;
    public EditableArtist EditableItem => GetEditableItem();

    private EditableArtist GetEditableItem()
    {
        return new EditableArtist
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
       Dispatcher.Dispatch(new ArtistUpdateRequest(Model, EditableItem));
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
