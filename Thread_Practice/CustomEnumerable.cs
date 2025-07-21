using System.Collections;

namespace Thread_Practice;

internal class CustomEnumerable<T>(T[] items) : IEnumerable<T>
{
    public IEnumerator<T> GetEnumerator()
    {
        for (int i = 0; i < items.Length; i++)
        {
            yield return items[i];
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}
