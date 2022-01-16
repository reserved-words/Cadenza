using Cadenza.Domain;
using Cadenza.Utilities;
using Microsoft.Extensions.Options;

namespace Cadenza.Source.Local;

public class LocalLibraryUpdater : ILibraryUpdater, IFileUpdateQueue
{
    private readonly IOptions<LocalApiSettings> _settings;
    private readonly IHttpHelper _httpClient;

    public LocalLibraryUpdater(IHttpHelper httpClient, IOptions<LocalApiSettings> settings)
    {
        _httpClient = httpClient;
        _settings = settings;
    }

    public async Task<bool> UpdateAlbum(AlbumInfo album, List<ItemPropertyUpdate> updates)
    {
        var response = await _httpClient.Post(_settings.GetApiEndpoint(e => e.UpdateAlbum), null, album);
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> RemoveQueuedUpdate(ItemPropertyUpdate update)
    {
        var response = await _httpClient.Delete(_settings.GetApiEndpoint(e => e.UnqueueUpdate), null, update);
        return response.IsSuccessStatusCode;
    }

    public async Task<FileUpdateQueue> GetQueuedUpdates()
    {
        var response = await _httpClient.Get(_settings.GetApiEndpoint(e => e.QueuedUpdates));
        return await response.Content.ReadFromJsonAsync<FileUpdateQueue>();
    }

    public async Task<bool> UpdateArtist(ArtistInfo artist, List<ItemPropertyUpdate> updates)
    {
        var response = await _httpClient.Post(_settings.GetApiEndpoint(e => e.UpdateArtist), null, artist);
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> UpdateTrack(TrackInfo track, List<ItemPropertyUpdate> updates)
    {
        var response = await _httpClient.Post(_settings.GetApiEndpoint(e => e.UpdateTrack), null, track);
        return response.IsSuccessStatusCode;
    }
}