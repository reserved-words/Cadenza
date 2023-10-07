using Cadenza.Common.DTO;

namespace Cadenza.Local.API.Files.Interfaces;

internal interface IId3Updater
{
    void UpdateTags(string filepath, List<PropertyUpdateDTO> updates);
}
