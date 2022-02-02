using Xunit;

namespace GreatIdeas.Extensions.Tests.Extensions;

public class StringTests
{
    // Add whitespace after every proper case word test passes
    [Fact]
    public void InsertWhitespaceAfterEveryProperCaseWordTestPasses()
    {
        var testString = "ThisIsATest.";
        var expectedString = "This Is A Test.";

        var actualString = testString.InsertSpaceBeforeUpperCase();

        Assert.Equal(expectedString, actualString);
    }
    
    [Fact]
    public void InsertWhitespaceAfterEveryCaseWordTestNotEqual()
    {
        var testString = "Thisisatest.";
        var expectedString = "This is a test.";

        var actualString = testString.InsertSpaceBeforeUpperCase();

        Assert.NotEqual(expectedString, actualString);
    }
    
    // Add number position suffix
    [Fact]
    public void St_NumberSuffixTestPasses()
    {
        var testString = 1;
        var expectedSuffix = "st";

        var actualString = testString.PositionSuffix();

        Assert.Equal(expectedSuffix, actualString);
    }
    
    [Fact]
    public void Nd_NumberSuffixTestPasses()
    {
        int? testString = 2;
        var expectedSuffix = "nd";

        var actualString = testString.PositionSuffix();

        Assert.Equal(expectedSuffix, actualString);
    }
    
    [Fact]
    public void Rd_NumberSuffixTestPasses()
    {
        var testString = "3";
        var expectedSuffix = "rd";

        var actualString = testString.PositionSuffix();

        Assert.Equal(expectedSuffix, actualString);
    }
}