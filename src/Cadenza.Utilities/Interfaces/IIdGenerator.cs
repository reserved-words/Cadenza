namespace Cadenza.Utilities;

public interface IIdGenerator
{
    string GenerateId(params string[] names);
}
