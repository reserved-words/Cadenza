﻿using Cadenza.Web.Common.Interfaces.Updates;

namespace Cadenza.Web.Core.Coordinators;

internal class UpdatesCoordinator : IUpdatesCoordinator
{
    private readonly IMessenger _messenger;

    public UpdatesCoordinator(IMessenger messenger)
    {
        _messenger = messenger;
    }

    public async Task UpdateArtist(ArtistUpdate update)
    {
        await _messenger.Send(this, new ArtistUpdatedEventArgs(update));
    }

    public async Task UpdateLyrics(TrackUpdate update)
    {
        await _messenger.Send(this, new LyricsUpdatedEventArgs(update));
    }
}