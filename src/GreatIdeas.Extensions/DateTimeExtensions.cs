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
        Random random = new Random();
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
        Random random = new Random();
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
    
    public static string PositionDate(DateTime value)
    {
        var converter = value!.Day.ToString();
        string position;
        string positionalDate;

        if (value.Day is 11 or 12 or 13)
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
}