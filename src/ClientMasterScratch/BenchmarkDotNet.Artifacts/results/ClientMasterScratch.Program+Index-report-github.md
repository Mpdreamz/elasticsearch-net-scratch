``` ini

BenchmarkDotNet=v0.10.11, OS=Windows 10.0.17063
Processor=Intel Core i7-6700HQ CPU 2.60GHz (Skylake), ProcessorCount=8
Frequency=2531251 Hz, Resolution=395.0616 ns, Timer=TSC
.NET Core SDK=2.1.2
  [Host]  : .NET Core 2.0.3 (Framework 4.6.25815.02), 64bit RyuJIT
  FastJob : .NET Core 2.0.3 (Framework 4.6.25815.02), 64bit RyuJIT

Job=FastJob  LaunchCount=1  RunStrategy=ColdStart  
TargetCount=5  WarmupCount=1  

```
|        Method |    Mean |    Error |   StdDev |       Gen 0 | Allocated |
|-------------- |--------:|---------:|---------:|------------:|----------:|
|           Run | 2.451 s | 0.4159 s | 0.1080 s | 709000.0000 |   2.08 GB |
| RunCreateOnce | 2.468 s | 0.2426 s | 0.0630 s | 707200.0000 |   2.08 GB |
