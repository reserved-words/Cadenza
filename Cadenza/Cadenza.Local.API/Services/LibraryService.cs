
namespace Cadenza.Local.API;

public class LibraryService : ILibraryService
{
    private readonly IImageSrcGenerator _imageSrcGenerator;
    private readonly ILibrary _library;
    private readonly IStaticSource _jsonLibrary;


    public LibraryService(ILibrary library, IImageSrcGenerator imageSrcGenerator, IStaticSource jsonLibrary)
    {
        _library = library;
        _imageSrcGenerator = imageSrcGenerator;
        _jsonLibrary = jsonLibrary;
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

    public async Task<ICollection<ArtistInfo>> GetArtists()
    {
        var library = await _jsonLibrary.GetStaticLibrary();
        return library.Artists;
    }

    public async Task<ICollection<AlbumInfo>> GetAlbums(string artworkUrlFormat)
    {
        var library = await _jsonLibrary.GetStaticLibrary();

        foreach (var album in library.Albums)
        {
            var firstTrack = library.AlbumTrackLinks.First(a => a.AlbumId == album.Id);
            album.ImageUrl = string.Format(artworkUrlFormat, firstTrack.TrackId);
        }

        return library.Albums;
    }

    public async Task<ICollection<TrackInfo>> GetTracks()
    {
        var library = await _jsonLibrary.GetStaticLibrary();
        return library.Tracks;
    }

    public async Task<ICollection<AlbumTrackLink>> GetAlbumTrackLinks()
    {
        var library = await _jsonLibrary.GetStaticLibrary();
        return library.AlbumTrackLinks;
    }

    public async Task<(byte[] Bytes, string Type)> GetArtwork(string id)
    {
        return _imageSrcGenerator.GetArtwork(id);
    }
}