{
    var input = File.ReadAllLines("input.txt");
    Part1(input);
    Part2(input);
}

void Part1(string[] input)
{
    var numberRows = input[..^1]
        .Select(l => l.Split(' ', StringSplitOptions.RemoveEmptyEntries)
            .Select(long.Parse).ToArray())
        .ToArray();
    
    var operators = input[^1]
        .Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
        .Select(s => s[0])
        .ToArray();

    long result = 0;
    for (int c = 0; c < operators.Length; c++)
    {
        var colNumbers = numberRows.Select(r => r[c]).ToArray();
        result += ApplyOperator(operators[c], colNumbers);
    }

    Console.WriteLine($"Part 1: {result}");
}

void Part2(string[] input)
{
    var operatorIndices = input.Last()
        .Select((c, i) => (c, i))
        .Where(ci => ci.c != ' ')
        .Select(ci => ci.i).ToArray();
    var operators = operatorIndices.Select(i => input[^1][i]).ToArray();
    
    var numberIndices = operatorIndices
        .Select((o1, i) =>
            (
                First: o1,
                Last: i < operatorIndices.Length - 1
                    ? operatorIndices[i + 1] - 2
                    : input[0].Length - 1
            )
        ).ToArray();

    var numberStringRows = input[..^1]
        .Select(line => numberIndices.Select(ni => line[ni.First..(ni.Last + 1)]).ToArray())
        .ToArray();

    long result = 0;
    for (int c = 0; c < operators.Length; c++)
    {
        var colNumbers = ParseColumnNumbers(numberStringRows, c);
        result += ApplyOperator(operators[c], colNumbers);
    }

    Console.WriteLine($"Part 2: {result}");
}

long[] ParseColumnNumbers(string[][] numberStringRows, int col)
{
    var lines = numberStringRows.Select(r => r[col]).ToArray();
    var numbers = new List<long>();
    for (int i = lines[0].Length - 1; i >= 0; i--)
    {
        var num = long.Parse(
            string.Join(
                "",
                lines.Select(line => line[i])
                    .Where(c => c != ' ')
                    .Select(c => c - '0')));
        numbers.Add(num);
    }

    return numbers.ToArray();
}

long ApplyOperator(char op, long[] numbers)
{
    switch (op)
    {
        case '+':
            return numbers.Sum();
        case '*':
            return numbers.Skip(1).Aggregate(numbers.First(), (a, b) => a * b);
        default:
            throw new ArgumentException($"Invalid operator {op}");
    }
}
