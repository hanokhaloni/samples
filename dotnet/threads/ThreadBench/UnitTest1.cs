
using Microsoft.VisualStudio.TestPlatform.TestHost;
using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace ThreadBench
{
    [TestClass]
    public class BenchmarkTests
    {
        [TestMethod]
        [Ignore]
        public void RunBenchmark()
        {
            //var summary = BenchmarkRunner.Run<CalculatorBenchmark>();

            // Optional: Assert something about the benchmark results if needed
            // This can be challenging as benchmark results can vary greatly based on the environment
        }
        [TestMethod]
        //[DataRow(1,1000)]
        //[DataRow(2, 500)]
        //[DataRow(4, 250)]
        [DataRow(8, 125)]
        [DataRow(10, 100)]
        [DataRow(20, 50)]
        public void test1000_1(int numThreads, int intemsCount)
        {
            var stopwatch = new Stopwatch();

            stopwatch.Start();
            Calculators.Calculator calculator = new Calculators.Calculator(numThreads, intemsCount);
            calculator.Calculate();
            stopwatch.Stop();

            var executionTime = stopwatch.ElapsedMilliseconds;

            Console.WriteLine($"Execution Time: {executionTime} ms");

            Assert.IsTrue(executionTime < 2000); // Example: assert that execution time is less than 1000ms
        }
    }


}