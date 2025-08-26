using System.Diagnostics;
using System.Text;

namespace Thread_Practice;

public class FileComparison
{
    const string FilePath = "test.txt";

    public static void Run()
    {
        if (!File.Exists(FilePath))
        {
            var sb = new StringBuilder();
            for (int i = 0; i < 1_000_000_000; i++)
            {
                sb.AppendLine($"Line {i}");
            }
            File.WriteAllText(FilePath, sb.ToString());
        }

        Measure("File.ReadAllText", () =>
        {
            var text = File.ReadAllText(FilePath);
        });

        Measure("FileStream + StreamReader", () =>
        {
            using var fs = new FileStream(FilePath, FileMode.Open, FileAccess.Read, FileShare.Read);
            using var reader = new StreamReader(fs);
            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine();
            }
        });

        Measure("StreamReader only", () =>
        {
            using var reader = new StreamReader(FilePath);
            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine();
            }
        });

        Measure("File.ReadLines (LINQ)", () =>
        {
            foreach (var line in File.ReadLines(FilePath))
            {
                var dummy = line;
            }
        });
    }

    static void Measure(string title, Action action)
    {
        var sw = Stopwatch.StartNew();
        action();
        sw.Stop();
        Console.WriteLine($"{title.PadRight(30)} : {sw.ElapsedMilliseconds} ms");
    }
}
