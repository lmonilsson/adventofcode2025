var instructions = File.ReadAllLines("input.txt");
Part1();
Part2();

void Part1()
{
    var pos = 50;
    var zeros = 0;

    foreach (var instr in instructions)
    {
        var n = int.Parse(instr[1..]);
        if (instr[0] == 'L' && pos != 0)
        {
            pos = 100 - pos;
        }
        pos = (pos + n) % 100;
        if (instr[0] == 'L' && pos != 0)
        {
            pos = 100 - pos;
        }

        if (pos == 0)
        {
            zeros++;
        }
    }
    
    Console.WriteLine($"Part 1: {zeros}");
}

void Part2()
{
    var pos = 50;
    var zeros = 0;

    foreach (var instr in instructions)
    {
        var n = int.Parse(instr[1..]);
        if (instr[0] == 'L' && pos != 0)
        {
            pos = 100 - pos;
        }
        pos += n;
        zeros += pos / 100;
        pos = pos % 100;
        if (instr[0] == 'L' && pos != 0)
        {
            pos = 100 - pos;
        }
    }
    
    Console.WriteLine($"Part 2: {zeros}");
}
