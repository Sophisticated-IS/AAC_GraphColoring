using System;
using System.Runtime.InteropServices;
using AAC_Graph.Benchmarks;
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

           BenchmarkRunner.Run<GraphColoringAlgorithmsBenchmark>();
           
           Console.WriteLine("Введите любую клавишу для выхода ...");
           Console.ReadLine();
        }
    }
}