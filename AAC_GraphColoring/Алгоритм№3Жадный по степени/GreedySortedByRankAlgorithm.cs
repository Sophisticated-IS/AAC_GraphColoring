using System.Collections.Generic;
using System.Linq;
using AAC_Graph.Модели;

namespace AAC_Graph
{
    public sealed class GreedySortedByRankAlgorithm : IGraphColor
    {
        public (int colorsAmount, int[] coloredNodes) ColorGraph(int dim, ref byte[,] adjacencyMatrix)
        {
            //структура номер вершины - цвет уже отсортированных по рангу 
            var sortedNodesByRank = GetSortedNodesByRank(dim, ref adjacencyMatrix);

            //пока есть еще не раскрашенная вершина
            int nodeIndex;
            var color = 0;
            while ((nodeIndex = FindFirstNotColoredNodeIndex(ref sortedNodesByRank)) != -1)
            {
                //список вершин одного цвета
                var nodesColoredBySameColor = new List<NodeNumberRank>();

                //окрасим первую выбранную вершину
               
                sortedNodesByRank[nodeIndex].nodeRank = color;
                nodesColoredBySameColor.Add(sortedNodesByRank[nodeIndex]);

                //цикл по всем вершинам
                for (var i = 0; i < dim; i++)
                {
                    //если вершина не окрашена
                    if (sortedNodesByRank[i].nodeRank == -1)
                    {
                        //если вершина НЕ смежна ни с одной уже окрашенной вершине этим же цветом,то её можно покрасить этим же цветом
                        if (!IsNodeAdjacentToColoredOnes(ref nodesColoredBySameColor, ref adjacencyMatrix, ref sortedNodesByRank[i]))
                        {
                            sortedNodesByRank[i].nodeRank = color;
                            nodesColoredBySameColor.Add(sortedNodesByRank[i]);
                        }
                    }
                }

                color++;
            }

            var result = GetResult(dim, ref sortedNodesByRank);
            return (result.Max() + 1, result);
        }

        /// <summary>
        /// Получить отсортированный список вершин по степени
        /// </summary>
        private NodeNumberRank[] GetSortedNodesByRank(int dim, ref byte[,] adjacencyMatrix)
        {
            var nodeRankList = new NodeNumberRank[dim];
            //цикл по каждой строке
            for (var i = 0; i < dim; i++)
            {
                var rank = 0;
                //цикл по каждому столбцу
                for (int j = 0; j < dim; j++)
                {
                    //добавим степень если вершина смежна с нашей
                    rank += adjacencyMatrix[i, j];
                }

                nodeRankList[i] = new NodeNumberRank(i, rank);
            }

            QuickSort(ref nodeRankList, 0, nodeRankList.Length - 1);

            //теперь поле nodeRank будет отвечать за цвет
            //инициализируем все вершины как не раскрашенные
            for (int i = 0; i < nodeRankList.Length; i++)
            {
                nodeRankList[i].nodeRank = -1;
            }

            return nodeRankList;
        }

        /// <summary>
        /// Находит индекс первой незакрашенной вершины в списке
        /// </summary>
        /// <param name="sortedNodesByRank"></param>
        private int FindFirstNotColoredNodeIndex(ref NodeNumberRank[] sortedNodesByRank)
        {
            //цикл по отсортированному списку вершин по рангу
            for (int i = 0; i < sortedNodesByRank.Length; i++)
            {
                if (sortedNodesByRank[i].nodeRank == -1)
                {
                    return i;
                }
            }

            return -1;
        }

        /// <summary>
        /// Проверяет смежна ли вершина с какой либо другой вершиной окрашенной в этот же цвет
        /// </summary>
        /// <param name="coloredNodes"> список окрашенных вершин в выбранный цвет</param>
        /// <param name="adjacencyMatrix">матрица смежности</param>
        /// <param name="coloringNode">окрашиваемая вершина</param>
        private bool IsNodeAdjacentToColoredOnes(ref List<NodeNumberRank> coloredNodes, ref byte[,] adjacencyMatrix,
            ref NodeNumberRank coloringNode)
        {
            //цикл по всем вершинам уже окрашенным выбранным цветом
            for (int i = 0; i < coloredNodes.Count; i++)
            {
                if (adjacencyMatrix[coloringNode.nodeNumber, coloredNodes[i].nodeNumber] == 1)
                {
                    return true;
                }
            }

            //все покрашенные вершины оказались не смежными с текущей вершиной, следовательно её можно будет покрасить тем же цветом
            return false;
        }

        /// <summary>
        /// Конвертирует структуру в массив, где индекс - номер узла, а значение - цвет 
        /// </summary>
        /// <param name="dim">размерность</param>
        /// <param name="sortedNodesByRank">список вершин отсортированных по степени</param>
        private int[] GetResult(int dim,ref NodeNumberRank[] sortedNodesByRank )
        {
            var nodeIndexColor = new int[dim];
            for (int i = 0; i < dim; i++)
            {
                var node = sortedNodesByRank[i];
                nodeIndexColor[node.nodeNumber] = node.nodeRank;
            }

            return nodeIndexColor;
        }
        
        
        # region QUICK SORT IMPLEMENTATION

        //метод для обмена элементов массива
        private void Swap(ref int x, ref int y)
        {
            var t = x;
            x = y;
            y = t;
        }

        //метод возвращающий индекс опорного элемента
        private int Partition(NodeNumberRank[] array, int minIndex, int maxIndex)
        {
            var pivot = minIndex - 1;
            for (var i = minIndex; i < maxIndex; i++)
            {
                if (array[i].nodeRank > array[maxIndex].nodeRank)
                {
                    pivot++;
                    Swap(ref array[pivot].nodeRank, ref array[i].nodeRank);
                    Swap(ref array[pivot].nodeNumber, ref array[i].nodeNumber);
                }
            }

            pivot++;
            Swap(ref array[pivot].nodeRank, ref array[maxIndex].nodeRank);
            Swap(ref array[pivot].nodeNumber, ref array[maxIndex].nodeNumber);
            return pivot;
        }

        //быстрая сортировка
        public void QuickSort(ref NodeNumberRank[] array, int minIndex, int maxIndex)
        {
            while (true)
            {
                if (minIndex >= maxIndex)
                {
                    return;
                }

                var pivotIndex = Partition(array, minIndex, maxIndex);
                QuickSort(ref array, minIndex, pivotIndex - 1);
                minIndex = pivotIndex + 1;
            }
        }

        #endregion
    }
}