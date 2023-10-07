namespace Cadenza.Web.Database.Repositories;

internal class UpdateRepository : IUpdateRepository
{
    private readonly DatabaseApiSettings _settings;
    private readonly IApiHttpHelper _http;
    private readonly IDataTransferObjectMapper _mapper;

    public UpdateRepository(IApiHttpHelper http, IOptions<DatabaseApiSettings> settings, IDataTransferObjectMapper mapper)
    {
        _http = http;
        _settings = settings.Value;
        _mapper = mapper;
    }

    public async Task RemoveTrack(int trackId)
    {
        var data = new TrackRemovalRequestDTO
        {
            TrackId = trackId
        };
        await _http.Delete(_settings.Endpoints.RemoveTrack, data);
    }

    public async Task UpdateAlbum(AlbumUpdateVM update)
    {
        var dto = _mapper.Map(update);
        await _http.Post(_settings.Endpoints.UpdateAlbum, dto);
    }

    public async Task UpdateArtist(ArtistUpdateVM update)
    {
        var dto = _mapper.Map(update);
        await _http.Post(_settings.Endpoints.UpdateArtist, dto);
    }

    public async Task UpdateTrack(TrackUpdateVM update)
    {
        var dto = _mapper.Map(update);
        await _http.Post(_settings.Endpoints.UpdateTrack, dto);
    }
}
