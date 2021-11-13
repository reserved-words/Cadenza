namespace Cadenza.Common;

public class CurrentDateTime : IDateTime
{
    public DateTime Now => DateTime.Now;
}
