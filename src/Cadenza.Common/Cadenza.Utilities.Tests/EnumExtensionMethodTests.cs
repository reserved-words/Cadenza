using Cadenza.Domain;
using NUnit.Framework;

namespace Cadenza.Utilities.Tests
{
    public class EnumExtensionMethodTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [TestCase(ReleaseType.Album, "Album")]
        [TestCase(ReleaseType.BestOf, "Best Of")]
        [TestCase(ReleaseType.VariousArtists, "Various Artists")]
        public void GetReleaseTypeDisplayName_ReturnsDisplayName(ReleaseType releaseType, string expectedResult)
        {
            // Arrange - N/A

            // Act
            var actualResult = releaseType.GetDisplayName();

            // Assert
            Assert.AreEqual(expectedResult, actualResult);
        }
    }
}