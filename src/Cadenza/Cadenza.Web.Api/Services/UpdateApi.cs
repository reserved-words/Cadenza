namespace Cadenza.Web.Api.Services;

internal class UpdateApi : IUpdateApi
{
    private readonly ApiSettings _settings;
    private readonly IApiHttpHelper _http;
    private readonly IDataTransferObjectMapper _mapper;

    public UpdateApi(IApiHttpHelper http, IOptions<ApiSettings> settings, IDataTransferObjectMapper mapper)
    {
        _http = http;
        _settings = settings.Value;
        _mapper = mapper;
    }

    public async Task<UpdateAlbumVM> GetAlbum(int albumId)
    {
        var dto = await _http.Get<AlbumForUpdateDTO>(_settings.Endpoints.GetAlbumForUpdate, albumId);
        return _mapper.MapAlbum(dto);
    }

    public async Task<List<UpdateAlbumTrackVM>> GetAlbumTracks(int albumId)
    {
        var dto = await _http.Get<List<AlbumTrackForUpdateDTO>>(_settings.Endpoints.GetAlbumTracksForUpdate, albumId);
        return dto.Select(t => _mapper.MapTrack(t)).ToList();
    }

    public async Task UpdateAlbum(int albumId, UpdateAlbumVM updatedAlbum, IReadOnlyCollection<UpdateAlbumTrackVM> updatedTracks, IReadOnlyCollection<int> removedTracks)
    {
        var album = updatedAlbum == null ? null : _mapper.MapAlbum(updatedAlbum);
        var albumTracks = updatedTracks == null ? null : _mapper.MapAlbumTracks(updatedTracks);

        var dto = new AlbumUpdateDTO
        {
            AlbumId = albumId,
            UpdatedAlbum = album,
            UpdatedAlbumTracks = albumTracks,
            RemovedTracks = removedTracks.ToList()
        };

        await _http.Post(_settings.Endpoints.UpdateAlbum, dto);
    }

    public async Task UpdateArtist(ArtistDetailsVM updatedArtist)
    {
        var dto = _mapper.MapArtist(updatedArtist);
        await _http.Post(_settings.Endpoints.UpdateArtist, dto);
    }

    public async Task UpdateTrack(TrackDetailsVM updatedTrack)
    {
        var dto = _mapper.MapTrack(updatedTrack);
        await _http.Post(_settings.Endpoints.UpdateTrack, dto);
    }
}
