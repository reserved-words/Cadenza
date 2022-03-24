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
        app.MapGet("/Artists", (int page, int limit) => apiLibraryService.GetAllArtists(page, limit));
        app.MapGet("/Artists/Album", (int page, int limit) => apiLibraryService.GetAlbumArtists(page, limit));
        app.MapGet("/Artists/Track", (int page, int limit) => apiLibraryService.GetTrackArtists(page, limit));
        app.MapGet("/Artists/Grouping", (Grouping id, int page, int limit) => apiLibraryService.GetArtistsByGrouping(id, page, limit));
        app.MapGet("/Artists/Genre", (string id, int page, int limit) => apiLibraryService.GetArtistsByGenre(id, page, limit));
        app.MapGet("/Artist", (string id) => apiLibraryService.GetArtist(id));
        app.MapGet("/Artist/Albums", (string id, int page, int limit) => apiLibraryService.GetAlbums(id, page, limit));
        app.MapGet("/Track", (string id) => apiLibraryService.GetTrack(id));
        app.MapGet("/Album", (string id) => apiLibraryService.GetAlbum(id));
        app.MapGet("/Album/Tracks", (string id) => apiLibraryService.GetTracks(id));

        var playTracks = app.Services.GetRequiredService<IPlayTrackCache>();
        app.MapGet("/Play/Tracks", (int page, int limit) => playTracks.GetAll(page, limit));
        app.MapGet("/Play/Artist", (string id, int page, int limit) => playTracks.GetByArtist(id, page, limit));
        app.MapGet("/Play/Album", (string id) => playTracks.GetByAlbum(id));
        app.MapGet("/Play/Genre", (string id, int page, int limit) => playTracks.GetByGenre(id, page, limit));
        app.MapGet("/Play/Grouping", (Grouping id, int page, int limit) => playTracks.GetByGrouping(id, page, limit));

        var searchItems = app.Services.GetRequiredService<ISearchCache>();
        app.MapGet("/Search/Artists", (int page, int limit) => searchItems.GetSearchArtists(page, limit));
        app.MapGet("/Search/Groupings", (int page, int limit) => searchItems.GetSearchGroupings(page, limit));
        app.MapGet("/Search/Genres", (int page, int limit) => searchItems.GetSearchGenres(page, limit));
        app.MapGet("/Search/Albums", (int page, int limit) => searchItems.GetSearchAlbums(page, limit));
        app.MapGet("/Search/Playlists", (int page, int limit) => searchItems.GetSearchPlaylists(page, limit));
        app.MapGet("/Search/Tracks", (int page, int limit) => searchItems.GetSearchTracks(page, limit));

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
