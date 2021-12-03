using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace AdventOfCode
{
    class Day2
    {
        struct Data
        {
            public Data(string command, int value)
            {
                Command = command;
                Value = value;
            }
            public string Command { get; private set; }
            public int Value { get; private set; }
        }
        static int Forward(int x, int value) => x + value;
        static int Down(int y, int value) => y + value;
        static int Up(int y, int value) => y - value;
        static int GetPositionMultiplied(List<Data> data)
        {
            int x = 0, y = 0;
            foreach (var item in data)
            {
                switch (item.Command)
                {
                    case "forward":
                        x = Forward(x, item.Value);
                        break;
                    case "down":
                        y = Down(y, item.Value);
                        break;
                    case "up":
                        y = Up(y, item.Value);
                        break;
                    default:
                        Console.WriteLine("woopsie, we sunk!");
                        break;
                }
            }
            return x * y;
        }
        static int ForwardAim(int z, int value) => z * value;
        static int ForwardAimHorizontal(int x, int value) => x + value;
        static int DownAim(int z, int value) => z + value;
        static int UpAim(int z, int value) => z - value;
        static int GetComplicatedPosition(List<Data> data)
        {
            int x = 0, y = 0, aim = 0;
            foreach (var item in data)
            {
                switch (item.Command)
                {
                    case "forward":
                        y += ForwardAim(aim, item.Value);
                        x = ForwardAimHorizontal(x, item.Value);
                        break;
                    case "down":
                        aim = DownAim(aim, item.Value);
                        break;
                    case "up":
                        aim = UpAim(aim, item.Value);
                        break;
                    default:
                        Console.WriteLine("woopsie, we sunk!");
                        break;
                }
            }
            return x * y;
        }
        static List<Data> LoadInput()
        {
            List<Data> input = new List<Data>();
            string s;
            using (StreamReader sr = File.OpenText("inputs/inputDay2.txt"))
                while ((s = sr.ReadLine()) != null)
                {
                    string[] st = s.Split(' ');
                    input.Add(new Data(st[0], int.Parse(st[1])));
                }
            return input;
        }
        public static void PrintSolution1()
        {
            List<Data> input = LoadInput();
            Console.WriteLine(GetPositionMultiplied(input));
        }
        public static void PrintSolution2()
        {
            List<Data> input = LoadInput();
            Console.WriteLine(GetComplicatedPosition(input));
        }
        static List<Data> testUnit = new List<Data>
        {
            new Data("forward", 5),
            new Data("down", 5),
            new Data("forward", 8),
            new Data("up", 3),
            new Data("down", 8),
            new Data("forward", 2)
        };
    }
}
