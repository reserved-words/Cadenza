using System.Text.Json;
using System.Text.Json.Serialization;

namespace Cadenza.Common;

public static class JsonSerialization
{
    static JsonSerialization()
    {
        Options = new JsonSerializerOptions(JsonSerializerDefaults.Web);
        SetOptions(Options);
    }

    public static JsonSerializerOptions Options { get; }

    public static void SetOptions(JsonSerializerOptions options)
    {
        options.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
        options.WriteIndented = true;
        options.Converters.Add(new JsonStringEnumConverter());
    }
}