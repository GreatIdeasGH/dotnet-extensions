using System;
using Xunit;

namespace GreatIdeas.Extensions.Tests.Extensions;

public class DateTimeTests
{
    [Fact]
    public void DatePositionalSuffix()
    {
        var birthDate = new DateTime(2003, 1, 3);
        var positionalSuffix = DateTimeExtensions.PositionDate(birthDate);

        Assert.Equal("3rd January, 2003", positionalSuffix);
    }
}