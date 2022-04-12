﻿using Cadenza.Components.Shared.Dialogs;
using Cadenza.Interfaces;
using IDialogService = Cadenza.Interfaces.IDialogService;

namespace Cadenza.Components.Tabs.Spotify;

public class SpotifySearchBase : ComponentBase
{
    [Inject]
    public ISpotifySearcher Library { get; set; }

    [Inject]
    public IDialogService DialogService { get; set; }

    public bool Searching { get; set; }

    public string SearchText { get; set; }

    public List<SpotifyArtist> ArtistSearchResults { get; set; }

    public async Task OnSearch()
    {
        if (string.IsNullOrEmpty(SearchText))
        {
            ArtistSearchResults = null;
            return;
        }

        Searching = true;
        ArtistSearchResults = await Library.SearchArtist(SearchText);
        Searching = false;
    }

    public async Task OnViewArtist(SpotifyArtist artist)
    {
        var albums = await Library.GetArtistAlbums(artist.Id);
        var playlists = await Library.GetArtistPlaylists(artist.Name);

        var artistProfile = new SpotifyArtistProfile
        {
            Artist = artist,
            Albums = albums,
            Playlists = playlists
        };

        await DialogService.Display<SpotifyArtistView, SpotifyArtistProfile>(artistProfile, $"Spotify Artist - {artist.Name}");
    }
}
