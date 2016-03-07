using System;

namespace CoinsApplication.Extensions
{
    public static class StringExtensions
    {
        public static bool Contains(this string source, string toCheck, StringComparison comp)
        {
            return toCheck != null && source.IndexOf(toCheck, comp) >= 0;
        }
    }
}
