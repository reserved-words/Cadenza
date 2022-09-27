﻿using Cadenza.Local.API.Common.Controllers;
using Cadenza.Local.API.Core.Interfaces;

namespace Cadenza.Local.API.Core;

internal class ArtworkService : IArtworkService
{
    private readonly IImageSrcGenerator _imageSrcGenerator;

    public ArtworkService(IImageSrcGenerator imageSrcGenerator)
    {
        _imageSrcGenerator = imageSrcGenerator;
    }

    public Task<(byte[] Bytes, string Type)> GetArtwork(string id)
    {
        var result = _imageSrcGenerator.GetArtwork(id);

        if (result.Bytes == null || result.Bytes.Length == 0)
        {
            var bytes = File.ReadAllBytes("Images/default.png");
            result = new(bytes, "image/png");
        }

        return Task.FromResult(result);
    }
}
