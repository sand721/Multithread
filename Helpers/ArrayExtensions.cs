namespace Multithreaded.Helpers;

public static class ArrayExtensions
{
    private static readonly Random rnd = new Random();
    public static void FillRandom(this int[] array)
    {
        var size = array.Length;
        Parallel.For(0, size-1, i =>
        {
            lock (rnd)
            {
                array[i] = rnd.Next(0, size - 1);
            }
        });        
    }
}