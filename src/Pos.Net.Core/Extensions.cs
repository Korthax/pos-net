using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Pos.Net.Core
{
    public static class Extensions
    {
        public static string RegexReplace(this string item, string pattern, string replacement)
        {
            return Regex.Replace(item, pattern, replacement, RegexOptions.IgnoreCase);
        }

        public static List<T> RemoveWhere<T>(this IEnumerable<T> collection, Predicate<T> predicate)
        {
            return collection.Where(x => !predicate(x)).ToList();
        }

        public static IEnumerable<List<string>> Split(this IEnumerable<string> list, string[] delimiters)
        {
            var result = new List<List<string>>();
            var current = new List<string>();

            foreach (var item in list)
            {
                if (!delimiters.Contains(item))
                    current.Add(item);
                else
                {
                    if (current.Count > 0)
                        result.Add(current);

                    current = new List<string>();
                }
            }

            if (current.Count > 0)
                result.Add(current);

            return result;
        }
    }
}
