﻿using Cadenza.Common.Domain.Model;

namespace Cadenza.Local.API.Files.Interfaces;

internal interface IId3TagsService
{
    Id3Data GetId3Data(string filepath);

    void SaveId3Data(string filepath, Id3Data data);

    ArtworkImage GetAlbumArtwork(string filepath);
    ArtworkImage GetArtistImage(string filepath);
}
