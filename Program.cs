int[] sizes = { 100000, 1000000, 10000000 };
foreach (var size in sizes)
{
    int[] array = CreateParallelRandomArray(size);

    Console.WriteLine($"Array Size: {size}");

    // Sequential Sum
    var watch = System.Diagnostics.Stopwatch.StartNew();
    long sequentialSum = SequentialSum(array);
    watch.Stop();
    Console.WriteLine($"Sequential Sum: {sequentialSum}, Time: {watch.ElapsedMilliseconds} ms");

    // Parallel Sum
    watch.Restart();
    long parallelSum = ParallelSum(array);
    watch.Stop();
    Console.WriteLine($"Parallel Sum: {parallelSum}, Time: {watch.ElapsedMilliseconds} ms");

    // LINQ Sum
    watch.Restart();
    long linqSum = LinqSum(array);
    watch.Stop();
    Console.WriteLine($"LINQ Sum: {linqSum}, Time: {watch.ElapsedMilliseconds} ms");

    Console.WriteLine();
}

static long SequentialSum(int[] array)
{
    long sum = 0;
    foreach (var item in array)
    {
        sum += item;
    }
    return sum;
}

static long ParallelSum(int[] array)
{
    int numThreads = Environment.ProcessorCount; // Количество потоков = количество ядер
    int chunkSize = array.Length / numThreads;
    List<Thread> threads = new List<Thread>();
    long[] partialSums = new long[numThreads];

    for (int i = 0; i < numThreads; i++)
    {
        int threadIndex = i;
        threads.Add(new Thread(() =>
        {
            int start = threadIndex * chunkSize;
            int end = (threadIndex == numThreads - 1) ? array.Length : start + chunkSize;
            for (int j = start; j < end; j++)
                partialSums[threadIndex] += array[j];
        }));
    }

    // Запуск потоков
    foreach (var thread in threads) thread.Start();
    foreach (var thread in threads) thread.Join(); // Ожидание завершения всех потоков

    return partialSums.Sum();
}

static long LinqSum(int[] array)
{
    return array.AsParallel().Sum(x => (long)x);
}


int[] CreateParallelRandomArray(int size)
{
    int[] array = new int[size];
    Parallel.For(0, size, i =>
    {
        Random localRandom = new Random(Guid.NewGuid().GetHashCode());
        array[i] = localRandom.Next(0, 1000);
    });

    return array;
}