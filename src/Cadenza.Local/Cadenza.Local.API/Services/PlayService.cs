using Cadenza.Local.Config;
using Microsoft.Extensions.Options;

namespace Cadenza.Local.API;

public class PlayService : IPlayService
{
    private readonly IBase64Converter _converter;
    private readonly IOptions<CurrentlyPlaying> _config;

    public PlayService(IOptions<CurrentlyPlaying> config, IBase64Converter converter)
    {
        _config = config;
        _converter = converter;
    }

    public Task<string> GetTrackPlayPath(string id)
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
        var directory = Path.Combine(_config.Value.BaseDirectory, _config.Value.DirectoryName);
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
        var newFileName = $"{_config.Value.FilePrefix}_{timestamp}{extension}";
        return newFileName;
    }
}