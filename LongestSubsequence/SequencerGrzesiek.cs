namespace LongestSubsequence;

public class SequencerGrzesiek
{
    private readonly int[] _array;

    public SequencerGrzesiek(int[] array)
    {
        _array = array;
    }
    
    public Dictionary<int,int> LongestSubsequence()
    {
        var longestSets = new Dictionary<int, int>(); //index - count

        var current = 0;

        for (var i = 0; i < _array.Length - 1; i++)
        {
            if (i == _array.Length - 2 && current > 1)
                longestSets.Add(i + 1, current + 1);
            else if (_array[i] + 1 == _array[i + 1])
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
}