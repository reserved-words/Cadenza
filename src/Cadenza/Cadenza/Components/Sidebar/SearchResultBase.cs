﻿using Cadenza.Common;
using Cadenza.Utilities;
using Cadenza.Database;

namespace Cadenza.Components.Sidebar;

public class SearchResultBase : ComponentBase
{
    [Inject]
    public INotificationService Notifications { get; set; }

    [Inject]
    public IPlaylistCreator PlaylistCreator { get; set; }

    [Inject]
    public IPlaylistPlayer Player { get; set; }

    [Inject]
    public IIdGenerator IdGenerator { get; set; }

    [Inject]
    public IAppController App { get; set; }

    [Inject]
    public NavigationManager NavigationManager { get; set; }

    [Parameter]
    public SourceSearchableItem Result { get; set; }

    [Parameter]
    public LibrarySource? ResultSource { get; set; }

    public bool DisplayArtistLink => Result != null && Result.Type != SearchableItemType.Playlist;
    public bool DisplayAlbumLink => Result != null && Result.Type != SearchableItemType.Artist;
    public bool DisplayTrackLink => Result != null && Result.Type == SearchableItemType.Track;

    protected async Task OnPlay()
    {
        try
        {
            await Play();
        }
        catch (ConnectorException ex)
        {
            // await App.DisableConnector(ex.Connector, ex.Error, ex.Message);
        }
    }

    private async Task Play()
    {
        if (Result.Type == SearchableItemType.Artist)
        {
            await Player.PlayArtist(Result.Id);
        }
        else if (Result.Type == SearchableItemType.Album)
        {
            await Player.PlayAlbum(ResultSource.Value, Result.Id);
        }
        else if (Result.Type == SearchableItemType.Playlist)
        {
            await Player.PlayPlaylist(Result.Id);
        }
        else if (Result.Type == SearchableItemType.Track)
        {
            await Player.PlayTrack(ResultSource.Value, Result.Id);
        }
    }

    protected void OnViewTrack()
    {
        if (Result.Type != SearchableItemType.Track)
        {
            return;
        }

        var url = $"/Track/{Result.Id}";
        NavigationManager.NavigateTo(url);
    }

    protected void OnViewAlbum()
    {
        if (Result.Type != SearchableItemType.Artist || Result.Type == SearchableItemType.Playlist)
        {
            return;
        }

        var albumId = Result.Type == SearchableItemType.Album
            ? Result.Id
            : IdGenerator.GenerateId(Result.Artist, Result.Album);

        var url = $"/Album/{albumId}";
        
        NavigationManager.NavigateTo(url);
    }

    protected void OnViewArtist()
    {
        var artistId = IdGenerator.GenerateId(Result.Artist);
        var url = $"/Artist/{artistId}";
        NavigationManager.NavigateTo(url);
    }

    protected static Dictionary<SearchableItemType, string> Icons = new Dictionary<SearchableItemType, string>
    {
        { SearchableItemType.Artist, MudBlazor.Icons.Material.Filled.PeopleAlt },
        { SearchableItemType.Album, MudBlazor.Icons.Material.Filled.Album },
        { SearchableItemType.Playlist, MudBlazor.Icons.Material.Filled.QueueMusic },
        { SearchableItemType.Track, MudBlazor.Icons.Material.Filled.MusicNote }
    };
}

