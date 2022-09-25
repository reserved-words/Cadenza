
using Cadenza.Domain.Models;
using Cadenza.Domain.Models.Update;
using Cadenza.Utilities.Interfaces;
using Cadenza.Web.Common.Interfaces;

namespace Cadenza.Web.Database.Services;

internal class UpdateService : ApiRepositoryBase, IUpdateService
{

    private readonly IApiRepositorySettings _settings;

    public UpdateService(IHttpHelper http, IApiRepositorySettings settings) 
        : base(http, settings)
    {
        _settings = settings;
    }

    public async Task UpdateArtist(ArtistUpdate update)
    {
        var data = new ItemUpdates
        {
            Id = update.Id,
            Type = update.Type,
            Name = update.Name,
            Updates = update.Updates
        };
        await Post(_settings.UpdateItem, data);
    }

    public async Task UpdateTrack(TrackUpdate update)
    {
        var data = new ItemUpdates
        {
            Id = update.Id,
            Type = update.Type,
            Name = update.Name,
            Updates = update.Updates
        };
        await Post(_settings.UpdateItem, data);
    }
}
