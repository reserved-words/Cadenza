using Cadenza.Library;
using Cadenza.Local.API.Interfaces;

namespace Cadenza.Local.API;

public static class Routing
{
    public static WebApplication AddRoutes(this WebApplication app)
    {
        app.MapGet("/Connect", () => "Connected");

        return app
            .AddLibraryRoutes()
            .AddPlayerRoutes()
            .AddUpdaterRoutes();
    }

    private static WebApplication AddUpdaterRoutes(this WebApplication app)
    {
        var apiService = app.Services.GetRequiredService<IApiUpdateService>();
        app.MapGet("/Update/Get", () => apiService.GetUpdates());
        app.MapPost("/Update/Album", (AlbumUpdate update) => apiService.UpdateAlbum(update));
        app.MapPost("/Update/Artist", (ArtistUpdate update) => apiService.UpdateArtist(update));
        app.MapPost("/Update/Track", (TrackUpdate update) => apiService.UpdateTrack(update));
        return app;
    }

    private static WebApplication AddPlayerRoutes(this WebApplication app)
    {
        var play = app.Services.GetService<IPlayService>();
        app.MapGet("/Play/Track/{id}", async (string id) => (await play.GetTrackPlayPath(id)).Stream());
        return app;
    }

    private static WebApplication AddLibraryRoutes(this WebApplication app)
    {
        var apiLibraryService = app.Services.GetRequiredService<IApiLibraryService>();
        app.MapPost("/Startup", () => apiLibraryService.Populate());
        app.MapGet("/Artists", () => apiLibraryService.GetAllArtists());
        app.MapGet("/Artists/Album", () => apiLibraryService.GetAlbumArtists());
        app.MapGet("/Artists/Track", () => apiLibraryService.GetTrackArtists());
        app.MapGet("/Artists/Grouping", (Grouping id) => apiLibraryService.GetArtistsByGrouping(id));
        app.MapGet("/Artists/Genre", (string id) => apiLibraryService.GetArtistsByGenre(id));
        app.MapGet("/Artist", (string id) => apiLibraryService.GetArtist(id));
        app.MapGet("/Artist/Albums", (string id) => apiLibraryService.GetAlbums(id));
        app.MapGet("/Track", (string id) => apiLibraryService.GetTrack(id));
        app.MapGet("/Album", (string id) => apiLibraryService.GetAlbum(id));
        app.MapGet("/Album/Tracks", (string id) => apiLibraryService.GetTracks(id));

        var playTracks = app.Services.GetRequiredService<IPlayTrackCache>();
        app.MapGet("/Play/Tracks", () => playTracks.GetAll());
        app.MapGet("/Play/Artist", (string id) => playTracks.GetByArtist(id));
        app.MapGet("/Play/Album", (string id) => playTracks.GetByAlbum(id));
        app.MapGet("/Play/Genre", (string id) => playTracks.GetByGenre(id));
        app.MapGet("/Play/Grouping", (Grouping id) => playTracks.GetByGrouping(id));

        var searchItems = app.Services.GetRequiredService<ISearchCache>();
        app.MapGet("/Search/Artists", () => searchItems.GetSearchArtists());
        app.MapGet("/Search/Groupings", () => searchItems.GetSearchGroupings());
        app.MapGet("/Search/Genres", () => searchItems.GetSearchGenres());
        app.MapGet("/Search/Albums", () => searchItems.GetSearchAlbums());
        app.MapGet("/Search/Playlists", () => searchItems.GetSearchPlaylists());
        app.MapGet("/Search/Tracks", () => searchItems.GetSearchTracks());

        var externalSourceService = app.Services.GetRequiredService<IExternalSourceService>();
        app.MapPost("/Source/Add", (FullLibrary library) => externalSourceService.AddLibrary(library));

        var artworkService = app.Services.GetRequiredService<IArtworkService>();
        app.MapGet("/Library/Artwork", async (HttpContext context) => 
        {
            var id = context.Request.Query["id"].Single();
            var artwork = await artworkService.GetArtwork(id);
            await context.WriteArtwork(artwork);
        });

        return app;
    }
}
