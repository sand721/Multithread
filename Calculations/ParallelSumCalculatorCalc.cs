using Multithreaded.Base;

namespace Multithreaded.Calculations;

public class ParallelSumCalculatorCalc : BaseSumCalculatorCalc
{
    public override string GetName()=> "Параллельное";
    public override long CalculateSum(int[] array)
    {
        var threads = new List<Thread>();
        var threadCount = Environment.ProcessorCount;
        var partSize = array.Length / threadCount;
        long sum = 0;
        for (var i = 0; i < threadCount; i++)
        {
            var start = i * partSize;
            var end = start + partSize;
            if (i == threadCount - 1)
            {
                end =array.Length;
            }
            var thr = new Thread(() =>
            {
                var localSum = 0;
                for (var j = start; j < end; j++)
                {
                    localSum += array[j];
                }
                Interlocked.Add(ref sum, localSum);
            });
            threads.Add(thr);
        }
        foreach (var thread in threads)
        {
            thread.Start();
        }
        foreach (var thread in threads)
        {
            thread.Join();
        }
        return sum;
    }
}