using Microsoft.Extensions.Options;

namespace Cadenza.Source.Local;

public class LocalLibrary : ISourceRepository
{
    private readonly IOptions<LocalApiSettings> _settings;
    private readonly IHttpClient _httpClient;

    public LocalLibrary(IHttpClient httpClient, IOptions<LocalApiSettings> settings)
    {
        _httpClient = httpClient;
        _settings = settings;
    }

    public async Task<List<string>> GetAllTracks()
    {
        var response = await _httpClient.Get(_settings.GetApiEndpoint(e => e.AllTracks));
        var tracks = await response.Content.ReadFromJsonAsync<string[]>();
        return tracks.ToList();
    }

    private static string Url(string format, string text)
    {
        return string.Format(format, HttpUtility.UrlEncode(text));
    }

    public async Task<ICollection<ArtistInfo>> GetArtists()
    {
        var response = await _httpClient.Get(_settings.GetApiEndpoint(e => e.Artists));
        return await response.Content.ReadFromJsonAsync<List<ArtistInfo>>();
    }

    public async Task<ICollection<AlbumInfo>> GetAlbums()
    {
        var response = await _httpClient.Get(_settings.GetApiEndpoint(e => e.Albums));
        var albums = await response.Content.ReadFromJsonAsync<List<AlbumInfo>>();

        foreach (var album in albums)
        {
            album.ArtworkUrl = GetArtworkUrl(album.ArtworkUrl);
        }

        return albums;
    }

    async Task<PlayingTrack> ISourceRepository.GetTrack(string id)
    {
        var response = await _httpClient.Get(Url(_settings.GetApiEndpoint(e => e.Track), id));
        var track = await response.Content.ReadFromJsonAsync<PlayingTrack>();
        track.ArtworkUrl = GetArtworkUrl(track.ArtworkUrl);
        return track;
    }

    async Task<FullTrack> ISourceRepository.GetFullTrack(string id)
    {
        var response = await _httpClient.Get(_settings.GetApiEndpoint(e => e.FullTrack), id);
        var track = await response.Content.ReadFromJsonAsync<FullTrack>();
        track.ArtworkUrl = GetArtworkUrl(track.ArtworkUrl);
        return track;
    }

    public async Task<List<string>> GetArtistTracks(string id)
    {
        var response = await _httpClient.Get(_settings.GetApiEndpoint(e => e.ArtistTracks), id);
        var tracks = await response.Content.ReadFromJsonAsync<string[]>();
        return tracks.ToList();
    }

    public async Task<List<string>> GetAlbumTracks(string artistId, string albumId)
    {
        var response = await _httpClient.Get(_settings.GetApiEndpoint(e => e.AlbumTracks), albumId);
        var tracks = await response.Content.ReadFromJsonAsync<string[]>();
        return tracks.ToList();
    }

    private string GetArtworkUrl(string artworkUrl)
    {
        return $"{_settings.Value.BaseUrl}{artworkUrl}";
    }
}