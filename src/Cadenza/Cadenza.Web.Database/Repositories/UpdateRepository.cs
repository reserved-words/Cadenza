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

    public async Task UpdateAlbum(AlbumDetailsVM originalAlbum, AlbumDetailsVM updatedAlbum)
    {
        var dto = _mapper.MapUpdate(originalAlbum, updatedAlbum);
        await _http.Post(_settings.Endpoints.UpdateAlbum, dto);
    }

    public async Task UpdateArtist(ArtistDetailsVM originalArtist, ArtistDetailsVM updatedArtist)
    {
        var dto = _mapper.MapUpdate(originalArtist, updatedArtist);
        await _http.Post(_settings.Endpoints.UpdateArtist, dto);
    }

    public async Task UpdateTrack(TrackDetailsVM originalTrack, TrackDetailsVM updatedTrack)
    {
        var dto = _mapper.MapUpdate(originalTrack, updatedTrack);
        await _http.Post(_settings.Endpoints.UpdateTrack, dto);
    }
}
