namespace Cadenza.Web.Components.Forms.Album;

using IDialogService = Interfaces.IDialogService;

public class EditAlbumTracksBase : FormBase<AlbumTracksVM>
{
    [Inject] public IDispatcher Dispatcher { get; set; }
    [Inject] public IDialogService DialogService { get; set; }
    [Inject] public IState<CurrentTrackState> CurrentTrackState { get; set; }

    protected int DiscCount { get; set; }
    protected List<EditableAlbumDisc> EditableItems { get; set; }

    protected override void OnInitialized()
    {
        SubscribeToAction<AlbumTracksUpdatedAction>(OnAlbumTracksUpdated);
        base.OnInitialized();
    }

    protected override void OnParametersSet()
    {
        EditableItems = Model.Discs.Select(d => new EditableAlbumDisc
        {
            DiscNo = d.DiscNo,
            TrackCount = d.TrackCount,
            Tracks = d.Tracks.Select(t => new EditableAlbumTrack
            {
                TrackId = t.TrackId,
                TrackNo = t.TrackNo,
                Title = t.Title,
                DurationSeconds = t.DurationSeconds,
                ArtistId = t.ArtistId,
                ArtistName = t.ArtistName,
                IdFromSource = t.IdFromSource
            }).ToList()
        }).ToList();

        DiscCount = Model.DiscCount; // Todo: Populate from DB, currently will always be 0

        StateHasChanged();
    }

    private void OnAlbumTracksUpdated(AlbumTracksUpdatedAction action)
    {
        Submit();
    }

    protected void OnCancel()
    {
        Cancel();
    }

    protected void OnSubmit()
    {
        var updatedDiscs = EditableItems
            .Where(d => d.Tracks.Any())
            .Select(d => new AlbumDiscVM
            {
                DiscNo = d.DiscNo,
                TrackCount = d.TrackCount,
                Tracks = d.Tracks.Select(t => new AlbumTrackVM
                {
                    TrackId = t.TrackId,
                    ArtistId = t.ArtistId,
                    ArtistName = t.ArtistName,
                    IdFromSource = t.IdFromSource,
                    DurationSeconds = t.DurationSeconds,
                    TrackNo = t.TrackNo,
                    Title = t.Title
                }).ToList()
            }).ToList();

        // Todo: Save DiscCount

        Dispatcher.Dispatch(new AlbumTracksUpdateRequest(Model.AlbumId, Model.Discs, updatedDiscs));
    }

    protected void OnRemoveTrack(EditableAlbumTrack track)
    {
        var disc = EditableItems.Single(d => d.Tracks.Contains(track));
        disc.Tracks.Remove(track);

        if (!disc.Tracks.Any())
        {
            EditableItems.Remove(disc);
        }

        DiscCount = EditableItems.Count;
    }

    protected void MoveToDisc(EditableAlbumTrack track, int? discNo)
    {
        var originalDisc = EditableItems.Single(d => d.Tracks.Contains(track));
        originalDisc.Tracks.Remove(track);

        if (discNo.HasValue)
        {
            var newDisc = EditableItems.Single(d => d.DiscNo == discNo.Value);
            newDisc.Tracks.Add(track);
            newDisc.Tracks = newDisc.Tracks.OrderBy(t => t.TrackNo).ToList();

            var maxTrackNo = newDisc.Tracks.Max(t => t.TrackNo);
            if (maxTrackNo > newDisc.TrackCount)
            {
                newDisc.TrackCount = maxTrackNo;
            }
        }
        else
        {
            var newDisc = new EditableAlbumDisc
            {
                DiscNo = EditableItems.Max(d => d.DiscNo) + 1,
                TrackCount = track.TrackNo,
                Tracks = new List<EditableAlbumTrack>()
            };
            newDisc.Tracks.Add(track);
            EditableItems.Add(newDisc);
        }

        EditableItems.ForEach(i =>
        {
            if (!i.Tracks.Any())
            {
                EditableItems.Remove(i);
            }
        });

        DiscCount = EditableItems.Count;
    }
}
