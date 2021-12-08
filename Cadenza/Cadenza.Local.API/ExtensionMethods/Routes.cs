namespace Cadenza.Local.API;

public static class Routes
{
    public static WebApplication MapRoutes(this WebApplication app)
    {
        var artworkUrlFormat = "/library/artwork?id={0}";

        var library = app.Services.GetService<ILibraryService>();
        var play = app.Services.GetService<IPlayService>();
        var playlists = app.Services.GetService<IPlaylistService>();
        var updater = app.Services.GetService<IUpdateService>();

        app.MapGet("/Library/Artists", () => library.GetArtists());
        app.MapGet("/Library/Albums", () => library.GetAlbums(artworkUrlFormat));
        app.MapGet("/Library/Tracks", () => library.GetTracks());
        app.MapGet("/Library/AlbumTrackLinks", () => library.GetAlbumTrackLinks());
        app.MapGet("/Library/Artwork", async (HttpContext context) =>
        {
            var id = context.Request.Query["id"];
            var artwork = await library.GetArtwork(id);
            context.Response.ContentType = artwork.Type;
            context.Response.ContentLength = artwork.Bytes.Length;
            await context.Response.BodyWriter.WriteAsync(new ReadOnlyMemory<byte>(artwork.Bytes));
        });

        app.MapGet("/Play/Track/{id}", async (string id) => (await play.GetTrackPlayPath(id)).Stream());

        app.MapPost("/Update/Album", (AlbumUpdate update) => updater.UpdateAlbum(update));
        app.MapPost("/Update/Artist", (ArtistUpdate update) => updater.UpdateArtist(update));
        app.MapPost("/Update/Track", (TrackUpdate update) => updater.UpdateTrack(update));

        app.MapGet("/Update/Queue", () => updater.GetQueue());

        app.MapDelete("/Update/Unqueue", ([FromBody] MetaDataUpdate update) => updater.Unqueue(update));

        return app;
    }
}
