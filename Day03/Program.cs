var banks = File.ReadAllLines("input.txt")
    .Select(b =>
        b.Select(c => c - '0').ToArray())
    .ToArray();

Part1();
Part2();
void Part1()
{
    var totalJoltage = 0;
    foreach (var bank in banks)
    {
        var bankAsSpan = bank.AsSpan();
        var maxIndex = FindIndexOfMaxDigit(bankAsSpan[..(bank.Length - 1)]);
        var nextMaxIndex = maxIndex + 1 + FindIndexOfMaxDigit(bankAsSpan[(maxIndex + 1)..]);
        var joltage = int.Parse($"{bank[maxIndex]}{bank[nextMaxIndex]}");
        totalJoltage += joltage;
    }

    Console.WriteLine($"Part 1: {totalJoltage}");
}

void Part2()
{
    var totalJoltage = 0L;
    var indices = new int[12];
    foreach (var bank in banks)
    {
        var prevIdx = -1;
        for (int i = 0; i < 12; i++)
        {
            for (int n = 9; n >= 1; n--)
            {
                var idx = Array.IndexOf(bank, n, prevIdx + 1, bank.Length - (prevIdx + 1) - (11 - i));
                if (idx >= 0)
                {
                    indices[i] = idx;
                    prevIdx = idx;
                    break;
                }
            }
        }

        var joltage = long.Parse(string.Join("", indices.Select(idx => bank[idx].ToString())));
        totalJoltage += joltage;
    }

    Console.WriteLine($"Part 2: {totalJoltage}");
}

int FindIndexOfMaxDigit(ReadOnlySpan<int> s)
{
    var max = -1;
    var maxIndex = -1;
    for (var i = 0; i < s.Length; i++)
    {
        if (s[i] > max)
        {
            max = s[i];
            maxIndex = i;
        }
    }

    return maxIndex;
}
