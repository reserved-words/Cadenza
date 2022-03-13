using Cadenza.Library;

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
        var id3UpdateQueue = app.Services.GetRequiredService<IFileUpdateService>();
        var library = app.Services.GetRequiredService<ILibrary>();
        var artists = app.Services.GetRequiredService<IArtistCache>();

        app.MapPost("/Update/Artist", async (ArtistUpdate update) =>
        {
            await artists.UpdateArtist(update);
            await library.UpdateArtist(update);
            foreach (var updatedItem in update.Updates)
            {
                await id3UpdateQueue.Add(updatedItem);
            }
        });

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
        var library = app.Services.GetRequiredService<ILibrary>();

        var albums = app.Services.GetRequiredService<IAlbumCache>();
        var artists = app.Services.GetRequiredService<IArtistCache>();
        var playTracks = app.Services.GetRequiredService<IPlayTrackCache>();
        var searchItems = app.Services.GetRequiredService<ISearchCache>();
        var tracks = app.Services.GetRequiredService<ITrackCache>();

        app.MapPost("/Startup", async () =>
        {
            await library.Populate();
            var fullLibrary = await library.Get();
            await albums.Populate(fullLibrary);
            await artists.Populate(fullLibrary);
            await playTracks.Populate(fullLibrary);
            await searchItems.Populate(fullLibrary);
            await tracks.Populate(fullLibrary);
        });

        app.MapGet("/Artists", (int page, int limit) => artists.GetAllArtists(page, limit));
        app.MapGet("/Artists/Album", (int page, int limit) => artists.GetAlbumArtists(page, limit));
        app.MapGet("/Artists/Track", (int page, int limit) => artists.GetTrackArtists(page, limit));
        app.MapGet("/Artists/Grouping", (Grouping id, int page, int limit) => artists.GetArtistsByGrouping(id, page, limit));
        app.MapGet("/Artists/Genre", (string id, int page, int limit) => artists.GetArtistsByGenre(id, page, limit));
        app.MapGet("/Artist", (string id) => artists.GetArtist(id));
        app.MapGet("/Artist/Albums", (string id, int page, int limit) => artists.GetAlbums(id, page, limit));

        app.MapGet("/Play/Tracks", (int page, int limit) => playTracks.GetAll(page, limit));
        app.MapGet("/Play/Artist", (string id, int page, int limit) => playTracks.GetByArtist(id, page, limit));
        app.MapGet("/Play/Album", (string id, int page, int limit) => playTracks.GetByAlbum(id, page, limit));
        app.MapGet("/Play/Genre", (string id, int page, int limit) => playTracks.GetByGenre(id, page, limit));
        app.MapGet("/Play/Grouping", (Grouping id, int page, int limit) => playTracks.GetByGrouping(id, page, limit));

        app.MapGet("/Search/Artists", (int page, int limit) => searchItems.GetSearchArtists(page, limit));
        app.MapGet("/Search/Groupings", (int page, int limit) => searchItems.GetSearchGroupings(page, limit));
        app.MapGet("/Search/Genres", (int page, int limit) => searchItems.GetSearchGenres(page, limit));
        app.MapGet("/Search/Albums", (int page, int limit) => searchItems.GetSearchAlbums(page, limit));
        app.MapGet("/Search/Playlists", (int page, int limit) => searchItems.GetSearchPlaylists(page, limit));
        app.MapGet("/Search/Tracks", (int page, int limit) => searchItems.GetSearchTracks(page, limit));

        app.MapGet("/Track", (string id) => tracks.GetTrack(id));
        app.MapGet("/Album", (string id) => albums.GetAlbum(id));
        app.MapGet("/Album/Tracks", (string id) => albums.GetTracks(id));

        var libraryService = app.Services.GetRequiredService<IArtworkService>();

        app.MapGet("/Library/Artwork", async (HttpContext context) => 
        {
            var id = context.Request.Query["id"].Single();

            var artwork = await libraryService.GetArtwork(id);

            if (artwork.Bytes == null || artwork.Bytes.Length == 0)
            {
                var bytes = File.ReadAllBytes("Images/default.png");
                artwork = new(bytes, "image/png");
            }

            context.Response.ContentType = artwork.Type;
            context.Response.ContentLength = artwork.Bytes.Length;
            await context.Response.BodyWriter.WriteAsync(new ReadOnlyMemory<byte>(artwork.Bytes));
        });

        return app;
    }
}
