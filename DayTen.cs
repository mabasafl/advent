using System;
using System.Collections.Generic;
using System.IO;

namespace DayTenTest{
public class DayTen
{
    static char[,] grid;
    static int rows;
    static int cols;

    static readonly int[] dx = { -1, 1, 0, 0 };
    static readonly int[] dy = { 0, 0, -1, 1 };

    static readonly Dictionary<char, (int, int)[]> directions = new Dictionary<char, (int, int)[]>()
    {
        { '|', new (int, int)[] { (-1, 0), (1, 0) } },
        { '-', new (int, int)[] { (0, -1), (0, 1) } },
        { 'L', new (int, int)[] { (-1, 0), (0, 1) } },
        { 'J', new (int, int)[] { (-1, 0), (0, -1) } },
        { '7', new (int, int)[] { (1, 0), (0, -1) } },
        { 'F', new (int, int)[] { (1, 0), (0, 1) } },
        { 'S', new (int, int)[] { (-1, 0), (1, 0), (0, -1), (0, 1) } }
    };

    public static void Test()
    {
        ReadGridFromFile("SampleData.txt");

        var start = FindStart();
        if (start == (-1, -1))
        {
            Console.WriteLine("Starting position not found.");
            return;
        }

        var distances = BFS(start);
        int maxDistance = FindMaxDistance(distances);

        Console.WriteLine("Maximum distance from the start: " + maxDistance);
    }

    static void ReadGridFromFile(string filePath)
    {
        var lines = File.ReadAllLines(filePath);
        rows = lines.Length;
        cols = lines[0].Length;
        grid = new char[rows, cols];

        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                grid[i, j] = lines[i][j];
            }
        }
    }

    static (int, int) FindStart()
    {
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                if (grid[i, j] == 'S')
                {
                    return (i, j);
                }
            }
        }
        return (-1, -1);
    }

    static int[,] BFS((int, int) start)
    {
        var distances = new int[rows, cols];
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                distances[i, j] = -1;
            }
        }

        var queue = new Queue<(int, int)>();
        queue.Enqueue(start);
        distances[start.Item1, start.Item2] = 0;

        while (queue.Count > 0)
        {
            var (x, y) = queue.Dequeue();
            foreach (var (dx, dy) in directions[grid[x, y]])
            {
                int nx = x + dx;
                int ny = y + dy;
                if (IsInBounds(nx, ny) && distances[nx, ny] == -1 && directions.ContainsKey(grid[nx, ny]) && IsConnected((x, y), (nx, ny)))
                {
                    distances[nx, ny] = distances[x, y] + 1;
                    queue.Enqueue((nx, ny));
                }
            }
        }

        return distances;
    }

    static bool IsInBounds(int x, int y)
    {
        return x >= 0 && x < rows && y >= 0 && y < cols;
    }

    static bool IsConnected((int, int) from, (int, int) to)
    {
        foreach (var (dx, dy) in directions[grid[to.Item1, to.Item2]])
        {
            if (from == (to.Item1 + dx, to.Item2 + dy))
            {
                return true;
            }
        }
        return false;
    }

    static int FindMaxDistance(int[,] distances)
    {
        int maxDistance = 0;
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                if (distances[i, j] > maxDistance)
                {
                    maxDistance = distances[i, j];
                }
            }
        }
        return maxDistance;
    }
}
}