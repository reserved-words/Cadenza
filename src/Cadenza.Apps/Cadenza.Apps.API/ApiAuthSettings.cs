namespace Cadenza.Apps.API;

public class ApiAuthSettings
{
    public string Domain { get; set; }
    public string Audience { get; set; }
    public string Scope { get; set; }
    public string[] ValidUsers { get; set; }

    public string FullDomain => $"https://{Domain}/";
}