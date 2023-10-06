using Cadenza.Common.Utilities.Interfaces;
using Cadenza.Common.Utilities.Json;
using System.Text.Json;

namespace Cadenza.Common.Utilities.Services;

internal class JsonService : IJsonService
{
    public string Serialize<T>(T item)
    {
        return JsonSerializer.Serialize(item, JsonSerialization.Options);
    }

    public T Deserialize<T>(string json) where T : new()
    {
        if (string.IsNullOrWhiteSpace(json))
            return new T();

        json = json.Trim();

        try
        {
            return JsonSerializer.Deserialize<T>(json, JsonSerialization.Options);
        }
        catch (Exception)
        {
            return new T();
        }
    }
}
