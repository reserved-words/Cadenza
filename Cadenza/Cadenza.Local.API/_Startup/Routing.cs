namespace Cadenza.Local.API;

public static class Routing
{
    private const string ArtworkUrlFormat = "/library/artwork?id={0}";

    public static WebApplication AddRoutes(this WebApplication app)
    {
        return app
            .AddLibraryRoutes()
            .AddPlayerRoutes()
            .AddUpdaterRoutes();
    }

    private static WebApplication AddUpdaterRoutes(this WebApplication app)
    {
        var updater = app.Services.GetService<IUpdateService>();
        app.MapPost("/Update/Album", (AlbumUpdate update) => updater.UpdateAlbum(update));
        app.MapPost("/Update/Artist", (ArtistUpdate update) => updater.UpdateArtist(update));
        app.MapPost("/Update/Track", (TrackUpdate update) => updater.UpdateTrack(update));
        app.MapGet("/Update/Queue", () => updater.GetQueue());
        app.MapDelete("/Update/Unqueue", ([FromBody] MetaDataUpdate update) => updater.Unqueue(update));
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
        var library = app.Services.GetService<ILibraryService>();

        app.MapGet("/Library/Artists", () => library.GetArtists());
        app.MapGet("/Library/Albums", () => library.GetAlbums(ArtworkUrlFormat));
        app.MapGet("/Library/Track/{id}", (string id) => library.GetTrackSummary(ArtworkUrlFormat, id));
        app.MapGet("/Library/FullTrack/{id}", (string id) => library.GetTrack(ArtworkUrlFormat, id));
        app.MapGet("/Library/AllTracks", () => library.GetAllTracks());
        app.MapGet("/Library/ArtistTracks/{id}", (string id) => library.GetArtistTracks(id));
        app.MapGet("/Library/AlbumTracks/{id}", (string id) => library.GetAlbumTracks(id));

        app.MapGet("/Library/Artwork", async (HttpContext context) =>
        {
            var id = context.Request.Query["id"];
            var artwork = await library.GetArtwork(id);

            if (artwork.Bytes == null || artwork.Bytes.Length == 0)
            {
                try
                {
                    var bytes = File.ReadAllBytes("Images/default.png");
                    artwork = new(bytes, "image/png");
                }
                catch (Exception ex)
                {

                    throw;
                }
            }

            context.Response.ContentType = artwork.Type;
            context.Response.ContentLength = artwork.Bytes.Length;
            await context.Response.BodyWriter.WriteAsync(new ReadOnlyMemory<byte>(artwork.Bytes));
        });

        return app;
    }
}
