using System;
using System.IO;
using System.Linq;

namespace DayOneTest{
public class DayOne
{
    public static void Test()
    {
        string[] lines = File.ReadAllLines("SampleData.txt");
        int sum = 0;

        foreach (string line in lines)
        {
            int firstDigit = -1;
            int lastDigit = -1;

            foreach (char c in line)
            {
                if (char.IsDigit(c))
                {
                    if (firstDigit == -1)
                    {
                        firstDigit = c - '0';
                    }
                    lastDigit = c - '0';
                }
            }

            if (firstDigit != -1 && lastDigit != -1)
            {
                int calibrationValue = firstDigit * 10 + lastDigit;
                sum += calibrationValue;
            }
        }

        Console.WriteLine("Sum of all calibration values: " + sum);
    }
}
}