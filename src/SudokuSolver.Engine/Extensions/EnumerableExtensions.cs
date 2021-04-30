using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace SudokuSolver.Engine.Extensions
{
    public static class EnumerableExtensions
    {
        public static void ForEach<T>(this IEnumerable<T> source, Action<T> action)
        {
            foreach (var item in source)
            {
                action(item);
            }
        }

        public static bool None<T>(this IEnumerable<T> source)
            => !source.Any();

        public static bool None<T>(this IEnumerable<T> source, Func<T, bool> predicate)
            => !source.Any(predicate);
    }
}
