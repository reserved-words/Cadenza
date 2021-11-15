namespace Cadenza.Local.API;

public class PlayService : IPlayService
{
    private readonly IBase64Converter _converter;
    private readonly ILibraryConfiguration _config;

    public PlayService(ILibraryConfiguration config, IBase64Converter converter)
    {
        _config = config;
        _converter = converter;
    }

    public async Task<string> GetTrackPlayPath(string id)
    {
        try
        {
            var filepath = _converter.FromBase64(id);
            var copyFilename = CreatePlayingFilepath(filepath);
            var copyLocation = GetCopyLocation();
            var copyFilepath = Path.Combine(copyLocation, copyFilename);
            System.IO.File.Copy(filepath, copyFilepath);
            return copyFilepath;
        }
        catch (Exception ex)
        {

            throw;
        }
    }

    private string GetCopyLocation()
    {
        var directory = _config.CurrentlyPlayingLocation;
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
        var newFileName = $"{_config.CurrentlyPlayingPrefix}_{timestamp}{extension}";
        return newFileName;
    }
}