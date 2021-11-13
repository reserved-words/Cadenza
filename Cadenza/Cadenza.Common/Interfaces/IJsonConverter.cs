namespace Cadenza.Common;

public interface IJsonConverter
{
    string Serialize<T>(T item);
    T Deserialize<T>(string json) where T : new();
}
