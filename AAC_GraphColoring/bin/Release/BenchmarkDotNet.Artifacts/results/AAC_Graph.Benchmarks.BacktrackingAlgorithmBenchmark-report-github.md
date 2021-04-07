``` ini

BenchmarkDotNet=v0.12.1, OS=Windows 10.0.19042
Intel Core i7-9750H CPU 2.60GHz, 1 CPU, 12 logical and 6 physical cores
  [Host]     : .NET Framework 4.8 (4.8.4341.0), X86 LegacyJIT
  DefaultJob : .NET Framework 4.8 (4.8.4341.0), X86 LegacyJIT


```
|                    Method |   dim |          matrix |        Mean |     Error |    StdDev | Rank |
|-------------------------- |------ |---------------- |------------:|----------:|----------:|-----:|
|       BenchmarkGreedyAlgo | 10000 | Byte[100000000] |    494.6 ms |   8.40 ms |  12.57 ms |    1 |
| BenchmarkBacktrackingAlgo | 10000 | Byte[100000000] | 60,911.3 ms | 327.36 ms | 306.21 ms |    2 |
