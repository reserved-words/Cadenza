using Microsoft.Extensions.Options;

namespace Cadenza.Local.API.Core;

internal class LibraryService : ILibraryService
{
    private readonly IBase64Converter _converter;
    private readonly IImageSrcGenerator _imageSrcGenerator;
    private readonly IOptions<CurrentlyPlayingSettings> _settings;

    public LibraryService(IOptions<CurrentlyPlayingSettings> settings, IBase64Converter converter, IImageSrcGenerator imageSrcGenerator)
    {
        _settings = settings;
        _converter = converter;
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

    public Task<string> GetPlayPath(string id)
    {
        var filepath = _converter.FromBase64(id);
        var copyFilename = CreatePlayingFilepath(filepath);
        var copyLocation = GetCopyLocation();
        var copyFilepath = Path.Combine(copyLocation, copyFilename);
        File.Copy(filepath, copyFilepath);
        return Task.FromResult(copyFilepath);
    }

    private string GetCopyLocation()
    {
        var directory = Path.Combine(_settings.Value.BaseDirectory, _settings.Value.DirectoryName);
        if (!Directory.Exists(directory))
        {
            Directory.CreateDirectory(directory);
        }
        return directory;
    }

    private string CreatePlayingFilepath(string filepath)
    {
        var extension = Path.GetExtension(filepath);
        var timestamp = DateTime.Now.ToFileTimeUtc().ToString();
        var newFileName = $"{_settings.Value.FilePrefix}_{timestamp}{extension}";
        return newFileName;
    }
}