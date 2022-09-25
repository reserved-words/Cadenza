using Cadenza.API.Database.Model;
using Cadenza.Domain.Models.Album;

namespace Cadenza.API.Database.Interfaces.Converters;

internal interface IAlbumConverter
{
    JsonAlbum ToJson(AlbumInfo artist);
    AlbumInfo ToModel(JsonAlbum artist, ICollection<JsonArtist> artists);
}