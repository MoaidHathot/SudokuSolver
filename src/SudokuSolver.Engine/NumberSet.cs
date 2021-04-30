using System.Collections;
using System.Collections.Generic;
using SudokuSolver.Engine.Extensions;

namespace SudokuSolver.Engine
{
    internal class NumberSet
    {
        private readonly BitArray _bits;

        public int Length => _bits.Length;
        public int Count { get; private set; }

        public NumberSet(int length)
            => _bits = new BitArray(length);

        public IEnumerable<int> ExistingNumbers()
            => GetExistingNumbers();

        public IEnumerable<int> MissingNumbers()
            => GetMissingNumbers();

        public void Add(int number)
            => Set(number, true);

        public void Remove(int number)
            => Set(number, false);

        public bool IsExist(int number)
            => _bits.Get(number - 1);

        public bool IsMissing(int number)
            => !IsExist(number);

        public bool IsFull()
            => GetMissingNumbers().None();

        public bool IsEmpty()
            => GetExistingNumbers().None();

        public IEnumerable<int> GetExistingNumbers()
        {
            for (var index = 0; index < _bits.Length; ++index)
            {
                if (_bits.Get(index))
                {
                    yield return index + 1;
                }
            }
        }

        public IEnumerable<int> GetMissingNumbers()
        {
            for (var index = 0; index < _bits.Length; ++index)
            {
                if (!_bits.Get(index))
                {
                    yield return index + 1;
                }
            }
        }

        private void Set(int number, bool exist)
        {
            _bits.Set(number - 1, exist);
            Count += exist ? 1 : -1;
        }
    }
}