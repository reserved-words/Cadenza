using Cadenza.Common.Utilities.Services;

namespace Cadenza.Common.Utilities.Tests;

public class NameComparerTests
{
    [TestCase(null, "")]
    [TestCase("", "")]
    [TestCase("The Divine Comedy", "divine comedy")]
    [TestCase("BOYS AND GIRLS", "boys and girls")]
    [TestCase("The Boys & Girls", "boys and girls")]
    [TestCase("the boys and girls", "boys and girls")]
    [TestCase("K.I.S.S.", "k.i.s.s.")]
    public void GetCompareName_ReturnsStandardisedName(string name, string expectedCompareName)
    {
        // Arrange
        var sut = new NameComparer();

        // Act
        var result = sut.GetCompareName(name);

        // Assert
        Assert.That(result, Is.EqualTo(expectedCompareName));
    }
}