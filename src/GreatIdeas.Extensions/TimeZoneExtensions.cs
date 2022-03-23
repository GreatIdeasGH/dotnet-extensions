namespace GreatIdeas.Extensions;

public static class TimeZoneExtensions
{
    public static DateTime ConvertToTimeZoneDateTimeUtc(string timeZoneId, DateTime input)
    {
        if (string.IsNullOrEmpty(timeZoneId))
        {
            throw new ArgumentNullException(nameof(timeZoneId));
        }

        var timezoneInfo = TimeZoneInfo.FindSystemTimeZoneById(timeZoneId);
        var dateTimeOffset = new DateTimeOffset(input);
        var endDateWithZone = TimeZoneInfo.ConvertTimeToUtc(dateTimeOffset.DateTime, timezoneInfo);
        var newDateTime = DateTime.SpecifyKind(endDateWithZone, DateTimeKind.Utc);
        return newDateTime;
    }

    public static DateTime ConvertToTimeZoneDateTimeUtc(TimeZoneInfo timeZoneInfo, DateTime input)
    {
        if (timeZoneInfo == null)
        {
            throw new ArgumentNullException(nameof(timeZoneInfo));
        }

        var dateTimeOffset = new DateTimeOffset(input);
        var endDateWithZone = TimeZoneInfo.ConvertTimeToUtc(dateTimeOffset.DateTime, timeZoneInfo);
        var newDateTime = DateTime.SpecifyKind(endDateWithZone, DateTimeKind.Utc);
        return newDateTime;
    }
}