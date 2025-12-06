{
    var input = File.ReadAllLines("input.txt");

    var ranges = input
        .TakeWhile(l => !string.IsNullOrEmpty(l))
        .Select(l => l.Split('-'))
        .Select(sp => (long.Parse(sp[0]), long.Parse(sp[1])))
        .ToList();

    var available = input
        .SkipWhile(l => !string.IsNullOrEmpty(l))
        .Skip(1)
        .Select(long.Parse).ToList();

    Part1(ranges, available);
    Part2(ranges);
}
void Part1(List<(long, long)> ranges, List<long> available)
{
    var freshAvailable = available.Where(id => IsFresh(id, ranges)).ToList();
    Console.WriteLine($"Part 1: {freshAvailable.Count}");
}

static bool IsFresh(long id, List<(long, long)> ranges)
{
    return ranges.Any(r => id >= r.Item1 && id <= r.Item2);
}

void Part2(List<(long, long)> ranges)
{
    ranges = ranges.ToList();

    for (int i = 0; i < ranges.Count; i++)
    {
        var overlap = ranges.FindIndex(i + 1, r => IsOverlap(ranges[i], r));
        while (overlap > 0)
        {
            ranges[i] = MergeRanges(ranges[i], ranges[overlap]);
            ranges.RemoveAt(overlap);
            overlap = ranges.FindIndex(i + 1, r => IsOverlap(ranges[i], r));
        }
    }

    var totalFresh = ranges.Select(r => r.Item2 - r.Item1 + 1).Sum();
    Console.WriteLine($"Part 2: {totalFresh}");
}

bool IsOverlap((long, long) r1, (long, long) r2)
{
    return r1.Item1 <= r2.Item1 && r1.Item2 >= r2.Item1
        || r1.Item1 <= r2.Item2 && r1.Item2 >= r2.Item2
        || r2.Item1 <= r1.Item1 && r2.Item2 >= r1.Item1
        || r2.Item1 <= r1.Item2 && r2.Item2 >= r1.Item2;
}

(long, long) MergeRanges((long, long) r1, (long, long) r2)
{
    var min = Math.Min(r1.Item1, r2.Item1);
    var max = Math.Max(r1.Item2, r2.Item2);
    return (min, max);
}
