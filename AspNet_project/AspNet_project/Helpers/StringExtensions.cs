using System;
using System.Linq;

namespace AspNet_project.Helpers
{
    public static class StringExtensions
    {
        public static string TruncateWords(this string input, int wordLimit)
        {
            if (string.IsNullOrWhiteSpace(input)) return string.Empty;

            var words = input.Split(' ');
            if (words.Length <= wordLimit) return input;

            return string.Join(" ", words.Take(wordLimit)) + "...";
        }
    }
}
