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
|        Method |         Mean |      Error |    StdDev |      Gen 0 |    Allocated |
|-------------- |-------------:|-----------:|----------:|-----------:|-------------:|
|           Run | 1,061.552 ms | 106.325 ms | 27.618 ms | 62000.0000 | 192203.37 KB |
| RunCreateOnce |     9.032 ms |   4.003 ms |  1.040 ms |          - |      4.57 KB |
