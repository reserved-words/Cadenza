namespace Cadenza.Utilities.Interfaces;

public interface IJsonConverter
{
    string Serialize<T>(T item);
    T Deserialize<T>(string json) where T : new();
}
