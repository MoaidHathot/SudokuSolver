using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuSolver.Engine.Extensions
{
    public static class BitArrayExtensions
    {
        public static IEnumerable<int> GetExistingNumbers(this BitArray array)
        {
            for (var index = 0; index < array.Length; ++index)
            {
                if (array.Get(index))
                {
                    yield return index + 1;
                }
            }
        }

        public static IEnumerable<int> GetMissingNumbers(this BitArray array)
        {
            for (var index = 0; index < array.Length; ++index)
            {
                if (!array.Get(index))
                {
                    yield return index + 1;
                }
            }
        }
    }
}
