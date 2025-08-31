namespace Thread_Practice;

internal class Value_Ref_Types
{
    class Box { public int Value; }
    struct Point { public int X; }

    public void Start()
    {
        Console.WriteLine("=== Value Types ===");
        DoValueExample1();
        DoValueExample2();
        DoValueExample3();
        DoValueExample4();
        Console.WriteLine("\n=== Reference Types ===");
        DoRefExample1();
        DoRefExample2();
        DoRefExample3();
    }

    internal static void DoValueExample1()
    {
        Point pt = new() { X = 5 };
        static void Move(Point p) { p.X += 10; }
        Move(pt);

        Console.WriteLine(pt.X);  // 5
    }

    internal static void DoValueExample2()
    {
        Point pt = new() { X = 5 };
        static void Move(ref Point p) { p.X += 10; }

        Move(ref pt);
        Console.WriteLine(pt.X);  // 15
    }

    internal static void DoValueExample3()
    {
        Point pt = new() { X = 5 };
        static void Move(ref Point p) { p = new() { X = 100 }; }

        Move(ref pt);
        Console.WriteLine(pt.X);  // 100
    }

    internal static void DoValueExample4()
    {
        Point pt = new() { X = 5 };
        static void Move(Point p) { p = new() { X = 100 }; }

        Move(pt);
        Console.WriteLine(pt.X);  // 5
    }

    internal static void DoRefExample1()
    {
        static void Change(Box b) { b.Value += 1; }

        Box box = new() { Value = 10 };
        Change(box);
        Console.WriteLine(box.Value);  // 11
    }

    internal static void DoRefExample2()
    {
        static void Replace(ref Box b) { b = new Box { Value = 100 }; }

        Box box = new() { Value = 10 };
        Replace(ref box);
        Console.WriteLine(box.Value);  // 100
    }

    internal static void DoRefExample3()
    {
        static void Change(Box b) { b = new() { Value = 123 }; }

        Box box = new() { Value = 10 };
        Change(box);
        Console.WriteLine(box.Value);  // 10
    }
}
