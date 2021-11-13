namespace Cadenza.Local;

public interface IMusicDirectoryConfiguration
{
    string LibraryDirectoryPath { get; }
    List<string> FileExtensions { get; }
}
