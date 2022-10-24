namespace LongestSubsequence;

public class SequencerDzemojad
{
    private readonly int[] _array;

    public SequencerDzemojad(int[] array)
    {
        _array = array;
    }

    public Dictionary<int, int> LongestSubsequence()
    {
        var longestString = 0;
        var startIndex = 0;

        Dictionary<int, int> data = new(_array.Length); // key - startValue value - stringLength 
        Dictionary<int, int> seekingFollowing = new(_array.Length);
        Dictionary<int, int> seekingPreceding = new(_array.Length);
        foreach (var element in _array)
        {
            if (data.ContainsKey(element)) continue; // it was before
            data[element] = 1; // mark as seen
            if (!seekingFollowing.ContainsKey(element) &&
                !seekingPreceding.ContainsKey(element)) // it is not being seeked
            {
                seekingFollowing[element + 1] = element;
                seekingPreceding[element - 1] = element;

                if (longestString == 0 || (longestString == 1 && startIndex > element))
                {
                    longestString = 1;
                    startIndex = element;
                }

                continue;
            }

            if (seekingFollowing.ContainsKey(element) && seekingPreceding.ContainsKey(element)) // join strings
            {
                data[seekingFollowing[element]] = data[seekingFollowing[element]] + 1 + data[seekingPreceding[element]];
                seekingFollowing[data[seekingFollowing[element]] + seekingFollowing[element]] =
                    seekingFollowing[element];

                if (data[seekingFollowing[element]] > longestString ||
                    (data[seekingFollowing[element]] == longestString && seekingFollowing[element] < startIndex))
                {
                    longestString = data[seekingFollowing[element]];
                    startIndex = seekingFollowing[element];
                }

                seekingFollowing.Remove(element);
                seekingPreceding.Remove(element);


                continue;
            }

            if (seekingFollowing.ContainsKey(element)) //add at the end
            {
                data[seekingFollowing[element]] = data[seekingFollowing[element]] + 1;

                if (data[seekingFollowing[element]] > longestString ||
                    (data[seekingFollowing[element]] == longestString && seekingFollowing[element] < startIndex))
                {
                    longestString = data[seekingFollowing[element]];
                    startIndex = seekingFollowing[element];
                }

                seekingFollowing[element + 1] = seekingFollowing[element];
                seekingFollowing.Remove(element);

                continue;
            }
            //add at the beginning

            data[element] = data[seekingPreceding[element]] + 1;
            seekingFollowing[element + data[element]] = element;
            seekingPreceding[element - 1] = element;
            seekingPreceding.Remove(element);
            if (data[element] > longestString || (data[element] == longestString && element < startIndex))
            {
                longestString = data[element];
                startIndex = element;
            }
        }

        return new Dictionary<int, int> {{startIndex, longestString}};
    }
}