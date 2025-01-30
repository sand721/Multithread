using System.Diagnostics;
using Multithreaded.Base.Interface;

namespace Multithreaded.Base;

public abstract class BaseSumCalculatorCalc: ISumCalculator
{
    protected (T result, long time) TimeExecution<T>(Func<T> action)
    {
        var stopwatch = Stopwatch.StartNew();
        T result = action();
        stopwatch.Stop();
        return (result, stopwatch.ElapsedMilliseconds);
    }

    public abstract long CalculateSum(int[] array);

    private long _sum = 0;
    private long _time = 0;
    private long _size = 0;
    public long GetTime() => _time;

    public long GetSum() => _sum;
    public long GetSize() => _size;
    
    public abstract string GetName();

    public void Execute(int[] array)
    {
        _size = array.Length;
        (_sum, _time) = TimeExecution(() => CalculateSum(array));
    }
}
