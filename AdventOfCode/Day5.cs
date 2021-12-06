using System;
using System.Collections.Generic;
using System.IO;

namespace AdventOfCode
{
    class Day5
    {
        struct Point
        {
            public Point(int x1, int y1, int x2, int y2)
            {
                X1 = x1;
                Y1 = y1;
                X2 = x2;
                Y2 = y2;
            }
            public int X1 { get; set; }
            public int Y1 { get; set; }
            public int X2 { get; set; }
            public int Y2 {get; set; }
        }
        struct Data
        {
            public List<Point> Points { get; set; }
            public int[,] HazardMap { get; set; }
            public Data Initialize()
            {
                Points = new List<Point>();
                return this;
            }
        }
        static Data GetHazardPoints(Data data, bool countDiagonals)
        {
            foreach (var points in data.Points)
            {
                if (points.X1 == points.X2)
                    for (int i = (points.Y1 < points.Y2 ? points.Y1 : points.Y2); i <= (points.Y1 > points.Y2 ? points.Y1 : points.Y2); ++i)
                        data.HazardMap[i, points.X1] += 1;
                else if (points.Y1 == points.Y2)
                    for (int i = (points.X1 < points.X2 ? points.X1 : points.X2); i <= (points.X1 > points.X2 ? points.X1 : points.X2); ++i)
                        data.HazardMap[points.Y1, i] += 1;
                if (!countDiagonals)
                    continue;
                if (points.X1 + points.Y2 == points.X2 + points.Y1)
                    for (int i = (points.X1 < points.X2 ? points.X1 : points.X2), j = 0; i <= (points.X1 > points.X2 ? points.X1 : points.X2); ++i, ++j)
                        data.HazardMap[(points.Y1 < points.Y2 ? points.Y1 : points.Y2) + j, (points.X1 < points.X2 ? points.X1 : points.X2) + j] += 1;
                else if (points.X1 + points.Y1 == points.X2 + points.Y2)
                    for (int i = (points.Y1 > points.Y2 ? points.Y1 : points.Y2), j = 0; i >= (points.Y1 < points.Y2 ? points.Y1 : points.Y2); --i, ++j)
                        data.HazardMap[(points.Y1 > points.Y2 ? points.Y1 : points.Y2) - j, (points.X1 < points.X2 ? points.X1 : points.X2) + j] += 1;
            } 
            return data;
        }
        static int GetHazardSumOfHazard(Data data, bool countDiagonals)
        {
            data = GetHazardPoints(data, countDiagonals);
            int sum = 0;
            for (int i = 0; i < data.HazardMap.GetLength(0); ++i)
                for (int j = 0; j < data.HazardMap.GetLength(1); ++j)
                    if (data.HazardMap[i, j] > 1) sum += 1;
            return sum;
        }
        static int ReturnMax(int currentMax, int x, int y) => x > currentMax ? x : (y > currentMax ? y : currentMax);
        static Data LoadInput()
        {
            Data input = new Data().Initialize();
            string s;
            int maxX = 0, maxY = 0;
            string[] separators = { "->", ",", " " };
            using (StreamReader sr = File.OpenText("inputs/inputDay5.txt"))
                while ((s = sr.ReadLine()) != null)
                {
                    string[] st = s.Split(separators,StringSplitOptions.RemoveEmptyEntries);
                    maxX = ReturnMax(maxX, int.Parse(st[0]), int.Parse(st[2]));
                    maxY = ReturnMax(maxY, int.Parse(st[1]), int.Parse(st[3]));
                    input.Points.Add(new Point(int.Parse(st[0]), int.Parse(st[1]), int.Parse(st[2]), int.Parse(st[3])));
                };
            input.HazardMap = new int[maxY + 1, maxX + 1];
            return input;
        }
        public static void PrintSolution1()
        {
            Data input = LoadInput();
            Console.WriteLine(GetHazardSumOfHazard(input, false));
        }
        public static void PrintSolution2()
        {
            Data input = LoadInput();
            Console.WriteLine(GetHazardSumOfHazard(input, true));
        }
        static Data testUnit = new Data()
        {
            Points = new List<Point>()
            {
                new Point(0, 9, 5, 9),
                new Point(8, 0, 0, 8),
                new Point(9, 4, 3, 4),
                new Point(2, 2, 2, 1),
                new Point(7, 0, 7, 4),
                new Point(6, 4, 2, 0),
                new Point(0, 9, 2, 9),
                new Point(3, 4, 1, 4),
                new Point(0, 0, 8, 8),
                new Point(5, 5, 8, 2)
            },
            HazardMap = new int[10, 10]
        };
    }
}