using Cadenza.Utilities.Interfaces;

namespace Cadenza.Utilities.Implementations;

internal class CurrentDateTime : IDateTime
{
    public DateTime Now => DateTime.Now;
}
