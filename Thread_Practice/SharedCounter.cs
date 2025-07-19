namespace Thread_Practice;

internal class SharedCounter
{
    private int _count = 0;
    private readonly object _lock = new();

    public void IncrementWithLock()
    {
        lock (_lock)
            _count++;
    }

    public void IncrementWithMonitor()
    {
        bool lockTaken = false;
        try
        {
            Monitor.Enter(_lock, ref lockTaken);
            _count++;
        }
        finally
        {
            if (lockTaken)
                Monitor.Exit(_lock);
        }
    }

    public void IncrementWithInterlocked()
    {
        Interlocked.Increment(ref _count);
    }

    public int Count => _count;
}
