namespace Cadenza.Local.API.Core.Interfaces;

internal interface IFilepathParser
{
    string GetFilepathFromId(string id);
    string GetIdFromFilepath(string filepath);
}