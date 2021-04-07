using System.IO;
using System.Linq;
using System.Text;

namespace AAC_Graph.Сервисы.FileIO
{
    public static class FileReaderWriter
    {
        public static byte[,] ReadAdjacencyMatrix(string filePath,int n)
        {
            if (!File.Exists(filePath)) return null;

            var adjMatrix = new byte[n,n];
            using (var fileReader = new StreamReader(filePath))
            {
                var index = 0;
                string line;
                while ((line = fileReader.ReadLine()) != null)
                {
                    var values = line.Split(',').Select(byte.Parse).ToArray();
                    for (var i = 0; i < n; i++)
                    {
                        adjMatrix[index, i] = values[i];
                    }

                    index++;
                }
            }

            return adjMatrix;
        }

        public static void WriteAdjacencyMatrix(string filePath,ref byte[,] adjacencyMatrix)
        {
            using (var fileStream = new FileStream(filePath,FileMode.Create))
            {
                var dimension = adjacencyMatrix.GetLength(0);
                for (var i = 0; i < dimension; i++)
                {
                    var line =GetWritingLine(adjacencyMatrix, dimension, i);
                    var data = Encoding.Default.GetBytes(line);
                    fileStream.Write(data,0,data.Length);
                }
            }
        }

        private static string GetWritingLine(in byte[,] adjacencyMatrix, int dimension, int i)
        {
            var writingLine = new StringBuilder();
            for (var j = 0; j < dimension; j++)
            {
                writingLine.Append(adjacencyMatrix[i, j]);
                writingLine.Append(',');
            }

            var line = writingLine.ToString().TrimEnd(',');
            line = line + "\n";
            return line;
        }
    }
}