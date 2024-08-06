using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace DayFourTest
{
public class DayFour
{
    public static void Test()
    {
        string[] lines = File.ReadAllLines("SampleData.txt");
        int totalPoints = 0;

        foreach (string line in lines)
        {
            var parts = line.Split('|');
            var winningNumbers = parts[0].Trim().Split(' ').Select(int.Parse).ToHashSet();
            var yourNumbers = parts[1].Trim().Split(' ').Select(int.Parse).ToArray();

            int points = CalculatePoints(winningNumbers, yourNumbers);
            totalPoints += points;
        }

        Console.WriteLine("Total points: " + totalPoints);
    }

    public static int CalculatePoints(HashSet<int> winningNumbers, int[] yourNumbers)
    {
        int matchCount = 0;
        int points = 0;

        foreach (var number in yourNumbers)
        {
            if (winningNumbers.Contains(number))
            {
                matchCount++;
                if (matchCount == 1)
                {
                    points += 1;
                }
                else
                {
                    points *= 2;
                }
            }
        }

        return points;
    }
}
}