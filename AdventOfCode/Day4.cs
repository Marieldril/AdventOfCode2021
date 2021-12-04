using System;
using System.Collections.Generic;
using System.IO;

namespace AdventOfCode
{
    class Day4
    {
        struct Table
        {
            public Table(int size)
            {
                table = new int[size, size];
                keys = new int[size, size];
            }
            public int[,] table;
            public int[,] keys;
        }
        struct Data
        {
            public Data(int a = 0)
            {
                Numbers = new List<int>();
                Tables = new List<Table>();
            }
            public List<int> Numbers { get; set; }
            public List<Table> Tables { get; set; }
        }
        static (Table, int) CheckForWin(Table table, int digit)
        {
            for (int i = 0; i < 5; ++i)
                for (int j = 0; j < 5; ++j)
                    if (table.table[i, j] == digit)
                        table.keys[i, j] = 1;

            for (int i = 0; i < 5; ++i)
            {
                int getFive = 0;
                for (int j = 0; j < 5; ++j)
                {
                    if (table.keys[i, j] == 0)
                        continue;
                    getFive += 1;
                    if (getFive == 5)
                        return (table, digit);
                }
                getFive = 0;
                for (int j = 0; j < 5; ++j)
                {
                    if (table.keys[j, i] == 0)
                        continue;
                    getFive += 1;
                    if (getFive == 5)
                        return (table, digit);
                }
            }
            return (table, 0);
        }
        static (Table, int) GetWinner(Data data)
        {
            foreach (var item in data.Numbers)
            {
                foreach (var table in data.Tables)
                {
                    (Table, int) CheckedTable = CheckForWin(table, item);
                    if (CheckedTable.Item2 != 0)
                        return CheckedTable;
                }
            }
            return (data.Tables[0], 0);
        }
        
        static (Table, int) GetLooser(Data data, int playersWonCounter = 0)
        {
            int[] playersWon = new int[data.Tables.Count];
            foreach (var item in data.Numbers)
            {
                int index = 0; 
                foreach (var table in data.Tables)
                {
                    if (playersWon[index] == 1)
                    {
                        index++;
                        continue;
                    }
                    (Table, int) CheckedTable = CheckForWin(table, item);
                    if (CheckedTable.Item2 == 0)
                    {
                        index++;
                        continue;
                    }
                    if (playersWonCounter == data.Tables.Count - 1)
                        return (table, item);
                    playersWon[index] = 1;
                    playersWonCounter += 1;
                    index++;
                }
            }
            return (data.Tables[0], 0);
        }
        static int GetSum(Data data, bool winner)
        {
            int sum = 0;
            (Table, int) table = winner ? GetWinner(data) : GetLooser(data);
            for (int i = 0; i < 5; ++i)
                for (int j = 0; j < 5; ++j)
                    if (table.Item1.keys[i, j] == 0)
                        sum += table.Item1.table[i, j];
            return sum * table.Item2;
        }
        static Data LoadInput()
        {
            Data input = new Data(0);
            string s;
            List<int> tableNumbers = new List<int>();
            int i = 0;
            using (StreamReader sr = File.OpenText("inputs/inputDay4.txt"))
                while ((s = sr.ReadLine()) != null)
                {
                    if (s == "") continue;
                    if (i == 0)
                    {
                        string[] st = s.Split(',');
                        foreach (var item in st)
                            input.Numbers.Add(int.Parse(item));
                        i++;
                    }
                    else
                    {
                        string[] st = s.Split(' ');
                        List<string> str = new List<string>();
                        foreach (var item in st)
                            if (item != "") str.Add(item);
                        foreach (var item in str)
                            tableNumbers.Add(int.Parse(item));
                    }
                };
            for (int j = 0, a = 0; j < tableNumbers.Count / 25; ++j)
            {
                Table tab = new Table(5);
                for (int k = 0; k < 5; ++k)
                    for (int l = 0; l < 5; ++l, ++a)
                        tab.table[k, l] = tableNumbers[a];
                input.Tables.Add(tab);
            }
            return input;
        }
        public static void PrintSolution1()
        {
            Data input = LoadInput();
            Console.WriteLine(GetSum(input, true));
        }
        public static void PrintSolution2()
        {
            Data input = LoadInput();
            Console.WriteLine(GetSum(input, false));
        }
        static Data testUnit = new Data(0)
        {
            Numbers = { 7, 4, 9, 5, 11, 17, 23, 2, 0, 14, 21, 24, 10, 16, 13, 6, 15, 25, 12, 22, 18, 20, 8, 19, 3, 26, 1 },
            Tables = new List<Table>
            {
                new Table
                {
                    table = new int[, ]
                    {
                        { 22, 13, 17, 11, 0 },
                        { 8, 2, 23, 4, 24 },
                        { 21, 9, 14, 16, 7 },
                        { 6, 10, 3, 18, 5 },
                        { 1, 12, 20, 15, 19 }
                    },
                    keys = new int[5, 5]
                },
                new Table
                {
                    table = new int[, ]
                    {
                        { 3, 15, 0, 2, 22 },
                        { 9, 18, 13, 17, 5 },
                        { 19, 8, 7, 25, 23 },
                        { 20, 11, 10, 24, 4 },
                        { 14, 21, 16, 12, 6 }
                    },
                    keys = new int[5, 5]
                },
                new Table
                {
                    table = new int[, ]
                    {
                        { 14, 21, 17, 24, 4 },
                        { 10, 16, 15, 9, 19 },
                        { 18, 8, 23, 26, 20 },
                        { 22, 11, 13, 6, 5 },
                        { 2, 0, 12, 3, 7 }
                    },
                    keys = new int[5, 5]
                },
            }
        };
    }
}