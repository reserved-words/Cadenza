namespace Cadenza.Local;

public interface IId3ToJsonConverter
{
    JsonArtist ConvertAlbumArtist(Id3Data id3Data);
    JsonAlbum ConvertAlbum(Id3Data id3Data);
    JsonTrack ConvertTrack(Id3Data id3Data);
    JsonArtist ConvertTrackArtist(Id3Data id3Data);
    JsonAlbumTrackLink ConvertAlbumTrackLink(Id3Data id3Data);
}