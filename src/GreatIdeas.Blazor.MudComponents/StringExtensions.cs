using System;
using System.Linq;

namespace GreatIdeas.Blazor.MudComponents
{
    public static class StringExtensions
    {
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
    }
}
