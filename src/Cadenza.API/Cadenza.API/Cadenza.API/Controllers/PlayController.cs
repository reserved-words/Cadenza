using Cadenza.Common;

namespace Cadenza.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PlayController : ControllerBase
{
    private readonly IPlayRepository _repository;
    private readonly IShuffler _shuffler;

    public PlayController(IPlayRepository repository, IShuffler shuffler)
    {
        _repository = repository;
        _shuffler = shuffler;
    }

    [HttpGet("Album/{id}")]
    public async Task<PlaylistDTO> Album(int id)
    {
        var title = await _repository.GetAlbumName(id);
        var tracks = await _repository.PlayAlbum(id);

        return new PlaylistDTO
        {
            Id = id.ToString(),
            Title = title,
            TrackIds = tracks
        };
    }

    [HttpGet("Artist/{id}")]
    public async Task<PlaylistDTO> Artist(int id)
    {
        var title = await _repository.GetArtistName(id);
        var tracks = await _repository.PlayArtist(id);
        _shuffler.Shuffle(tracks);

        return new PlaylistDTO
        {
            Id = id.ToString(),
            Title = title,
            TrackIds = tracks
        };
    }

    [HttpGet("Genre/{genre}")]
    public async Task<PlaylistDTO> Genre(string genre)
    {
        var split = genre.SplitGenreId();
        var title = await _repository.GetGenreName(split.Grouping, split.Genre);
        var tracks = await _repository.PlayGenre(split.Grouping, split.Genre);
        _shuffler.Shuffle(tracks);

        return new PlaylistDTO
        {
            Id = genre,
            Title = title,
            TrackIds = tracks
        };
    }

    [HttpGet("Grouping/{grouping}")]
    public async Task<PlaylistDTO> Grouping(string grouping)
    {
        var tracks = await _repository.PlayGrouping(grouping);
        _shuffler.Shuffle(tracks);

        return new PlaylistDTO
        {
            Id = grouping,
            Title = grouping,
            TrackIds = tracks
        };
    }

    [HttpGet("Tag/{id}")]
    public async Task<PlaylistDTO> Tag(string id)
    {
        var tracks = await _repository.PlayTag(id);
        _shuffler.Shuffle(tracks);

        return new PlaylistDTO
        {
            Id = id,
            Title = id,
            TrackIds = tracks
        };
    }

    [HttpGet("Track/{id}")]
    public async Task<PlaylistDTO> Track(int id)
    {
        var title = await _repository.GetTrackName(id);

        return new PlaylistDTO
        {
            Id = id.ToString(),
            Title = title,
            TrackIds = [ id ]
        };
    }

    [HttpGet("Tracks")]
    public async Task<PlaylistDTO> Tracks()
    {
        var tracks = await _repository.PlayAll();
        _shuffler.Shuffle(tracks);

        return new PlaylistDTO
        {
            Id = "",
            Title = "All Library",
            TrackIds = tracks
        };
    }
}
