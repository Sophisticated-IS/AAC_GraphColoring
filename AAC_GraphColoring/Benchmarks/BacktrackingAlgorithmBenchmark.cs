using System.Collections.Generic;
using AAC_Graph.Модели;
using AAC_Graph.Сервисы.FileIO;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Order;

namespace AAC_Graph.Benchmarks
{
    [Orderer(SummaryOrderPolicy.FastestToSlowest)]
    [RankColumn]
    public class BacktrackingAlgorithmBenchmark
    {
        private static readonly GreedyTrivialAlgorithm GreedyTrivialAlgorithm = new GreedyTrivialAlgorithm();
        private static readonly IGraphColor _greedyAlgo = new RankGreedyAlgorithm();

        #region Benchmarks

        [Benchmark]
        [ArgumentsSource(nameof(ArgumentsProvider))]
        public void BenchmarkBacktrackingAlgo(int dim, ref byte[,] matrix)
        {
            GreedyTrivialAlgorithm.ColorGraph(dim, ref matrix);
        }


        [Benchmark]
        [ArgumentsSource(nameof(ArgumentsProvider))]
        public void BenchmarkGreedyAlgo(int dim, ref byte[,] matrix)
        {
            _greedyAlgo.ColorGraph(dim, ref matrix);
        }

        #endregion

        #region Data Providers

        public IEnumerable<object[]> ArgumentsProvider()
        {
            var dataFromFile = FileReaderWriter.ReadAdjacencyMatrix(@"ФайлыВвода_Вывода\Input.txt", 10000);
            yield return new object[] {10000, dataFromFile};
        }

        #endregion
        
    }
}