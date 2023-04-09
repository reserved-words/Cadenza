﻿namespace Cadenza.Tabs.Library;

public class AlbumTabBase : ComponentBase
{
    [Inject]
    public IAlbumRepository Repository { get; set; }

    [Parameter]
    public string Id { get; set; }

    public AlbumInfo Album { get; set; }

    public List<Disc> Discs { get; set; } = new();

    protected override async Task OnParametersSetAsync()
    {
        await UpdateAlbum();
    }

    private async Task UpdateAlbum()
    {
        Album = await Repository.GetAlbum(Id);

        var tracks = await Repository.GetAlbumTracks(Id);

        Discs = tracks.GroupByDisc();

        StateHasChanged();
    }
}
