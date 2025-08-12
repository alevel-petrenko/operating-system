using System.Diagnostics;
using System.Text;

namespace SystemMonitor.Api.Services;

public class ProcessEnvironmentReader : IEnvironmentService
{
    private const string SystemInformerPath = @"C:\Program Files\SystemInformer\SystemInformer.exe";

    public Dictionary<string, string> GetEnvironmentVariablesForProcess(int processId)
    {
        string arguments = $"-c \"process {processId} environment\"";

        ProcessStartInfo psi = new ()
        {
            FileName = SystemInformerPath,
            Arguments = arguments,
            RedirectStandardOutput = true,
            UseShellExecute = false,
            CreateNoWindow = true,
            StandardOutputEncoding = Encoding.UTF8
        };

        Process proc = new() { StartInfo = psi };
        proc.Start();

        string output = proc.StandardOutput.ReadToEnd();
        proc.WaitForExit();

        return ParseEnvironmentVariables(output);
    }

    static Dictionary<string, string> ParseEnvironmentVariables(string rawOutput)
    {
        var dict = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
        var lines = rawOutput.Split(['\r', '\n'], StringSplitOptions.RemoveEmptyEntries);

        foreach (var line in lines)
        {
            int idx = line.IndexOf('=');
            if (idx > 0)
            {
                string key = line.Substring(0, idx).Trim();
                string value = line.Substring(idx + 1).Trim();
                dict[key] = value;
            }
        }
        return dict;
    }
}
