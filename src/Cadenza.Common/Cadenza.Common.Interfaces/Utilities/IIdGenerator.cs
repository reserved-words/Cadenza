namespace Cadenza.Common.Interfaces.Utilities;

public interface IIdGenerator
{
    string GenerateId(params string[] names);
}
