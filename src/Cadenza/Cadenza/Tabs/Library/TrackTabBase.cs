namespace Cadenza.Tabs.Library;

public class TrackTabBase : ComponentBase
{
    [Inject]
    public ITrackRepository Repository { get; set; }

    [Parameter]
    public int Id { get; set; }

    public TrackFull Model { get; set; } = new();

    protected override async Task OnParametersSetAsync()
    {
        await UpdateTrack();
    }

    private async Task UpdateTrack()
    {
        Model = Id == 0 
            ? null
            : await Repository.GetTrack(Id);

        StateHasChanged();
    }
}
