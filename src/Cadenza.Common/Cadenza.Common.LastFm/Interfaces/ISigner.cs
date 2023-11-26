namespace Cadenza.Common.LastFm.Interfaces;

internal interface ISigner
{
    void Sign(Dictionary<string, string> parameters);
}
