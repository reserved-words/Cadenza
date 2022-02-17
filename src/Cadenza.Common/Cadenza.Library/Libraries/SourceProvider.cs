namespace Cadenza.Library.Libraries
{
    internal class SourceProvider : ISource
    {
        private readonly LibrarySource _librarySource;

        public SourceProvider(LibrarySource librarySource)
        {
            _librarySource = librarySource;
        }

        public LibrarySource Source => _librarySource;
    }
}
