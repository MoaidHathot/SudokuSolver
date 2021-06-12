using System;
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
            => Count == Length;

        public bool IsEmpty()
            => 0 == Count;

        public IEnumerable<int> GetExistingNumbers()
            => NumberIterator(index => _bits.Get(index));

        public IEnumerable<int> GetMissingNumbers()
            => NumberIterator(index => !_bits.Get(index));

        private IEnumerable<int> NumberIterator(Func<int, bool> filter)
        {
            for (var index = 0; index < _bits.Length; ++index)
            {
                if (filter(index))
                {
                    yield return index + 1;
                }
            }
        }

        private void Set(int number, bool @on)
        {
            var index = number - 1;
            var existed = _bits.Get(index);

            //Bit was already set and now it should be off
            if (existed && !@on)
            {
                _bits.Set(index, false);
                Count -= 1;
            } 
            //Bit was off and now it should be set
            else if (!existed && @on)
            {
                _bits.Set(index, true);
                Count += 1;
            }
        }
    }
}