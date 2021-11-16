namespace Cadenza.Source.Local;

public class LocalLibraryUpdater : ISourceLibraryUpdater, IFileUpdateQueue
{
    private readonly ILocalApiConfig _apiConfig;
    private readonly IHttpClient _httpClient;

    public LocalLibraryUpdater(IHttpClient httpClient, ILocalApiConfig apiConfig)
    {
        _httpClient = httpClient;
        _apiConfig = apiConfig;
    }

    public async Task<bool> UpdateAlbum(AlbumUpdate album)
    {
        var response = await _httpClient.Post(_apiConfig.UpdateAlbumUrl, null, album);
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> RemoveQueuedUpdate(MetaDataUpdate update)
    {
        var response = await _httpClient.Delete(_apiConfig.UnqueueUrl, null, update);
        return response.IsSuccessStatusCode;
    }

    public async Task<FileUpdateQueue> GetQueuedUpdates()
    {
        var response = await _httpClient.Get(_apiConfig.QueuedUpdatesUrl);
        return await response.Content.ReadFromJsonAsync<FileUpdateQueue>();
    }

    public async Task<bool> UpdateArtist(ArtistUpdate artist)
    {
        var response = await _httpClient.Post(_apiConfig.UpdateArtistUrl, null, artist);
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> UpdateTrack(TrackUpdate track)
    {
        var response = await _httpClient.Post(_apiConfig.UpdateTrackUrl, null, track);
        return response.IsSuccessStatusCode;
    }
}