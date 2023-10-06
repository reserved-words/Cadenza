using System.Text.Json;
using System.Text.Json.Serialization;
using Cadenza.Common.Domain.Model.Library;

namespace Cadenza.Common.Utilities.Json;

internal class TagListConverter : JsonConverter<TagList>
{
    public override TagList Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var value = reader.GetString();

        return string.IsNullOrWhiteSpace(value)
            ? new TagList()
            : new TagList(value);
    }

    public override void Write(Utf8JsonWriter writer, TagList value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.ToString());
    }
}
