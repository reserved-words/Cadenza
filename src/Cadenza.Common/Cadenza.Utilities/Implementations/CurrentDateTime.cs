using Cadenza.Utilities.Interfaces;

namespace Cadenza.Utilities.Implementations;

public class CurrentDateTime : IDateTime
{
    public DateTime Now => DateTime.Now;
}
