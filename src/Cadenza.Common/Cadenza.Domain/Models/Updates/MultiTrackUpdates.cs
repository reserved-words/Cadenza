﻿namespace Cadenza.Domain.Models.Updates;

public class MultiTrackUpdates
{
    public List<string> TrackIds { get; set; }
    public List<PropertyUpdate> Updates { get; set; } = new();
}