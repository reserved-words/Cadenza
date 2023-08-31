using Cadenza.Local.API.Core.Services;
using Cadenza.Local.API.Core.Settings;
using Microsoft.Extensions.Options;
using NSubstitute;

namespace Cadenza.Local.API.Core.Tests;

public class FilepathParserTests
{
    private readonly IOptions<MusicLibrarySettings> _mockSettingsOptions = Substitute.For<IOptions<MusicLibrarySettings>>();

    [TestCase("C:\\", "test.mp3", "C:\\test.mp3")]
    [TestCase("C:\\Music", "test.mp3", "C:\\Music\\test.mp3")]
    [TestCase("C:\\", "Music\\test.mp3", "C:\\Music\\test.mp3")]
    [TestCase("C:\\Music", "Artist\\Album\\Track.mp3", "C:\\Music\\Artist\\Album\\Track.mp3")]
    [TestCase("\\\\A\\B\\C", "D\\test.mp3", "\\\\A\\B\\C\\D\\test.mp3")]
    [TestCase("\\\\A\\B\\C\\", "D\\test.mp3", "\\\\A\\B\\C\\D\\test.mp3")]
    public void GetFilepathFromId_AddsBaseDirectory(string baseDirectory, string id, string expectedResult)
    {
        // Arrange
        ConfigureBaseDirectory(baseDirectory);
        var sut = GetSubjectUnderTest();

        // Act
        var result = sut.GetFilepathFromId(id);

        // Assert
        Assert.That(result, Is.EqualTo(expectedResult));
    }

    [TestCase("C:\\", "C:\\test.mp3", "test.mp3")]
    [TestCase("C:\\Music", "C:\\Music\\test.mp3", "test.mp3")]
    [TestCase("C:\\", "C:\\Music\\test.mp3", "Music\\test.mp3")]
    [TestCase("C:\\Music", "C:\\Music\\Artist\\Album\\Track.mp3", "Artist\\Album\\Track.mp3")]
    [TestCase("\\\\A\\B\\C", "\\\\A\\B\\C\\D\\test.mp3", "D\\test.mp3")]
    [TestCase("\\\\A\\B\\C\\", "\\\\A\\B\\C\\D\\test.mp3", "D\\test.mp3")]
    public void GetIdFromFilepath_StripsBaseDirectory(string baseDirectory, string filepath, string expectedResult)
    {
        // Arrange
        ConfigureBaseDirectory(baseDirectory);
        var sut = GetSubjectUnderTest();

        // Act
        var result = sut.GetIdFromFilepath(filepath);

        // Assert
        Assert.That(result, Is.EqualTo(expectedResult));
    }

    private void ConfigureBaseDirectory(string baseDirectory)
    {
        var settings = new MusicLibrarySettings { Directory = baseDirectory };
        _mockSettingsOptions.Value.Returns(settings);
    }

    private FilepathParser GetSubjectUnderTest()
    {
        return new FilepathParser(_mockSettingsOptions);
    }
}