using System;
using System.Collections.Generic;
using System.IO;

namespace DayThreeTest
{
public class DayThree
{
    public static void Test()
    {
        string[] lines = File.ReadAllLines("SampleData.txt");
        int rows = lines.Length;
        int cols = lines[0].Length;

        char[,] grid = new char[rows, cols];
        bool[,] isPartNumber = new bool[rows, cols];

        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                grid[i, j] = lines[i][j];
            }
        }

        int sum = 0;

        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                if (char.IsDigit(grid[i, j]) && IsAdjacentToSymbol(grid, i, j, rows, cols))
                {
                    sum += grid[i, j] - '0';
                }
            }
        }

        Console.WriteLine("Sum of all part numbers: " + sum);
    }

    static bool IsAdjacentToSymbol(char[,] grid, int x, int y, int rows, int cols)
    {
        int[] dx = { -1, -1, -1, 0, 0, 1, 1, 1 };
        int[] dy = { -1, 0, 1, -1, 1, -1, 0, 1 };

        for (int k = 0; k < 8; k++)
        {
            int nx = x + dx[k];
            int ny = y + dy[k];

            if (nx >= 0 && nx < rows && ny >= 0 && ny < cols)
            {
                if (grid[nx, ny] != '.' && !char.IsDigit(grid[nx, ny]))
                {
                    return true;
                }
            }
        }

        return false;
    }
}
}