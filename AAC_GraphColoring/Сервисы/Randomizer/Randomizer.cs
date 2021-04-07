using System;

namespace AAC_Graph.Сервисы.Randomizer
{
    public static class Randomizer
    {
        public static byte[,] GetRandomAdjacencyMatrix(int dim)
        {
            var matrix = new byte[dim, dim];
            var rand = new Random();
            for (var i = 0; i < dim; i++)
            {
                for (var j = 0; j <= i; j++)
                {
                    
                    if (i == j)
                    {
                        matrix[i, j] = 0;
                    }
                    else
                    {
                        matrix[i, j] = (byte)rand.Next(0, 2);
                        matrix[j, i] = matrix[i, j];
                    }
                    
                }
            }

            return matrix;
        }
    }
}