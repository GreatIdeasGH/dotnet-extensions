﻿using System.Text;

namespace GreatIdeas.Extensions;

public static class StringExtensions
{
    /// <summary>
    /// Generate AlphaNumeric characters with a speicified number of characters
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
        StringBuilder stringBuilder = new StringBuilder();
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

        if (converter.EndsWith('1'))
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

    public static string PositionSuffix(this int? value)
    {
        var converter = value!.ToString();

        if (value == 11 || value == 12 || value == 13)
        {
            return "th";
        }

        if (converter.EndsWith('1'))
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

    public static string PositionSuffix(this string value)
    {
        var parsedInt = int.Parse(value);
        var converter = int.Parse(value).ToString();

        if (parsedInt == 11 || parsedInt == 12 || parsedInt == 13)
        {
            return "th";
        }

        if (converter.EndsWith('1'))
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

    #endregion
}