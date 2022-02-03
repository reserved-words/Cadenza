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
        //var updateQueue = app.Services.GetService<IFileUpdateService>();
        //var updaterFactory = app.Services.GetService<Func<IUpdater>>();
        //var updater = updaterFactory();
        //app.MapPost("/Update/Album", (AlbumInfo album, List<ItemPropertyUpdate> updates) => updater.Update(album, updates));
        //app.MapPost("/Update/Artist", (ArtistInfo artist, List<ItemPropertyUpdate> updates) => updater.Update(artist, updates));
        //app.MapPost("/Update/Track", (TrackInfo track, List<ItemPropertyUpdate> updates) => updater.Update(track, updates));
        //app.MapGet("/Update/Queue", () => updateQueue.Get());
        //app.MapDelete("/Update/Unqueue", ([FromBody] ItemPropertyUpdate update) => updateQueue.Remove(update));
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
        var libraryService = app.Services.GetRequiredService<IArtworkService>();

        app.MapGet("/Library/Artists", () => library.GetArtists());
        app.MapGet("/Library/Albums", () => library.GetAlbums());
        app.MapGet("/Library/Track/{id}", (string id) => library.GetTrack(id));
        app.MapGet("/Library/FullTrack/{id}", (string id) => library.GetFullTrack(id));
        app.MapGet("/Library/AllTracks", () => library.GetAllTracks());

        app.MapGet("/Library/Artwork", async (HttpContext context) =>
        {
            var id = context.Request.Query["id"];
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
