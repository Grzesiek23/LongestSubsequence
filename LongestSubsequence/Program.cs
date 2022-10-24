using System.Text.RegularExpressions;

var random = new Random();

var arr = Enumerable.Range(0, 200000).Select(x => random.Next(0, 50000)).Distinct().ToArray();
var longest1 = LongestSubsequence(arr);
PrintDetails(longest1, arr);

arr = new[] {4, 6, 1, 3, 2, 4, 5, 100, 200, 1, 101, 105, 300, 102, 5, 104, 19, 103};
var longest2 = LongestSubsequence(arr);
PrintDetails(longest2, arr);

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

Dictionary<int,int> LongestSubsequence(int[] array)
{
    array = array.Distinct().ToArray();
    
    Array.Sort(array);

    var longestSets = new Dictionary<int, int>(); //index - count

    var current = 0;

    for (var i = 0; i < array.Length - 1; i++)
    {
        if (i == array.Length - 2 && current > 1)
            longestSets.Add(i + 1, current + 1);
        else if (array[i] + 1 == array[i + 1])
            current++;
        else
        {
            if (current > 1)
                longestSets.Add(i, current);

            current = 0;
        }
    }

    return longestSets;
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