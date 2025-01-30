using Multithreaded.Base;

namespace Multithreaded.Calculations;

public class LinqSumCalculatorCalc: BaseSumCalculatorCalc
{
    public override long  CalculateSum(int[] array)
    {
        return array.AsParallel().Sum(x=>(long)x); 
    }

    public override string GetName() => "LINQ";
}