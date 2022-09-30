namespace Cadenza.Local.API.Files.Interfaces;

internal interface IId3Fetcher
{
    TrackFull GetFileData(string filepath);
}