namespace Cadenza.Domain.Enums;

public enum MergeMode
{
    Merge, // use original value if not empty, otherwise use update
    Override, // use update value if not empty, otherwise use original
    Update // always use update value 
}
