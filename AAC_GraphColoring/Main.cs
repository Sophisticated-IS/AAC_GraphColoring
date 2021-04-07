using System;
using System.Runtime.InteropServices;
using AAC_Graph.Сервисы.FileIO;
using AAC_Graph.Сервисы.Randomizer;

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

            const int dimension = 11;
            var ewfedsw = Randomizer.GetRandomAdjacencyMatrix(dimension);
                //todo hard code path
            FileReaderWriter.WriteAdjacencyMatrix(
                @"C:\Users\Sova IS\RiderProjects\AAC_Graph\AAC_GraphColoring\ФайлыВвода_Вывода\Input.txt"
                ,ref ewfedsw);

            var dataFromFile = FileReaderWriter.ReadAdjacencyMatrix(
                @"C:\Users\Sova IS\RiderProjects\AAC_Graph\AAC_GraphColoring\ФайлыВвода_Вывода\Input.txt"
                , dimension);
            
            
            var algo = new GreedyTrivialAlgorithm();
            var algo2 = new RankGreedyAlgorithm();
            var f1 = algo.ColorGraph(dimension, ref dataFromFile);
            var f2 = algo2.ColorGraph(dimension, ref dataFromFile);
            Console.WriteLine($"Greedy: {f1.colorsAmount}");
            Console.WriteLine($"Жадный: {f2.colorsAmount}");
            
            var govno = new BacktrackingAlgorithm();
            
            var resBack = govno.ColorGraph(dimension, ref dataFromFile);
            Console.WriteLine("Backtracking:" + resBack.colorsAmount);

           // BenchmarkRunner.Run<BacktrackingAlgorithmBenchmark>();


            Console.ReadLine();
        }
    }
}