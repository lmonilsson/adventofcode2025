{
    var input = File.ReadAllLines("input.txt").Select(s => s.ToList()).ToList();
    Part1(input);
    Part2(input);
}

void Part1(List<List<char>> map)
{
    var accessible = GetAccessibleRolls(map);
    Console.WriteLine($"Part 1: {accessible.Count}");
}

void Part2(List<List<char>> map)
{
    var currentMap = map.Select(r => r.ToList()).ToList();
    var accessible = GetAccessibleRolls(map);
    while (accessible.Any())
    {
        accessible.ForEach(p => currentMap[p.Y][p.X] = '.');
        accessible = GetAccessibleRolls(currentMap);
    }

    var original = map.SelectMany(r => r).Count(c => c == '@');
    var nonMovable = currentMap.SelectMany(r => r).Count(c => c == '@');
    Console.WriteLine($"Part 2: {original - nonMovable}");
}

List<(int Y, int X)> GetAccessibleRolls(List<List<char>> map)
{
    return Enumerable.Range(0, map.Count)
        .SelectMany(y => Enumerable.Range(0, map[0].Count)
            .Select(x => (y, x)))
        .Where(p => map[p.y][p.x] == '@')
        .Where(p =>  SurroundingRolls(map, p.y, p.x) < 4)
        .ToList();
}

int SurroundingRolls(List<List<char>> map, int y, int x)
{
    var rows = map.Count;
    var cols = map[0].Count;
    var rolls = 0;
    
    if (y > 0 && x > 0 && map[y - 1][x - 1] == '@')
        rolls++;
    if (y > 0 && map[y - 1][x] == '@')
        rolls++;
    if (y > 0 && x < cols - 1 && map[y - 1][x + 1] == '@')
        rolls++;
    
    if (x > 0 && map[y][x - 1] == '@')
        rolls++;
    if (x < cols - 1 && map[y][x + 1] == '@')
        rolls++;
    
    if (y < rows - 1 && x > 0 && map[y + 1][x - 1] == '@')
        rolls++;
    if (y < rows - 1 && map[y + 1][x] == '@')
        rolls++;
    if (y < rows - 1 && x < cols - 1 && map[y + 1][x + 1] == '@')
        rolls++;

    return rolls;
}

