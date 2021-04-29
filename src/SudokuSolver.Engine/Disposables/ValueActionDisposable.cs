using System;

namespace src.Disposables
{
    internal struct ValueActionDisposable : IDisposable
    {
        private readonly Action action;

        public ValueActionDisposable(Action action)
        {
            this.action = action;
        }

        public void Dispose()
            => action?.Invoke();
    }
}