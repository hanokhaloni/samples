using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using Calculators;

public class Program
{
    public static void Main()
    {
        BenchmarkRunner.Run<CalculatorBenchmark>();
    }
}



[MemoryDiagnoser] // Optional: This attribute is used for memory profiling
public class CalculatorBenchmark
{
    private Calculator calculator;

    [GlobalSetup]
    public void Setup()
    {
        calculator = new Calculator(5); // Example setup
    }

    [Benchmark]
    public void CalculateBenchmark()
    {
        calculator.Calculate();
    }
}