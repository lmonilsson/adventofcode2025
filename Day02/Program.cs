var input = File.ReadAllLines("input.txt")[0];

var ranges = input.Split(",")
    .Select(s => s.Split("-"))
    .Select(p => (First: long.Parse(p[0]), Last: long.Parse(p[1])))
    .ToList();

Part1();
Part2();

void Part1()
{
    long sum = 0;
    foreach (var r in ranges)
    {
        for (var id = r.First; id <= r.Last; id++)
        {
            var sid = id.ToString();
            if (sid.Length % 2 == 0)
            {
                if (sid[..(sid.Length / 2)] == sid[(sid.Length / 2)..])
                {
                    sum += id;
                }
            }
        }
    }

    Console.WriteLine($"Part 1: {sum}");
}

void Part2()
{
    long sum = 0;
    foreach (var r in ranges)
    {
        for (var id = r.First; id <= r.Last; id++)
        {
            var sid = id.ToString();
            if (Enumerable.Range(1, sid.Length / 2)
                .Where(sublen => sid.Length % sublen == 0)
                .Any(sublen =>
                {
                    for (var k = sublen; k < sid.Length; k += sublen)
                    {
                        if (sid[..sublen] != sid[k..(k + sublen)])
                        {
                            return false;
                        }
                    }
                    
                    return true;
                }))
            {
                sum += id;
            }
        }
    }

    Console.WriteLine($"Part 2: {sum}");
}