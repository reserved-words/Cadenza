namespace Cadenza.Web.Api.Services;

internal class PlayApi : IPlayApi
{
    private readonly ApiEndpoints _settings;
    private readonly IApiHttpHelper _apiHelper;

    public PlayApi(IOptions<ApiSettings> settings, IApiHttpHelper apiHelper)
    {
        _settings = settings.Value.Endpoints;
        _apiHelper = apiHelper;
    }

    public async Task<PlaylistVM> PlayAll()
    {
        var playlist = await _apiHelper.Get<PlaylistDTO>(_settings.PlayTracks);
        return Map(playlist);
    }

    public async Task<PlaylistVM> PlayAlbum(int id)
    {
        var playlist = await _apiHelper.Get<PlaylistDTO>(_settings.PlayAlbum, id);
        return Map(playlist);
    }

    public async Task<PlaylistVM> PlayArtist(int id)
    {
        var playlist = await _apiHelper.Get<PlaylistDTO>(_settings.PlayArtist, id);
        return Map(playlist);
    }

    public async Task<PlaylistVM> PlayGenre(string genre)
    {
        var playlist = await _apiHelper.Get<PlaylistDTO>(_settings.PlayGenre, genre);
        return Map(playlist);
    }

    public async Task<PlaylistVM> PlayGrouping(string grouping)
    {
        var playlist = await _apiHelper.Get<PlaylistDTO>(_settings.PlayGrouping, grouping);
        return Map(playlist);
    }

    public async Task<PlaylistVM> PlayTag(string id)
    {
        var playlist = await _apiHelper.Get<PlaylistDTO>(_settings.PlayTag, id);
        return Map(playlist);
    }

    public async Task<PlaylistVM> PlayTrack(int id)
    {
        var playlist = await _apiHelper.Get<PlaylistDTO>(_settings.PlayTrack, id);
        return Map(playlist);
    }

    private PlaylistVM Map(PlaylistDTO playlist)
    {
        return new PlaylistVM(playlist.Id, playlist.Title, playlist.TrackIds);
    }
}
