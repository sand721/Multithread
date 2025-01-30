namespace Multithreaded.Base.Interface;

public interface ISumCalculator
{
    long CalculateSum(int[] array);
    void Execute(int[] array);
    long GetTime();
    long GetSum();
    long GetSize();
    string GetName();
}