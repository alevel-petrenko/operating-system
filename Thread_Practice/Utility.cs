namespace Thread_Practice;

public static class Utility
{
    public static void PrintWithTime(string message)
    {
        Console.WriteLine($"{DateTime.Now:HH:mm:ss.fff} - {message}");
    }
}
