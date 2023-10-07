//using System.Text.Json;
//using System.Text.Json.Serialization;

//namespace Cadenza.Common.DTO.Serialization;

//internal class TagListConverter : JsonConverter<TagListDTO>
//{
//    public override TagListDTO Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
//    {
//        var value = reader.GetString();

//        return string.IsNullOrWhiteSpace(value)
//            ? new TagListDTO()
//            : new TagListDTO(value);
//    }

//    public override void Write(Utf8JsonWriter writer, TagListDTO value, JsonSerializerOptions options)
//    {
//        writer.WriteStringValue(value.ToString());
//    }
//}
