namespace Cadenza.Common.Utilities.Services;

internal class CurrentDateTime : IDateTime
{
    public DateTime Now => DateTime.Now;
}
