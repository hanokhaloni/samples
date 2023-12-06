using System;
using System.Threading;

namespace Calculators
{
    public class Calculator2
    {
        private int numThreads;
        private int sleepInterval;
        private int[] threadResults;
        private int lastResult;
        private Thread[] threads;

        public Calculator2(int numThreads, int sleepInterval = 500)
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
            Console.WriteLine("Starting ParallelExecuteCalculation()");
            CancellationTokenSource cts = new CancellationTokenSource();
            cts.CancelAfter(TimeSpan.FromMilliseconds(520));
            ManualResetEvent[] doneEvents = new ManualResetEvent[numThreads];

            try
            {
                for (int i = 0; i < numThreads; i++)
                {
                    int localI = i; // Correctly capture the loop variable
                    doneEvents[localI] = new ManualResetEvent(false);
                    ThreadPool.QueueUserWorkItem(new WaitCallback((object state) =>
                    {
                        try
                        {
                            Console.WriteLine($"Starting WorkerThread-{localI}");

                            if (cts.Token.IsCancellationRequested)//TODO bad implementation!!!!
                            {
                                throw new OperationCanceledException(cts.Token);
                            }

                            Thread.Sleep(sleepInterval); // Simulating a small unit of work
                            threadResults[localI] = 5; // Each thread works on its own part of the array

                            Console.WriteLine($"Finishing WorkerThread-{localI} with result threadResult[{localI}]={threadResults[localI]}");
                        }
                        finally
                        {
                            doneEvents[localI].Set();
                        }
                    }));
                }

                WaitHandle.WaitAll(doneEvents);
                Console.WriteLine("Ended ParallelExecuteCalculation()");
            }
            catch (OperationCanceledException ex)
            {
                throw new Exception("Failing test - cancellation token expired", ex);
            }
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