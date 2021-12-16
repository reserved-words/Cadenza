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

    public async Task<TrackFull> GetTrack(string id)
    {
        var url = Url(_apiConfig.TrackUrl, id);
        var response = await _httpClient.Get(url);
        return await response.Content.ReadFromJsonAsync<TrackFull>();
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
            album.ImageUrl = $"{_apiConfig.BaseUrl}{album.ImageUrl}";
        }

        return albums;
    }

    async Task<PlayingTrack> ISourceRepository.GetTrack(string id)
    {
        var response = await _httpClient.Get(Url(_apiConfig.TrackUrl, id));
        return await response.Content.ReadFromJsonAsync<PlayingTrack>();
    }

    public async Task<ICollection<AlbumTrackLink>> GetAlbumTrackLinks()
    {
        var response = await _httpClient.Get(_apiConfig.AlbumTrackLinksUrl);
        return await response.Content.ReadFromJsonAsync<List<AlbumTrackLink>>();
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