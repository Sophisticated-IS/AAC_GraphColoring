using System;
using System.Runtime.InteropServices;
using AAC_Graph.Benchmarks;
using AAC_Graph.Сервисы.FileIO;
using AAC_Graph.Сервисы.Randomizer;
using BenchmarkDotNet.Running;

namespace AAC_Graph
{
    internal class Program
    {
        #region DLLImports

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern bool SetConsoleMode(IntPtr hConsoleHandle, int mode);

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern bool GetConsoleMode(IntPtr handle, out int mode);

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern IntPtr GetStdHandle(int handle);

        #endregion


        public static void Main(string[] args)
        {
            #region ConsoleConfigurationStuff

            var handle = GetStdHandle(-11);
            int mode;
            GetConsoleMode(handle, out mode);
            SetConsoleMode(handle, mode | 0x4);

            #endregion

             // BenchmarkRunner.Run<GraphColoringAlgorithmsBenchmark>();
            
             // Console.WriteLine(datenow);
            // const int dim = 11;
            //
            // var data = Randomizer.GetRandomAdjacencyMatrix(dim);
            //
            // var backTrackingAlgo = new BacktrackingAlgorithm();
            // var greedyTrivial = new GreedyTrivialAlgorithm();
            // var greedyRank = new GreedySortedByRankAlgorithm();
            // var r1 = backTrackingAlgo.ColorGraph(dim, ref data);
            // var r2 = greedyTrivial.ColorGraph(dim, ref data);
            // var r3 = greedyRank.ColorGraph(dim, ref data);
            //
            // Console.WriteLine(DateTime.Now.Subtract(datenow).TotalSeconds);
            // Console.WriteLine($"Перебор:{r1.colorsAmount}");
            // Console.WriteLine($"Жадный со степенями:{r3.colorsAmount}");
            // Console.WriteLine($"Жадный тривиальный:{r2.colorsAmount}");
            // Console.WriteLine();

            Console.WriteLine("Введите любую клавишу для выхода ...");
            Console.ReadLine();
        }
    }
}