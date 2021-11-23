namespace Cadenza.Local.API;

public static class Routes
{
    public static WebApplication MapRoutes(this WebApplication app)
    {
        var library = app.Services.GetService<ILibraryService>();
        var play = app.Services.GetService<IPlayService>();
        var playlists = app.Services.GetService<IPlaylistService>();
        var updater = app.Services.GetService<IUpdateService>();

        app.MapGet("/Library/Artists", () => library.GetArtists());
        app.MapGet("/Library/Albums", () => library.GetAlbums());
        app.MapGet("/Library/Tracks", () => library.GetTracks());
        app.MapGet("/Library/AlbumTrackLinks", () => library.GetAlbumTrackLinks());

        //app.MapGet("/Library/Artist/{id}", (string id) => library.GetAlbumArtist(id));
        //app.MapGet("/Library/TrackSummary/{id}", (string id) => library.GetTrackSummary(id));
        //app.MapGet("/Library/Track/{id}", (string id) => library.GetTrack(id));

        app.MapGet("/Play/Track/{id}", async (string id) => (await play.GetTrackPlayPath(id)).Stream());

        //app.MapGet("/Playlist/All", () => playlists.All());

        app.MapPost("/Update/Album", (AlbumUpdate update) => updater.UpdateAlbum(update));
        app.MapPost("/Update/Artist", (ArtistUpdate update) => updater.UpdateArtist(update));
        app.MapPost("/Update/Track", (TrackUpdate update) => updater.UpdateTrack(update));

        app.MapGet("/Update/Queue", () => updater.GetQueue());

        app.MapDelete("/Update/Unqueue", ([FromBody] MetaDataUpdate update) => updater.Unqueue(update));

        return app;
    }
}
