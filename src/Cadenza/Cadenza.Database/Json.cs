using System.Text;

namespace Cadenza.Database;

internal static class Json
{
    public static string Serialize<T>(T input)
    {
        var bytes = SpanJson.JsonSerializer.Generic.Utf8.Serialize<T>(input);
        return Encoding.UTF8.GetString(bytes);
        // return JsonConvert.SerializeObject(input);
    }

    public static T Deserialize<T>(string input)
    {
        var toBytes = Encoding.UTF8.GetBytes(input);
        return SpanJson.JsonSerializer.Generic.Utf8.Deserialize<T>(toBytes);
        // return JsonConvert.DeserializeObject<T>(input);
    }
}