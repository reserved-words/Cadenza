using Cadenza.Local.Common.Interfaces.Converters;
using Cadenza.Local.Common.Model.Id3;
using Cadenza.Local.Common.Model.Json;

namespace Cadenza.Local;

public class Id3ToJsonConverter : IId3ToJsonConverter
{
    private readonly IAlbumConverter _albumConverter;
    private readonly IAlbumTrackLinkConverter _albumTrackLinkConverter;
    private readonly IArtistConverter _artistConverter;
    private readonly ITrackConverter _trackConverter;

    public Id3ToJsonConverter(IAlbumConverter albumConverter, IArtistConverter artistConverter,
        ITrackConverter trackConverter, IAlbumTrackLinkConverter albumTrackLinkConverter)
    {
        _albumConverter = albumConverter;
        _artistConverter = artistConverter;
        _trackConverter = trackConverter;
        _albumTrackLinkConverter = albumTrackLinkConverter;
    }

    public JsonAlbum ConvertAlbum(Id3Data id3Data)
    {
        return _albumConverter.ToJsonModel(id3Data);
    }

    public JsonArtist ConvertAlbumArtist(Id3Data id3Data)
    {
        return _artistConverter.ToJsonModel(id3Data, true);
    }

    public JsonAlbumTrackLink ConvertAlbumTrackLink(Id3Data id3Data)
    {
        return _albumTrackLinkConverter.ToJsonModel(id3Data);
    }

    public JsonTrack ConvertTrack(Id3Data id3Data)
    {
        return _trackConverter.ToJsonModel(id3Data);
    }

    public JsonArtist ConvertTrackArtist(Id3Data id3Data)
    {
        return _artistConverter.ToJsonModel(id3Data, false);
    }
}