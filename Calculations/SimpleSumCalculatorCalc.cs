using Multithreaded.Base;

namespace Multithreaded.Calculations;

public class SimpleSumCalculatorCalc: BaseSumCalculatorCalc
{
    public override string GetName()=> "Обычное";
    public override long  CalculateSum(int[] array)
    {
        var sum = 0;
        foreach (var i in array)
        {
            sum += i;
        }
        return sum; 
    }
}