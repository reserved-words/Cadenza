namespace Cadenza.Source.Local;

public class LocalLibrary : ISourceRepository
{
    private readonly ILocalApiConfig _apiConfig;
    private readonly IHttpClient _httpClient;

    public LocalLibrary(IHttpClient httpClient, ILocalApiConfig apiConfig)
    {
        _httpClient = httpClient;
        _apiConfig = apiConfig;
    }

    public async Task<List<string>> GetAllTracks()
    {
        var response = await _httpClient.Get(_apiConfig.AllTracksUrl);
        var tracks = await response.Content.ReadFromJsonAsync<string[]>();
        return tracks.ToList();
    }

    private static string Url(string format, string text)
    {
        return string.Format(format, HttpUtility.UrlEncode(text));
    }

    public async Task<ICollection<ArtistInfo>> GetArtists()
    {
        var response = await _httpClient.Get(_apiConfig.ArtistsUrl);
        return await response.Content.ReadFromJsonAsync<List<ArtistInfo>>();
    }

    public async Task<ICollection<AlbumInfo>> GetAlbums()
    {
        var response = await _httpClient.Get(_apiConfig.AlbumsUrl);
        var albums = await response.Content.ReadFromJsonAsync<List<AlbumInfo>>();

        foreach (var album in albums)
        {
            album.ArtworkUrl = $"{_apiConfig.BaseUrl}{album.ArtworkUrl}";
        }

        return albums;
    }

    async Task<PlayingTrack> ISourceRepository.GetTrack(string id)
    {
        var response = await _httpClient.Get(Url(_apiConfig.TrackUrl, id));
        var track = await response.Content.ReadFromJsonAsync<PlayingTrack>();
        track.ArtworkUrl = $"{_apiConfig.BaseUrl}{track.ArtworkUrl}";
        return track;
    }

    async Task<FullTrack> ISourceRepository.GetFullTrack(string id)
    {
        var response = await _httpClient.Get(Url(_apiConfig.FullTrackUrl, id));
        var track = await response.Content.ReadFromJsonAsync<FullTrack>();
        track.ArtworkUrl = $"{_apiConfig.BaseUrl}{track.ArtworkUrl}";
        return track;
    }

    public async Task<List<string>> GetArtistTracks(string id)
    {
        var response = await _httpClient.Get(Url(_apiConfig.ArtistTracksUrl, id));
        var tracks = await response.Content.ReadFromJsonAsync<string[]>();
        return tracks.ToList();
    }

    public async Task<List<string>> GetAlbumTracks(string artistId, string albumId)
    {
        var response = await _httpClient.Get(Url(_apiConfig.AlbumTracksUrl, albumId));
        var tracks = await response.Content.ReadFromJsonAsync<string[]>();
        return tracks.ToList();
    }
}