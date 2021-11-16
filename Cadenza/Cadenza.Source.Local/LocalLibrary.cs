namespace Cadenza.Source.Local;

public class LocalLibrary : ILibrary
{
    private readonly ILocalApiConfig _apiConfig;
    private readonly IHttpClient _httpClient;

    public LocalLibrary(IHttpClient httpClient, ILocalApiConfig apiConfig)
    {
        _httpClient = httpClient;
        _apiConfig = apiConfig;
    }

    public async Task<ICollection<Artist>> GetAlbumArtists()
    {
        var response = await _httpClient.Get(_apiConfig.AlbumArtistsUrl);
        return await response.Content.ReadFromJsonAsync<List<Artist>>();
    }

    public async Task<ArtistFull> GetAlbumArtist(string id)
    {
        var url = Url(_apiConfig.ArtistUrl, id);
        var response = await _httpClient.Get(url);

        if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            return null;

        var artist = await response.Content.ReadFromJsonAsync<ArtistFull>();
        return artist;
    }

    public async Task<TrackSummary> GetTrackSummary(string id)
    {
        var url = Url(_apiConfig.TrackSummaryUrl, id);
        var response = await _httpClient.Get(url);
        return await response.Content.ReadFromJsonAsync<TrackSummary>();
    }

    public async Task<TrackFull> GetTrack(string id)
    {
        var url = Url(_apiConfig.TrackUrl, id);
        var response = await _httpClient.Get(url);
        return await response.Content.ReadFromJsonAsync<TrackFull>();
    }

    public async Task<ICollection<Track>> GetAllTracks()
    {
        var response = await _httpClient.Get(_apiConfig.PlaylistAllUrl);
        return await response.Content.ReadFromJsonAsync<List<Track>>();
    }

    private static string Url(string format, string text)
    {
        return string.Format(format, HttpUtility.UrlEncode(text));
    }
}