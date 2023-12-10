namespace Cadenza.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UpdateController : ControllerBase
{
    private readonly ILibraryRepository _libraryRepository;
    private readonly IUpdateRepository _updateRepository;
    private readonly IUpdateService _service;

    public UpdateController(IUpdateService service, IUpdateRepository updateRepository, ILibraryRepository libraryRepository)
    {
        _service = service;
        _updateRepository = updateRepository;
        _libraryRepository = libraryRepository;
    }

    [HttpGet("GetAlbum/{id}")]
    public async Task<AlbumForUpdateDTO> GetAlbum(int id)
    {
        // TODO: Should really get the DTOs directly from database instead of mapping to one DTO then another

        var album = await _libraryRepository.GetAlbum(id);

        return new AlbumForUpdateDTO
        {
            AlbumId = album.Id,
            ArtistName = album.ArtistName,
            Title = album.Title,
            ReleaseType = album.ReleaseType,
            Year = album.Year,
            DiscCount = album.DiscCount,
            Tags = album.Tags,
        };
    }

    [HttpGet("GetAlbumTracks/{id}")]
    public async Task<List<AlbumTrackForUpdateDTO>> GetAlbumTracks(int id)
    {
        // TODO: Should really get the DTOs directly from database instead of mapping to one DTO then another

        var discs = await _libraryRepository.GetAlbumTracks(id);

        var tracks = new List<AlbumTrackForUpdateDTO>();

        foreach (var disc in discs)
        {
            tracks.AddRange(disc.Tracks.Select(t => new AlbumTrackForUpdateDTO
            {
                TrackId = t.TrackId,
                IdFromSource = t.IdFromSource,
                Title = t.Title,
                ArtistName = t.ArtistName,
                TrackNo = t.TrackNo,
                DiscNo = disc.DiscNo,
                DiscTrackCount = disc.TrackCount
            }));
        }

        return tracks;
    }

    [HttpPost("UpdateTrack")]
    public async Task UpdateTrack([FromBody] UpdatedTrackPropertiesDTO request)
    {
        await _service.UpdateTrack(request);
    }

    [HttpPost("UpdateArtist")]
    public async Task UpdateArtist([FromBody] UpdatedArtistPropertiesDTO request)
    {
        await _service.UpdateArtist(request);
    }

    [HttpPost("UpdateAlbum")]
    public async Task UpdateAlbum([FromBody] AlbumUpdateDTO request)
    {
        await _service.UpdateAlbum(request);
    }

    [HttpPost("LoveTrack")]
    public async Task LoveTrack([FromBody] UpdateLovedTrackDTO request)
    {
        var username = HttpContext.GetUsername();
        await _updateRepository.LoveTrack(username, request.TrackId);
    }

    [HttpPost("UnloveTrack")]
    public async Task UnloveTrack([FromBody] UpdateLovedTrackDTO request)
    {
        var username = HttpContext.GetUsername();
        await _updateRepository.UnloveTrack(username, request.TrackId);
    }
}