using System.Text.RegularExpressions;
using BenchmarkDotNet.Running;
using LongestSubsequence;


// var random = new Random();
//
// var arr = Enumerable.Range(0, 200000).Select(x => random.Next(0, 50000)).Distinct().ToArray();
// // arr = new[] {4, 6, 1, 3, 2, 4, 5, 100, 200, 1, 101, 105, 300, 102, 5, 104, 19, 103};
//
// var distinctArray = arr.Distinct().ToArray();
// Array.Sort(distinctArray);
//
// var seqGrz = new SequencerGrzesiek(distinctArray);
// var longest1 = seqGrz.LongestSubsequence();
// PrintDetails(longest1, distinctArray);
//
// var seqDze = new SequencerDzemojad(arr);
// var longest2 = seqDze.LongestSubsequence();
// Console.WriteLine($"Najdłuższy ciąg ma długość {longest2.First().Value} i jest to [{string.Join(", ", Enumerable.Range(longest2.First().Key, longest2.First().Value))}]");


BenchmarkRunner.Run(typeof(Program).Assembly);

Console.Read();


static void PrintDetails(Dictionary<int, int> longestSets, int[] input)
{
    if (!longestSets.Any())
    {
        Write("[No sequence found!]", ConsoleColor.Yellow);
        return;
    }

    var longest = longestSets.OrderByDescending(x => x.Value).ThenBy(x => x.Key).First();
    var start = input[longest.Key - longest.Value];
    var sequence = string.Join(",", Enumerable.Range(start, longest.Value + 1));

    Write($"Znaleziono {longestSets.Count()} ciągów", ConsoleColor.Green);
    
    Write($"Najdłuższy ciąg ma długość [{longest.Value + 1}] i jest to [{sequence}]", ConsoleColor.Cyan);
    
    var otherWithSameLength = longestSets.Count(x => x.Value == longest.Value);
    
    if(otherWithSameLength > 1) 
        Write($"Istnieje więcej ([{otherWithSameLength}]) ciągów o tej samej długości", ConsoleColor.Yellow);
}

static void Write(string message, ConsoleColor color)
{
    var pieces = Regex.Split(message, @"(\[[^\]]*\])");

    foreach (var item in pieces)
    {
        var piece = item;

        if (piece.StartsWith("[") && piece.EndsWith("]"))
        {
            Console.ForegroundColor = color;
            piece = piece.Substring(1, piece.Length - 2);
        }

        Console.Write(piece);
        Console.ResetColor();
    }

    Console.WriteLine();
}