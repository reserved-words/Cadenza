namespace Cadenza.API.LastFM.Interfaces;

public interface ILastFmSigner
{
    void Sign(Dictionary<string, string> parameters);
}
