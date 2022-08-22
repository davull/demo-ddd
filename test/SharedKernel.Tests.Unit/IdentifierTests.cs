using FluentAssertions;
using Xunit;

namespace SharedKernel.Tests.Unit;

public class IdentifierTests
{
    [Fact]
    public void SameIdentifierShouldBeEqual()
    {
        // Arrange
        var id1 = new Identifier<string>(IdentifiableFeature.Gtin, "1234567890123");
        var id2 = new Identifier<string>(IdentifiableFeature.Gtin, "1234567890123");

        // Act
        var result = id1.Equals(id2);

        // Assert
        result.Should().BeTrue();
    }

    [Fact]
    public void DifferentFeatureShouldNotBeEqual()
    {
        // Arrange
        var id1 = new Identifier<string>(IdentifiableFeature.Gtin, "1234567890123");
        var id2 = new Identifier<string>(IdentifiableFeature.Unknown, "1234567890123");

        // Act
        var result = id1.Equals(id2);

        // Assert
        result.Should().BeFalse();
    }

    [Fact]
    public void DifferentValueShouldNotBeEqual()
    {
        // Arrange
        var id1 = new Identifier<string>(IdentifiableFeature.Gtin, "1234");
        var id2 = new Identifier<string>(IdentifiableFeature.Gtin, "0000");

        // Act
        var result = id1.Equals(id2);

        // Assert
        result.Should().BeFalse();
    }

    [Fact]
    public void EmptyShouldBeEmpty()
    {
        // Arrange
        var id1 = Identifier<string>.Empty;
        var id2 = Identifier<string>.Empty;

        // Act
        var result = id1.Equals(id2);

        // Assert
        result.Should().BeTrue();
    }
}