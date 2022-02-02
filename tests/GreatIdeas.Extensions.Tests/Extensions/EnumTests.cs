using System;
using System.Text;
using Xunit;

namespace GreatIdeas.Extensions.Tests.Extensions;

public class EnumTests
{
    // Get description of enum value
    [Fact]
    public void StatusDescriptionIsEqual()
    {
        var colorEnumsString = GetEnumsDescriptions();

        Assert.Equal("Active|Completed|Transferred|Stopped|Suspended|Dismissed|", colorEnumsString);
    }
    
    private static string GetEnumsDescriptions()
    {
        StringBuilder stringBuilder = new StringBuilder();
        foreach (StatusEnum statusEnum in Enum.GetValues(typeof(StatusEnum)))
        {
            stringBuilder.Append(statusEnum.GetDescription() + "|");
        }
        return stringBuilder.ToString();
    }
}