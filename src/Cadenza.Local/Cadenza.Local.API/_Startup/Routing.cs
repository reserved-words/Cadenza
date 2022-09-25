using Cadenza.Local.API.Common.Controllers;

namespace Cadenza.Local.API;

public static class Routing
{
    public static WebApplication AddRoutes(this WebApplication app)
    {
        app.MapGet("/Connect", () => "Connected");

        return app
            .AddPlayerRoute()
            .AddArtworkRoute();
    }

    private static WebApplication AddPlayerRoute(this WebApplication app)
    {
        var play = app.Services.GetService<IPlayService>();
        app.MapGet("/Play/Track/{id}", async (string id) => (await play.GetTrackPlayPath(id)).Stream());
        return app;
    }

    private static WebApplication AddArtworkRoute(this WebApplication app)
    {
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
