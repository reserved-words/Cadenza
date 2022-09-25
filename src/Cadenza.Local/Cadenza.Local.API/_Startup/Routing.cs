using Cadenza.Domain.Models;
using Cadenza.Local.API.Common.Controllers;

namespace Cadenza.Local.API;

public static class Routing
{
    public static WebApplication AddRoutes(this WebApplication app)
    {
        app.MapGet("/Connect", () => "Connected");

        return app
            .AddArtworkRoute()
            .AddPlayerRoute()
            .AddSyncRoutes();
    }

    private static WebApplication AddArtworkRoute(this WebApplication app)
    {
        var service = app.Services.GetRequiredService<IArtworkService>();
        app.MapGet("/Library/Artwork", async (HttpContext context) =>
        {
            var id = context.Request.Query["id"].Single();
            var artwork = await service.GetArtwork(id);
            await context.WriteArtwork(artwork);
        });
        return app;
    }

    private static WebApplication AddPlayerRoute(this WebApplication app)
    {
        var service = app.Services.GetService<IPlayService>();
        app.MapGet("/Play/Track/{id}", async (string id) => (await service.GetTrackPlayPath(id)).Stream());
        return app;
    }

    private static WebApplication AddSyncRoutes(this WebApplication app)
    {
        var service = app.Services.GetService<ISyncService>();
        app.MapGet("/Sync/GetAllTracks", async (string id) => await service.GetAllTracks());
        app.MapGet("/Sync/GetTrack/{id}", async (string id) => await service.GetTrack(id));
        app.MapPost("/Sync/UpdateTrack/{id}", async (string id, List<PropertyUpdate> updates) => await service.UpdateTrack(id, updates));
        return app;
    }
}
