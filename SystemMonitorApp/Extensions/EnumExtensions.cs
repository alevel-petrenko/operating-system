using System.Diagnostics;
using SystemMonitor.Api.Models;

namespace SystemMonitor.Api.Extensions;

public static class EnumExtensions
{
    public static ProcessPriority GetNext(this ProcessPriority value)
    {
        var allValues = (ProcessPriority[])Enum.GetValues(typeof(ProcessPriority));
        int currentIndex = Array.IndexOf(allValues, value);

        if (currentIndex + 1 < (int)ProcessPriority.RealTime)
            return allValues[currentIndex + 1];
        else
            return allValues[currentIndex];
    }

    public static ProcessPriority GetPrevious(this ProcessPriority value)
    {
        var allValues = (ProcessPriority[])Enum.GetValues(typeof(ProcessPriority));
        int currentIndex = Array.IndexOf(allValues, value);

        if (currentIndex - 1 >= 0)
            return allValues[currentIndex - 1];
        else
            return allValues[currentIndex];
    }

    public static T GetNext<T>(this T value) where T : Enum
    {
        var values = (T[])Enum.GetValues(typeof(T));
        int index = Array.IndexOf(values, value);

        return values[(index + 1) % values.Length];
    }

    public static T GetPrevious<T>(this T value) where T : Enum
    {
        var values = (T[])Enum.GetValues(typeof(T));
        int index = Array.IndexOf(values, value);

        return values[(index - 1 + values.Length) % values.Length];
    }
}
