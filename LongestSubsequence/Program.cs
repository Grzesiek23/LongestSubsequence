using System.Text.RegularExpressions;

var random = new Random();
var arr = Enumerable.Range(0, 200000).Select(x => random.Next(0, 50000)).Distinct().ToArray();

LongestSubsequence(arr);

Console.Read();

void LongestSubsequence(int[] array)
{
    Array.Sort(array);
    
    var longestSets = new Dictionary<int, int>();
    
    var current = 0;
    
    for (var i = 0; i < array.Length - 1; i++)
    {
        if (array[i] + 1 == array[i + 1])
            current++;
        else
        {
            if(current > 1)
                longestSets.Add(i, current);
            
            current = 0;
        }
    }

    if (!longestSets.Any())
    {
        Write("[No sequence found!]", ConsoleColor.Yellow);
        return;
    }
    
    var longest = longestSets.MaxBy(x => x.Value);
    var start = array[longest.Key - longest.Value];
    var sequence = string.Join(",", Enumerable.Range(start, longest.Value + 1));

    Write($"Znaleziono {longestSets.Count()} ciągów", ConsoleColor.Green);
    
    Write($"Najdłuższy ciąg ma długość [{longest.Value + 1}] i jest to [{sequence}]", ConsoleColor.Cyan);
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