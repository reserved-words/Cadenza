﻿namespace Cadenza.Common.DTO;

public class UpdateAlbumDTO
{
    public UpdatedAlbumPropertiesDTO OriginalAlbum { get; set; }
    public UpdatedAlbumPropertiesDTO UpdatedAlbum { get; set; }
}

public class UpdateLovedTrackDTO
{
    public int TrackId { get; set; }
}