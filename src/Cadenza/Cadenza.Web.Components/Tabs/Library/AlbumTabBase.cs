﻿namespace Cadenza.Web.Components.Tabs.Library;

public class AlbumTabBase : FluxorComponent
{
    [Inject] public IState<ViewAlbumState> ViewAlbumState { get; set; }

    public bool Loading => ViewAlbumState.Value.IsLoading;
    public AlbumDetailsVM Album => ViewAlbumState.Value.Album;
    public IReadOnlyCollection<AlbumDiscVM> Discs => ViewAlbumState.Value.Tracks;

    public int DurationSeconds => Discs.SelectMany(d => d.Tracks).Sum(t => t.DurationSeconds);
}
