using System.Text.RegularExpressions;

int[] arr = {100, 200, 1, 101, 105, 300, 102, 5, 104, 19, 103};
var subsequenceSet = LongestSubsequence(arr);
Write(
    $"Najdłuższy ciąg ma długość [{subsequenceSet.Count}] i jest to: [{string.Join(",", subsequenceSet.ToArray())}]",
    ConsoleColor.Blue);
Console.Read();

bool ArrayContains(IReadOnlyList<int> arr, int num)
{
    for (var i = 0; (i < arr.Count); i++)
    {
        if (arr[i] == num)
        {
            return true;
        }
    }

    return false;
}

List<int> LongestSubsequence(IReadOnlyList<int> array)
{
    var longestList = new List<int>();

    foreach (var number in array)
    {
        var current = number;
        var currentList = new List<int>() {number};
        while (ArrayContains(array, (current + 1)))
        {
            current++;
            currentList.Add(current);
        }

        if (currentList.Count > longestList.Count)
            longestList = currentList;
    }

    return longestList;
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