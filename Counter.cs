using System.Threading;

namespace life
{
    /// <summary>
    /// Потокобезопасный счётчик
    /// </summary>
    public class Counter
    {
        private int _counter;
        public int Count
        {
            get => Interlocked.CompareExchange(ref _counter, 0, 0);
            set => Interlocked.Exchange(ref _counter, value);
        }
    }
}
