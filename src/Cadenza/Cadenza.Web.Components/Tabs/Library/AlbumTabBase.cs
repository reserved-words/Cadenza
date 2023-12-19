﻿using Cadenza.Common;

namespace Cadenza.Web.Components.Tabs.Library;

public class AlbumTabBase : FluxorComponent
{
    [Inject] public IState<ViewAlbumState> ViewAlbumState { get; set; }

    public bool Loading => ViewAlbumState.Value.IsLoading;
    public AlbumFullVM Album => ViewAlbumState.Value.Album;

    protected Dictionary<int, int> DiscDurations => Album.Discs.ToDictionary(d => d.DiscNo, d => d.Tracks.Sum(t => t.DurationSeconds));

    protected List<LibraryBreadcrumb> Breadcrumbs => new List<LibraryBreadcrumb>
    {
        new LibraryBreadcrumb(PlayerItemType.Grouping, Album.Artist.Grouping, Album.Artist.Grouping),
        new LibraryBreadcrumb(PlayerItemType.Genre, (Album.Artist.Grouping, Album.Artist.Genre).GenreId(), Album.Artist.Genre),
        new LibraryBreadcrumb(PlayerItemType.Artist, Album.Artist.Id, Album.Artist.Name),
        new LibraryBreadcrumb(PlayerItemType.Album, Album.Album.Id, Album.Album.Title)
    };
}
