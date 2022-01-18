namespace Cadenza.Local.API;

public class ArtworkService : IArtworkService
{
    private readonly IImageSrcGenerator _imageSrcGenerator;

    public ArtworkService(IImageSrcGenerator imageSrcGenerator)
    {
        _imageSrcGenerator = imageSrcGenerator;
    }
    
    public Task<(byte[] Bytes, string Type)> GetArtwork(string id)
    {
        var result = _imageSrcGenerator.GetArtwork(id);
        return Task.FromResult(result);
    }
}