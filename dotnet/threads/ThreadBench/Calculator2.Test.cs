
using Microsoft.VisualStudio.TestPlatform.TestHost;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace ThreadBench
{
    [TestClass]
    public class Calculator2Test
    {
        [TestMethod]
        [DataRow(2,  500)]
        [DataRow(1, 1000)]
        [DataRow(4,  500)]
        [DataRow(8,  500)]
        [DataRow(16, 500)]
        [DataRow(32, 500)]
        [DataRow(64, 500)]
        [DataRow(128,500)]

        public void calculator2(int numThreads, int sleepPerThread)
        {
            var stopwatch = new Stopwatch();

            Calculators.Calculator1 calculator = new Calculators.Calculator1(numThreads, sleepPerThread);
            stopwatch.Start();
            int result = calculator.Calculate();
            stopwatch.Stop();

            var executionTime = stopwatch.ElapsedMilliseconds;

            Console.WriteLine($"Execution Time: {executionTime} ms");
            Assert.AreEqual(5 * numThreads, result);
            Assert.IsTrue(executionTime < 2000); // Example: assert that execution time is less than 1000ms
        }
    }


}