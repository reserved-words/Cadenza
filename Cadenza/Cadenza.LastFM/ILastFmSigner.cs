namespace Cadenza.LastFM;

public interface ILastFmSigner
{
    void Sign(Dictionary<string, string> parameters);
}
