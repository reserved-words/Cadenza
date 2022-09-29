using Cadenza.Common.Domain.Model.Album;

namespace Cadenza.API.Database.Interfaces.Converters;

internal interface IAlbumConverter
{
    JsonAlbum ToJson(AlbumInfo artist);
    AlbumInfo ToModel(JsonAlbum artist, ICollection<JsonArtist> artists);
}