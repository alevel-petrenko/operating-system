namespace Thread_Practice;

internal class Thread_Task
{
    public void ThreadMethod()
    {
        Utility.PrintWithTime($"[Thread] Start | ThreadId: {Thread.CurrentThread.ManagedThreadId}");
        Thread.Sleep(1000);
        Utility.PrintWithTime($"[Thread] End   | ThreadId: {Thread.CurrentThread.ManagedThreadId}");
    }

    public void TaskMethod()
    {
        Utility.PrintWithTime($"[Task] Start   | ThreadId: {Thread.CurrentThread.ManagedThreadId}");
        Thread.Sleep(1000);
        Utility.PrintWithTime($"[Task] End     | ThreadId: {Thread.CurrentThread.ManagedThreadId}");
    }
}
