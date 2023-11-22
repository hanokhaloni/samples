namespace Calculators
{
    public class Calculator
    {
        private int numThreads;
        private int itemsCount;
        private int[,] threadResults;
        private int lastResult;

        public Calculator(int numThreads, int itemsCount = 2000)
        {
            this.numThreads = numThreads;
            this.itemsCount = itemsCount;
            this.threadResults = new int[numThreads, itemsCount];
        }

        public int Calculate()
        {
            CopyLastResult();
            int calculationResult = ParallelExecuteCalculation();
            if (calculationResult != -1)
            {
                PrepareResult(calculationResult);
            }
            return calculationResult;
        }

        private void CopyLastResult()
        {
            // Empty implementation as per your requirement
        }

        private int ParallelExecuteCalculation()
        {
            CancellationTokenSource cts = new CancellationTokenSource();
            cts.CancelAfter(TimeSpan.FromSeconds(2)); 

            try
            {
                Parallel.For(0, numThreads, new ParallelOptions { CancellationToken = cts.Token }, i =>
                {
                    for (int j = 0; j < itemsCount; j++)
                    {
                        Thread.Sleep(1); // Simulating a small unit of work
                        threadResults[i, j] = 1; // Example calculation: Each item counts as 1
                    }
                });
            }
            catch (OperationCanceledException ex)
            {
                //return -1; // Calculation took more than 1 second
                throw new Exception("Failing test - cancellation tokex expired", ex);

            }

            return SumAllItems(); // Sum of all items in the array
        }

        private int SumAllItems()
        {
            int totalSum = 0;
            for (int i = 0; i < numThreads; i++)
            {
                for (int j = 0; j < itemsCount; j++)
                {
                    totalSum += threadResults[i, j];
                }
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