using SystemMonitor.Api.Extensions;

namespace SystemMonitor.Test;

[TestFixture]
public class EnumExtensionsTests
{
    public enum TestEnum
    {
        First = 1,
        Second = 2,
        Third = 3
    }

    [Test]
    public void GetNext_NormalCase_ReturnsNext()
    {
        var result = TestEnum.First.GetNext();
        Assert.That(result, Is.EqualTo(TestEnum.Second));
    }

    [Test]
    public void GetNext_LastValue_ReturnsLast()
    {
        var result = TestEnum.Third.GetNext();
        Assert.That(result, Is.EqualTo(TestEnum.Third));
    }

    [Test]
    public void GetPrevious_NormalCase_ReturnsPrevious()
    {
        var result = TestEnum.Third.GetPrevious();
        Assert.That(result, Is.EqualTo(TestEnum.Second));
    }

    [Test]
    public void GetPrevious_FirstValue_ReturnsFirst()
    {
        var result = TestEnum.First.GetPrevious();
        Assert.That(result, Is.EqualTo(TestEnum.First));
    }

    [Test]
    public void GetNext_And_GetPrevious_AreInverse()
    {
        var start = TestEnum.Second;
        var next = start.GetNext();
        var prev = next.GetPrevious();
        Assert.That(prev, Is.EqualTo(start));
    }
}
