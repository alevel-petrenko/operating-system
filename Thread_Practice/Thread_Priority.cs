namespace Thread_Practice;

internal class Thread_Priority
{
    int counter1 = 0;
    int counter2 = 0;

    public void Run()
    {
        Thread t1 = new(() =>
        {
            while (true) 
                counter1++;
        });
        t1.Priority = ThreadPriority.Lowest;

        Thread t2 = new(() =>
        {
            while (true) 
                counter2++;
        });
        t2.Priority = ThreadPriority.Highest;

        t1.Start();
        t2.Start();

        Thread.Sleep(2000);
        Console.WriteLine($"Low: {FormatValue(counter1)}, High: {FormatValue(counter2)}");
    }

    private static string FormatValue(int number)
    {
        return number.ToString("N0", new System.Globalization.CultureInfo("en-US"));
    }
}
