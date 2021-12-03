using System;
using System.IO;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode
{
    class Day1
    {
        static int Measurements(List<int> measurements)
        {
            int sum = 0, previous = measurements[0];
            for (int i = 1; i < measurements.Count; ++i)
            {
                if (measurements[i] > previous) sum++;
                previous = measurements[i];
            }
            return sum;
        }
        static int TripleMeasurements(List<int> m)
        {
            int sum = 0, previousSum = m[0] + m[1] + m[2];
            for (int i = 2; i < m.Count - 1; ++i)
            {
                if ((m[i - 1] + m[i] + m[i + 1]) > previousSum) sum++;
                previousSum = m[i - 1] + m[i] + m[i + 1];
            }
            return sum;
        }
        static List<int> LoadInput()
        {
            List<int> input = new List<int>();
            string s;
            using (StreamReader sr = File.OpenText("inputs/inputDay1.txt"))
                while ((s = sr.ReadLine()) != null)
                    input.Add(int.Parse(s));
            return input;
        }
        public static void PrintSolution1()
        {
            List<int> input = LoadInput();
            Console.WriteLine(Measurements(input));
        }
        public static void PrintSolution2()
        {
            List<int> input = LoadInput();
            Console.WriteLine(TripleMeasurements(input));
        }
        static List<int> testUnit = new List<int>
        {
            199,
            200,
            208,
            210,
            200,
            207,
            240,
            269,
            260,
            263
        };
    }
}
