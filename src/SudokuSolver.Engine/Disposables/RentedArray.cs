using System;
using System.Buffers;

namespace src.Disposables
{
    internal struct RentedArray<T> : IDisposable
    {
        public int Length { get; }
        private T?[] _array;

        public RentedArray(int length, bool init = true, T? defaultValue = default)
        {
            _array = ArrayPool<T>.Shared.Rent(length);
            Length = length;

            if(init)
            {
                Clear(defaultValue);
            }
        }

        public T? this[int index]
        {
            get => _array[index];
            set => _array[index] = value;
        }

        private void Clear(T? defaultValue = default)
        {
            for(var index = 0; index < Length; ++index)
            {
                _array[index] = defaultValue;
            }
        }

        public void Dispose()
        {
            if(_array is not null)
            {
                ArrayPool<T>.Shared.Return(_array!);
            }
        }
    }
}