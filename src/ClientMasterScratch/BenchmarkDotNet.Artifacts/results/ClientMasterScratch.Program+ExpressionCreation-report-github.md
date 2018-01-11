``` ini

BenchmarkDotNet=v0.10.11, OS=Windows 10.0.17063
Processor=Intel Core i7-6700HQ CPU 2.60GHz (Skylake), ProcessorCount=8
Frequency=2531251 Hz, Resolution=395.0616 ns, Timer=TSC
.NET Core SDK=2.1.2
  [Host]       : .NET Core 2.0.3 (Framework 4.6.25815.02), 64bit RyuJIT
  BenchmarkRun : .NET Core 2.0.3 (Framework 4.6.25815.02), 64bit RyuJIT

Job=BenchmarkRun  LaunchCount=1  RunStrategy=Monitoring  
TargetCount=5  WarmupCount=1  

```
|        Method |         Mean |       Error |       StdDev |      Gen 0 |    Allocated |
|-------------- |-------------:|------------:|-------------:|-----------:|-------------:|
|           Run | 702,564.6 us | 89,837.1 us | 23,334.85 us | 43000.0000 | 133634.66 KB |
| RunCreateOnce |     724.3 us |    345.3 us |     89.69 us |          - |      6.56 KB |
