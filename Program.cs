using System.Management;
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

ShowEvironmetInfo();

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

return;

void ShowEvironmetInfo()
{
    ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT Name FROM Win32_Processor");
    foreach (ManagementObject obj in searcher.Get())
    {
        // Выводим название процессора
        string processoName = obj["Name"].ToString().Trim();
        Console.WriteLine($"Processor Name: {processoName} / Threads: {Environment.ProcessorCount}" );
    }
// Получаем объем оперативной памяти
    ManagementObjectSearcher memorySearcher = new ManagementObjectSearcher("SELECT TotalPhysicalMemory FROM Win32_ComputerSystem");
    foreach (ManagementObject obj in memorySearcher.Get())
    {
        ulong totalMemoryBytes = (ulong)obj["TotalPhysicalMemory"];
        double totalMemoryGB = totalMemoryBytes / (1024.0 * 1024.0 * 1024.0);
        Console.WriteLine($"Total RAM: {totalMemoryGB:F2} GB");
    }
// Получаем информацию о версии ОС
    ManagementObjectSearcher osSearcher = new ManagementObjectSearcher("SELECT Caption, Version FROM Win32_OperatingSystem");
    foreach (ManagementObject obj in osSearcher.Get())
    {
        string osName = obj["Caption"].ToString();
        string osVersion = obj["Version"].ToString();
        Console.WriteLine($"OS Name: {osName}");
        Console.WriteLine($"OS Version: {osVersion}");
    }
}

