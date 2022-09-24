namespace Cadenza.Utilities.Interfaces;

public interface IIdGenerator
{
    string GenerateId(params string[] names);
}
