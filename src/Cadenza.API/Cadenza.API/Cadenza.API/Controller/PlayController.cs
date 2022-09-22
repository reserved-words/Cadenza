using Cadenza.API.Common.Interfaces.Cache;
using Microsoft.AspNetCore.Mvc;

namespace Cadenza.API.Controller;

[Route("api/[controller]")]
[ApiController]
public class PlayController : ControllerBase
{
    private readonly IPlayTrackCache _playTracks;

    public PlayController(IPlayTrackCache playTracks)
    {
        _playTracks = playTracks;
    }

    //app.MapGet("/Play/Tracks", () => playTracks.GetAll());
    //    app.MapGet("/Play/Artist", (string id) => playTracks.GetByArtist(id));
    //    app.MapGet("/Play/Album", (string id) => playTracks.GetByAlbum(id));
    //    app.MapGet("/Play/Genre", (string id) => playTracks.GetByGenre(id));
    //    app.MapGet("/Play/Grouping", (Grouping id) => playTracks.GetByGrouping(id));

}
