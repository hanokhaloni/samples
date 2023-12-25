using System;
using System.Threading;

namespace Calculators
{
    public class Calculator1
    {
        string customFormat = "dd/MM/yyyyHH:mm:ss.fff";

        private int numThreads;
        private int sleepInterval;
        private int[] threadResults;
        private int lastResult;
        private Thread[] threads;

        public Calculator1(int numThreads, int sleepInterval = 500)
        {
            this.numThreads = numThreads;
            this.sleepInterval = sleepInterval;
            this.threadResults = new int[numThreads];
            threads = new Thread[numThreads];
        }

        public int Calculate()
        {
            CopyLastResult();
            ParallelExecuteCalculation();
            int calculationResult = SumAllItems();
            PrepareResult(calculationResult);
            return lastResult;
        }

        private void CopyLastResult()
        {
            // Empty implementation as per your requirement
        }

        private void ParallelExecuteCalculation()
        {
            Console.WriteLine("Stating ParallelExecuteCalculation()");
            CancellationTokenSource cts = new CancellationTokenSource();
            cts.CancelAfter(TimeSpan.FromMilliseconds(520));

            try
            {
                Console.WriteLine($"{DateTime.Now.ToString(customFormat)} Starting creating threads");
                for (int i = 0; i < numThreads; i++)
                {
                    ///Console.WriteLine($"Loop {i}");

                    int localI = i; // Correctly capture the loop variable
                    threads[localI] = new Thread(() =>
                    {
                        Thread.CurrentThread.Name = $"WorkerThread-{localI}";
                        Console.WriteLine($"Starting {Thread.CurrentThread.Name}");

                        if (cts.Token.IsCancellationRequested)
                        {
                            throw new OperationCanceledException(cts.Token);
                        }


                        Thread.Sleep(sleepInterval); // Simulating a small unit of work
                        threadResults[localI] = 5; // Each thread works on its own part of the array

                        Console.WriteLine($"Finishing {Thread.CurrentThread.Name} with result threadResult[{localI}]={threadResults[localI]}");
                    });

                }
                Console.WriteLine($"{DateTime.Now.ToString(customFormat)} Starting running threads");
                for (int i = 0; i < numThreads; i++)
                {

                    threads[i].Start();
                }

                Console.WriteLine($"{DateTime.Now.ToString(customFormat)} Starting wait for result");
                foreach (Thread thread in threads)
                {
                    thread.Join(); // Wait for each thread to complete
                }
                Console.WriteLine($"{DateTime.Now.ToString(customFormat)} End wait for result");

            }
            catch (OperationCanceledException ex)
            {
                //return -1; // Calculation took more than 1 second
                throw new Exception("Failing test - cancellation tokex expired", ex);

            }

            return; // Sum of all items in the array
        }

        private int SumAllItems()
        {
            int totalSum = 0;
            for (int i = 0; i < numThreads; i++)
            {
                    totalSum += threadResults[i];
            }
            return totalSum;
        }

        private void PrepareResult(int result)
        {
            // Process the result as needed
            lastResult = result;
        }
    }
}