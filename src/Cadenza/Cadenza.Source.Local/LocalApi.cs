﻿using Microsoft.Extensions.Options;

namespace Cadenza.Source.Local;

public class LocalApi : ILibrary
{
    private readonly IOptions<LocalApiSettings> _settings;
    private readonly IHttpHelper _httpClient;

    public LocalApi(IHttpHelper httpClient, IOptions<LocalApiSettings> settings)
    {
        _httpClient = httpClient;
        _settings = settings;
    }

    public LibrarySource Source => LibrarySource.Local;

    public async Task<IEnumerable<BasicTrack>> GetAllTracks()
    {
        var response = await _httpClient.Get(GetApiEndpoint(e => e.AllTracks));
        return await response.Content.ReadFromJsonAsync<List<BasicTrack>>();
    }

    public async Task<IEnumerable<ArtistInfo>> GetArtists()
    {
        var response = await _httpClient.Get(GetApiEndpoint(e => e.Artists));
        return await response.Content.ReadFromJsonAsync<List<ArtistInfo>>();
    }

    public async Task<IEnumerable<AlbumInfo>> GetAlbums()
    {
        var response = await _httpClient.Get(GetApiEndpoint(e => e.Albums));
        var albums = await response.Content.ReadFromJsonAsync<List<AlbumInfo>>();

        foreach (var album in albums)
        {
            album.ArtworkUrl = GetArtworkUrl(album.ArtworkUrl);
        }

        return albums;
    }

    public async Task<TrackSummary> GetTrack(string id)
    {
        var response = await _httpClient.Get(GetApiEndpoint(e => e.Track, id));
        var track = await response.Content.ReadFromJsonAsync<TrackSummary>();
        track.ArtworkUrl = GetArtworkUrl(track.ArtworkUrl);
        return track;
    }

    public async Task<TrackFull> GetFullTrack(string id)
    {
        var response = await _httpClient.Get(GetApiEndpoint(e => e.FullTrack, id));
        var track = await response.Content.ReadFromJsonAsync<TrackFull>();
        track.ArtworkUrl = GetArtworkUrl(track.ArtworkUrl);
        return track;
    }

    private string GetArtworkUrl(string artworkUrl)
    {
        return $"{_settings.Value.BaseUrl}{artworkUrl}";
    }

    private string GetApiEndpoint(Func<LocalApiEndpoints, string> getEndpoint, string parameter = null)
    {
        return string.Format(_settings.GetApiEndpoint(getEndpoint), parameter);
    }
}