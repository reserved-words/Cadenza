using Microsoft.Extensions.Options;

namespace Cadenza.Local.API.Core.Services;

internal class FilepathParser : IFilepathParser
{
    private readonly MusicLibrarySettings _settings;

    public FilepathParser(IOptions<MusicLibrarySettings> settingsOptions)
    {
        _settings = settingsOptions.Value;
    }

    public string GetFilepathFromId(string id)
    {
        return Path.Combine(_settings.Directory, id);
    }

    public string GetIdFromFilepath(string filepath)
    {
        return Path.GetRelativePath(_settings.Directory, filepath);
    }
}
