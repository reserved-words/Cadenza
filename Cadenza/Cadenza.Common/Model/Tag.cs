namespace Cadenza.Common;

public class Tag : IMergeable
{
    public string Value { get; set; }

    public string Id => Value;
    public bool IsPopulated => true;
}
