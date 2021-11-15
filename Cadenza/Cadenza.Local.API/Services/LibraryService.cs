
namespace Cadenza.Local.API;

public class LibraryService : ILibraryService
{
    private readonly IImageSrcGenerator _imageSrcGenerator;
    private readonly ILibrary _library;

    public LibraryService(ILibrary library, IImageSrcGenerator imageSrcGenerator)
    {
        _library = library;
        _imageSrcGenerator = imageSrcGenerator;
    }

    public async Task<ICollection<Artist>> GetAlbumArtists()
    {
        return await _library.GetAlbumArtists();
    }

    public async Task<ArtistFull> GetAlbumArtist(string artistId)
    {
        artistId = UrlDecode(artistId);

        var artist = await _library.GetAlbumArtist(artistId);
        if (artist == null)
            return null;

        foreach (var album in artist.Albums)
        {
            album.Album.ImageUrl ??= _imageSrcGenerator.GetImageSrc(album);
        }
        return artist;
    }

    public async Task<TrackSummary> GetTrackSummary(string id)
    {
        id = UrlDecode(id);
        var track = await _library.GetTrackSummary(id);
        track.Album.ImageUrl ??= _imageSrcGenerator.GetImageSrc(track);
        return track;
    }

    public async Task<TrackFull> GetTrack(string id)
    {
        id = UrlDecode(id);
        var track = await _library.GetTrack(id);
        track.Album.ImageUrl ??= _imageSrcGenerator.GetImageSrc(track);
        return track;
    }

    private string UrlDecode(string text)
    {
        return HttpUtility.UrlDecode(text);
    }
}