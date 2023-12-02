using Cadenza.Web.Api.Interfaces;
using Cadenza.Web.Api.Settings;

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

    public async Task UpdateAlbumTracks(int albumId, IReadOnlyCollection<AlbumDiscVM> originalTracks, IReadOnlyCollection<AlbumDiscVM> updatedTracks)
    {
        var dto = new UpdateAlbumTracksDTO
        {
            AlbumId = albumId,
            OriginalTracks = _mapper.MapAlbumTracks(originalTracks),
            UpdatedTracks = _mapper.MapAlbumTracks(updatedTracks)
        };
        await _http.Delete(_settings.Endpoints.UpdateAlbumTracks, dto);
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
