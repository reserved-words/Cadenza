namespace Cadenza.API.LastFM.Interfaces;

internal interface ISigner
{
    void Sign(Dictionary<string, string> parameters);
}
