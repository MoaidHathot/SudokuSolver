using System.Collections;
using System.Collections.Generic;

namespace SudokuSolver.Engine
{
    internal class NumberSet
    {
        private readonly BitArray _bits;

        public int Length => _bits.Length;

        public NumberSet(int length)
            => _bits = new BitArray(length);

        public void Add(int number)
            => Set(number, true);

        public void Remove(int number)
            => Set(number, false);

        public bool Exist(int number)
            => _bits.Get(number - 1);

        public bool Missing(int number)
            => !Exist(number);

        private void Set(int number, bool exist)
            => _bits.Set(number - 1, exist);
    }
}