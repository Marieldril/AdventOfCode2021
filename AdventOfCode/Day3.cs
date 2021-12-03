using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace AdventOfCode
{
    class Day3
    {
        struct Rate
        {
            public int Gamma { get; set; }
            public int Epsilon { get; set; }
        }
        static int GetDecimal(string bits)
        {
            int dec = 0, j = 0;
            for (int i = bits.Length - 1; i >= 0; i--, j++)
                dec += (j == 0 && bits[i] == '0') ? 0 : (int)Math.Pow((bits[i] == '1' ? 2 : 0), j);
            return dec;
        }
        static Rate CommonBitPosition(List<string> input)
        {
            string ep = "", gm = "";
            for (int i = 0; i < input[0].Length; i++)
            {
                int one = 0;
                for (int j = 0; j < input.Count; j++)
                    if (input[j][i] == '1') one++;
                if (one > input.Count - one)
                {
                    gm += "1";
                    ep += "0";
                }
                else
                {
                    gm += "0";
                    ep += "1";
                }
            }
            Rate rate = new Rate();
            rate.Epsilon = GetDecimal(ep);
            rate.Gamma = GetDecimal(gm);
            return rate;
        }
        static int PowerConsumption(Rate r) => r.Gamma * r.Epsilon;
        static int SupportRating(int x, int y) => x * y;
        static List<string> LoadInput()
        {
            List<string> input = new List<string>();
            string s;
            using (StreamReader sr = File.OpenText("inputs/inputDay3.txt"))
                while ((s = sr.ReadLine()) != null)
                    input.Add(s);
            return input;
        }
        static List<string> GetOxygenList(List<string> input, int pos = 0)
        {
            if (pos == input[0].Length || input.Count == 1) return input;
            List<string> list = new List<string>();
            int one = 0;
            for (int i = 0; i < input.Count; i++)
                if (input[i][pos] == '1') one++;
            if (one >= input.Count - one)
                for (int i = 0; i < input.Count; i++)
                {
                    if (input[i][pos] == '1')
                        list.Add(input[i]);
                }
            else
                for (int i = 0; i < input.Count; i++)
                    if (input[i][pos] == '0')
                        list.Add(input[i]);
            return GetOxygenList(list, pos + 1);
        }

        static List<string> GetCoList(List<string> input, int pos = 0)
        {
            if (pos == input[0].Length || input.Count == 1) return input;
            List<string> list = new List<string>();
            int one = 0;
            for (int i = 0; i < input.Count; i++)
                if (input[i][pos] == '0') one++;
            if (one <= input.Count - one)
                for (int i = 0; i < input.Count; i++)
                {
                    if (input[i][pos] == '0')
                        list.Add(input[i]);
                }
            else
                for (int i = 0; i < input.Count; i++)
                    if (input[i][pos] == '1')
                        list.Add(input[i]);
            return GetCoList(list, pos + 1);
        }

        public static void PrintSolution1()
        {
            List<string> input = LoadInput();
            Rate rate = CommonBitPosition(input);
            Console.WriteLine(PowerConsumption(rate));
        }
        public static void PrintSolution2()
        {
            List<string> input = LoadInput();
            List<string> oxygen = GetOxygenList(input);
            List<string> co = GetCoList(input);
            int oxDec = GetDecimal(oxygen[0]);
            int coDec = GetDecimal(co[0]);
            Console.WriteLine(SupportRating(oxDec, coDec));
        }
        static List<string> testUnit = new List<string>
        {
            "00100",
            "11110",
            "10110",
            "10111",
            "10101",
            "01111",
            "00111",
            "11100",
            "10000",
            "11001",
            "00010",
            "01010"
        };
    }
}
