using BenchmarkDotNet.Attributes;

namespace LongestSubsequence;

[MemoryDiagnoser]
public class Benchmarks
{
    private int[] array = new[] {4, 6, 1, 3, 2, 4, 5, 100, 200, 1, 101, 105, 300, 102, 5, 104, 19, 103};

    [Benchmark]
    public void Grzesiek()
    {
        var result = new SequencerGrzesiek(array);
        result.LongestSubsequence();
    }
    
    [Benchmark]
    public void Dzemojad()
    {
        var result = new SequencerDzemojad(array);
        result.LongestSubsequence();
    }
}