namespace Cadenza.Local.API.Files.Interfaces;

internal interface IId3Updater
{
    Task UpdateTags(string filepath, List<PropertyUpdate> updates);
}
