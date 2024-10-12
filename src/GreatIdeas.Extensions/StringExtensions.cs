using System.Text;

namespace GreatIdeas.Extensions;

public static class StringExtensions
{
    /// <summary>
    /// Generate AlphaNumeric characters with a specified number of characters
    /// </summary>
    /// <param name="number">Number of characters</param>
    /// <returns>String</returns>
    public static string GenerateCode(int number = 6)
    {
        // Generate 8 character alphanumeric code
        var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        var random = new Random();
        var result = new string(
            Enumerable.Repeat(chars, number)
                .Select(s => s[random.Next(s.Length)])
                .ToArray());

        return result;
    }

    public static string GetInitials(this string text, string separator)
    {
        string[] output = text.Split(' ');
        string initials = string.Empty;

        var firstChar = text.Split(' ').Select(s => s[0]);

        foreach (var c in firstChar)
        {
            initials += c + separator;
        }

        return initials.Trim();
    }

    public static string InsertSpaceBeforeUpperCase(this string inputString)
    {
        StringBuilder stringBuilder = new();
        char ch = char.MinValue;
        inputString = inputString.Trim();
        foreach (char c in inputString)
        {
            if (char.IsUpper(c) && stringBuilder.Length != 0 && ch != ' ')
                stringBuilder.Append(' ');
            stringBuilder.Append(c);
            ch = c;
        }
        return stringBuilder.ToString();
    }

    #region Positional Suffix

    public static string PositionSuffix(this int value)
    {
        var converter = value.ToString();

        if (value == 11 || value == 12 || value == 13)
        {
            return "th";
        }

        return ConvertToSuffix(converter);
    }

    public static string PositionSuffix(this int? value)
    {
        var converter = value!.ToString();

        if (value == 11 || value == 12 || value == 13)
        {
            return "th";
        }

        return ConvertToSuffix(converter);
    }


    public static string PositionSuffix(this string value)
    {
        var parsedInt = int.Parse(value);
        var converter = int.Parse(value).ToString();

        if (parsedInt == 11 || parsedInt == 12 || parsedInt == 13)
        {
            return "th";
        }

        return ConvertToSuffix(converter);
    }

    #endregion

    public static string GetBytesReadable(this long i)
    {
        // Get absolute value
        long absolute_i = (i < 0 ? -i : i);
        // Determine the suffix and readable value
        string suffix;
        double readable;
        if (absolute_i >= 0x1000000000000000) // Exabyte
        {
            suffix = "EB";
            readable = (i >> 50);
        }
        else if (absolute_i >= 0x4000000000000) // Petabyte
        {
            suffix = "PB";
            readable = (i >> 40);
        }
        else if (absolute_i >= 0x10000000000) // Terabyte
        {
            suffix = "TB";
            readable = (i >> 30);
        }
        else if (absolute_i >= 0x40000000) // Gigabyte
        {
            suffix = "GB";
            readable = (i >> 20);
        }
        else if (absolute_i >= 0x100000) // Megabyte
        {
            suffix = "MB";
            readable = (i >> 10);
        }
        else if (absolute_i >= 0x400) // Kilobyte
        {
            suffix = "KB";
            readable = i;
        }
        else
        {
            return i.ToString("0 B"); // Byte
        }
        // Divide by 1024 to get fractional value
        readable = (readable / 1024);
        // Return formatted number with suffix
        return readable.ToString("0.### ") + suffix;
    }

    private static string ConvertToSuffix(string? converter)
    {
        if (converter!.EndsWith('1'))
        {
            return "st";
        }

        if (converter.EndsWith('2'))
        {
            return "nd";
        }

        if (converter.EndsWith('3'))
        {
            return "rd";
        }
        return "th";
    }
}