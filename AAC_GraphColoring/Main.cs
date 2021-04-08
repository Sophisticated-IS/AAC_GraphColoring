using System;
using System.Collections.Generic;
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

            BenchmarkRunner.Run<GraphColoringAlgorithmsBenchmark>();
             
            // StartProgramMenu();

            Console.WriteLine("Введите любую клавишу для выхода ...");
            Console.ReadLine();
        }

        [Obsolete]
        public static void StartProgramMenu()
        {
            var menuList = new List<string>()
            {
                "1 - Запустить авто - бенчмарк",
                "2 - Запустить ручное тестирование"
            };
            
            Console.WriteLine("Здравствуйте! Вас приветствует программа для расчета хроматического числа графа)");
            Console.WriteLine("Выберете, что вы бы хотели сделать?");

            foreach (var menuItem in menuList)
            {
                Console.WriteLine(menuItem);
            }
            
            m1: var key = Console.ReadKey();
            Console.WriteLine();
            int numberAlgo;
            switch (key.KeyChar)
            {
                case '1':
                    numberAlgo = ChooseAlgo(); 
                    
                    break;
                case '2':
                    numberAlgo = ChooseAlgo();
                    break;
                
                default:
                    Console.WriteLine("Неверно введен пункт меню");
                    Console.WriteLine("Введите номер пункта заново!");
                    goto m1;
            }
        }

        public static int ChooseAlgo()
        {
            var menuList = new List<string>()
            {
                "1 - Алгоритм перебора",
                "2 - Алгоритм жадный тривиальный",
                "3 - Алгоритм жадный с сортировкой по невозрастанию степени вершин"
            }; 
                m1: var key = Console.ReadKey();
                Console.WriteLine();
                switch (key.KeyChar)
                {
                case '1':
                    return 1;
                case '2':
                    return 2;
                case '3':
                    return 3; 
                
                default:
                Console.WriteLine("Неверно введен пункт меню");
                Console.WriteLine("Введите номер пункта заново!");
                goto m1;
            }
          
        }

        // public static int ChooseDimension()
        // {
        //     Console.WriteLine("Введите размерность");
        //     Console.ReadLine("")
        // }
    }
}