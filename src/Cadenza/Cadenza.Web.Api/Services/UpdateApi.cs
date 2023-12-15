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

    public async Task UpdateAlbum(int albumId, AlbumDetailsVM updatedAlbum, IReadOnlyCollection<AlbumTrackVM> updatedTracks, IReadOnlyCollection<int> removedTracks)
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

    public async Task UpdateArtist(int artistId, ArtistDetailsVM updatedArtist, IReadOnlyCollection<AlbumVM> updatedReleases)
    {
        var artist = updatedArtist == null ? null : _mapper.MapArtist(updatedArtist);
        var releases = updatedReleases == null ? null : _mapper.MapArtistReleases(updatedReleases);

        var dto = new ArtistUpdateDTO
        {
            ArtistId = artistId,
            UpdatedArtist = artist,
            UpdatedArtistReleases = releases
        };

        await _http.Post(_settings.Endpoints.UpdateArtist, dto);
    }

    public async Task UpdateTrack(TrackDetailsVM updatedTrack)
    {
        var dto = _mapper.MapTrack(updatedTrack);
        await _http.Post(_settings.Endpoints.UpdateTrack, dto);
    }
}
