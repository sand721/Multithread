using Microsoft.Extensions.DependencyInjection;
using Multithreaded.Base;
using Multithreaded.Helpers;

int[] sizes = [100000, 1000000, 10000000];
const int columnWidth = 15;
        
var serviceCollection = new ServiceCollection();
serviceCollection.Scan(scan => scan
    .FromAssemblyOf<BaseSumCalculatorCalc>()
    .AddClasses(classes => classes.AssignableTo<BaseSumCalculatorCalc>())
    .As<BaseSumCalculatorCalc>() 
    .WithSingletonLifetime()); 

var serviceProvider = serviceCollection.BuildServiceProvider();
var calculators = serviceProvider.GetServices<BaseSumCalculatorCalc>().ToList();

Console.WriteLine($"{"Размер",columnWidth} {string.Join("", calculators.Select(c => $"{c.GetName(),columnWidth}"))}");
foreach (var size in sizes)
{
    var array = new int[size];

    Console.Write($"{size,columnWidth}");
    foreach (var calculator in calculators)
    {
        array.FillRandom();
        calculator.Execute(array);
        Console.Write($" {calculator.GetTime(),columnWidth}");
    }
    Console.WriteLine();
}

