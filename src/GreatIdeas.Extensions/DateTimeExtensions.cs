namespace GreatIdeas.Extensions;

public static class DateTimeExtensions
{
    /// <summary>
    /// Generates Random Dates with a start date and ending date with DateOnly type.
    /// </summary>
    /// <param name="startDate"></param>
    /// <param name="endDate"></param>
    /// <returns></returns>
    public static DateOnly GenerateRandomDateOnly(DateOnly startDate, DateOnly endDate)
    {
        Random random = new();
        int range;
        if (startDate > endDate)
        {
            range = startDate.DayNumber - endDate.DayNumber;
            // range = ((TimeSpan)(startDate - endDate)).Days;
        }
        else
        {
            range = endDate.DayNumber - startDate.DayNumber;
            // range = ((TimeSpan)(endDate - startDate)).Days;
        }

        return startDate.AddDays(random.Next(range));
    }

    /// <summary>
    /// Generates Random DateTime with a start date and ending date with DateTime type.
    /// </summary>
    /// <param name="startDate"></param>
    /// <param name="endDate"></param>
    /// <returns></returns>
    public static DateTime GenerateRandomDateTime(DateTime startDate, DateTime endDate)
    {
        Random random = new();
        int range;
        if (startDate > endDate)
        {
            range = (startDate - endDate).Days;
        }
        else
        {
            range = (endDate - startDate).Days;
        }

        return startDate.AddDays(random.Next(range));
    }



    public static string PositionDate(DateTime? value)
    {
        var converter = value!.Value.Day.ToString();
        string position;
        string positionalDate;

        if (value.Value.Day is 11 or 12 or 13)
        {
            position = $"th";
            positionalDate = GetDate(value, position);
            return positionalDate;
        }

        if (converter.EndsWith('1'))
        {
            position = $"st";
            positionalDate = GetDate(value, position);
            return positionalDate;
        }

        if (converter.EndsWith('2'))
        {
            position = $"nd";
            positionalDate = GetDate(value, position);
            return positionalDate;
        }

        if (converter.EndsWith('3'))
        {
            position = $"rd";
            positionalDate = GetDate(value, position);
            return positionalDate;
        }

        position = $"th";
        positionalDate = GetDate(value, position);
        return positionalDate;
    }

    public static string PositionSuffix(DateTime value)
    {
        var converter = value!.Day.ToString();
        string position;
        string suffix;

        if (value.Day is 11 or 12 or 13)
        {
            position = $"th";
            suffix = GetDate(value, position);
            return suffix;
        }

        if (converter.EndsWith('1'))
        {
            position = $"st";
            suffix = GetDate(value, position);
            return suffix;
        }

        if (converter.EndsWith('2'))
        {
            position = $"nd";
            suffix = GetDate(value, position);
            return suffix;
        }

        if (converter.EndsWith('3'))
        {
            position = $"rd";
            suffix = GetDate(value, position);
            return suffix;
        }

        position = $"th";
        suffix = GetDate(value, position);
        return suffix;
    }

    private static string GetDate(DateTime? date, string position)
    {
        // Get the first 2 characters of the value
        var day = date!.Value.Day;

        // Append the position
        var positionalDay = $"{day}{position}";

        // Compose complete date
        var monthYear = date.Value.ToString("MMMM, yyyy");
        var finalDate = $"{positionalDay} {monthYear}";

        return finalDate;
    }

    public static int GetCurrentAge(this DateTime dateTime)
    {
        var currentDate = DateTime.UtcNow;
        int age = currentDate.Year - dateTime.Year;

        if (currentDate < dateTime.AddYears(age))
        {
            age--;
        }

        return age;
    }

    public static int DatePeriod(this DateTime date)
    {
        DateTimeOffset now = DateTimeOffset.UtcNow;
        ;
        int years = new DateTime(DateTimeOffset.Now.Subtract(date).Ticks).Year - 1;
        DateTimeOffset pastYearDate = date.AddYears(years);
        int months = 0;
        for (int i = 1; i <= 12; i++)
        {
            if (pastYearDate.AddMonths(i) == now)
            {
                months = i;
                break;
            }
            else if (pastYearDate.AddMonths(i) >= now)
            {
                months = i - 1;
                break;
            }
        }

        int Days = now.Subtract(pastYearDate.AddMonths(months)).Days;
        int Hours = now.Subtract(pastYearDate).Hours;
        int Minutes = now.Subtract(pastYearDate).Minutes;
        int Seconds = now.Subtract(pastYearDate).Seconds;
        // return String.Format("Age: {0} Year(s) {1} Month(s) {2} Day(s) {3} Hour(s) {4} Second(s)",
        //     Years, Months, Days, Hours, Seconds);
        return years;
    }

    public static bool ValidateAge(DateTime entryDate, int ageLimit)
    {
        DateTime now = DateTime.Today;

        int age = now.Year - Convert.ToDateTime(entryDate).Year;

        if (age < ageLimit)
        {
            return false;
        }
        else
        {
            return true;
        }
    }


}