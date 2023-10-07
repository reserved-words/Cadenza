using Cadenza.Common.Domain.Model.Sync;

namespace Cadenza.Local.API.Files.Interfaces;

internal interface IId3Fetcher
{
    SyncTrackDTO GetFileData(string id, string filepath);
}