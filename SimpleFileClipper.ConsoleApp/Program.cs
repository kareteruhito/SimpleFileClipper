using Reactive.Bindings;

namespace SimpleFileClipper;

class Program
{
    static void Main()
    {
        string className = SimpleFileClipper.Lib.Class1.CLASS_NAME;
        Console.WriteLine($"ClassName:{className}");
        
        ReactiveCollection<string> List = [];
    }
}
