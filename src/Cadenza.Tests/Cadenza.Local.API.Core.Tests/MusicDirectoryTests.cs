using Cadenza.Common.Domain.Model;
using Cadenza.Common.Utilities.Interfaces;
using Cadenza.Local.API.Core.Services;
using Cadenza.Local.API.Core.Settings;
using Microsoft.Extensions.Options;
using NSubstitute;

namespace Cadenza.Local.API.Core.Tests;

public class MusicDirectoryTests
{
    private const string MusicDirectory = "C:\\Music";
    private const string RemovedDirectory = "\\\\Test\\C$\\Removed";
    
    private readonly IFileAccess _mockFileAccess = Substitute.For<IFileAccess>();
    private readonly IOptions<MusicLibrarySettings> _mockSettingsOptions = Substitute.For<IOptions<MusicLibrarySettings>>();

    private readonly FileDetails _file1 = new() { Path = "1.mp3" };
    private readonly FileDetails _file2 = new() { Path = "2.mp3" };

    private readonly List<string> _fileExtensions = new List<string> { ".mp3" };

    [SetUp]
    public void SetUp()
    {
        var settings = new MusicLibrarySettings 
        { 
            Directory = MusicDirectory, 
            FileExtensions = _fileExtensions,
            RemovedDirectory = RemovedDirectory
        };
        _mockSettingsOptions.Value.Returns(settings);
    }
    
    [Test]
    public async Task GetAllFiles_ReturnsAllFiles()
    {
        // Arrange
        var expectedFiles = new List<FileDetails> { _file1, _file2 };
        _mockFileAccess.GetFiles(MusicDirectory, _fileExtensions).Returns(expectedFiles);
        var sut = GetSubjectUnderTest();

        // Act
        var result = await sut.GetAllFiles();

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result, Is.Not.Empty);
        Assert.That(result, Has.Count.EqualTo(expectedFiles.Count));
        Assert.That(result, Contains.Item(_file1));
        Assert.That(result, Contains.Item(_file2));
    }

    [Test]
    public async Task GetModifiedFiles_ReturnsAllModifiedFiles()
    {
        // Arrange
        var sinceDate = new DateTime(2022, 1, 1);
        var modifiedFile = new FileDetails { DateModified = sinceDate.AddDays(1) };
        var unmodifiedFile = new FileDetails { DateModified = sinceDate.AddDays(-1) };
        var allFiles = new List<FileDetails> { _file1, _file2, modifiedFile, unmodifiedFile };
        _mockFileAccess.GetFiles(MusicDirectory, _fileExtensions).Returns(allFiles);
        var sut = GetSubjectUnderTest();

        // Act
        var result = await sut.GetModifiedFiles(sinceDate);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result, Is.Not.Empty);
        Assert.That(result, Has.Count.EqualTo(1));
        Assert.That(result, Contains.Item(modifiedFile));
    }

    [Test]
    public async Task RemoveFile_RemovesFile()
    {
        // Arrange
        var relativeFilePath = "Artist\\Album\\Track.mp3";
        var filepath = Path.Combine(MusicDirectory, relativeFilePath);
        var targetPath = Path.Combine(RemovedDirectory, relativeFilePath);
        var sut = GetSubjectUnderTest();

        // Act
        await sut.RemoveFile(filepath);

        // Assert
        _mockFileAccess.Received().MoveFile(filepath, targetPath);
    }

    private MusicDirectory GetSubjectUnderTest()
    {
        return new MusicDirectory(_mockSettingsOptions, _mockFileAccess);
    }
}
