using System;
using System.Linq;

namespace BaseAlgorithmGraph
{
    public static class MatrixMethods
    {
        public class AdjacencyMatrix
        {
            public readonly int CountVertex;
            public readonly int[,] Matrix;

            public AdjacencyMatrix() : this(3) { }
            public AdjacencyMatrix(int countVertex)
            {
                CountVertex = countVertex;
                Matrix = new int[countVertex, countVertex];
            }

            public void CreateMatrix()
            {
                for (int i = 0; i < CountVertex; i++)
                {
                    Matrix[i, i] = 0;
                    for (int j = i + 1; j < CountVertex; j++)
                    {
                        Console.WriteLine($"distance between {i + 1} -> {j + 1}");
                        Matrix[i, j] = Matrix[j, i] = Convert.ToInt32(Console.ReadLine());
                    }
                }
            }

            public void InfoMatrix()
            {
                if (!IsEmpty())
                {
                    Console.WriteLine("  " + string.Join(" ", Enumerable.Range(1, CountVertex)));
                    for (int i = 0; i < CountVertex; i++)
                    {
                        Console.Write(i + 1 + " ");
                        for (int j = 0; j < CountVertex; j++) Console.Write(Matrix[j, i] + " ");
                        Console.WriteLine();
                    }
                }
                else
                {
                    Console.WriteLine("Матрица пуста");
                }
            }

            public bool IsEmpty()
            {
                for (int i = 0; i < CountVertex; i++)
                {
                    for (int j = 0; j < CountVertex; j++)
                    {
                        if (Matrix[i, j] != 0)
                        {
                            return false;
                        }
                    }
                }
                return true;
            }
        }

        #region AlgorithmsBase
        public static class DegreeSequenceReducer
        {
            public static AdjacencyMatrix Realize(int[] vectorDegreeVertex)
            {
                if (vectorDegreeVertex.Sum() % 2 == 1) { return null; }
                /*Console.WriteLine("Редукционный алгоритм:");
                Console.WriteLine(string.Join(" ", vectorDegreeVertex));*/
                AdjacencyMatrix matrix = new AdjacencyMatrix(vectorDegreeVertex.Length);

                while (vectorDegreeVertex.Sum() > 0)
                {
                    int maxDegree = vectorDegreeVertex.Max();
                    int maxIndexDegree = Array.IndexOf(vectorDegreeVertex, maxDegree);
                    vectorDegreeVertex[maxIndexDegree] = 0;

                    for (int j = 0; j < vectorDegreeVertex.Length; j++)
                    {
                        if (vectorDegreeVertex[j] != 0)
                        {
                            vectorDegreeVertex[j] -= 1;
                            matrix.Matrix[maxIndexDegree, j] = matrix.Matrix[j, maxIndexDegree] = 1;
                        }
                        // Console.Write($"{vectorDegreeVertex[j]} ");

                    }

                    if (vectorDegreeVertex.Sum() % 2 == 1) { return null; }
                    // Console.WriteLine();
                }

                return matrix;


            }
        }
        public static class DijkstraAlgorithm
        {
            public static void Realize(AdjacencyMatrix matrix, int startVertex, int endVertex)
            {
                if (matrix.IsEmpty())
                {
                    Console.WriteLine("Матрица пуста");
                    return;
                }

                int[] distances = new int[matrix.CountVertex];
                bool[] visited = new bool[matrix.CountVertex];
                int[] previous = new int[matrix.CountVertex];

                // Инициализация массива расстояний
                for (int i = 0; i < matrix.CountVertex; i++)
                {
                    distances[i] = int.MaxValue;
                    visited[i] = false;
                    previous[i] = -1;
                }

                // Расстояние до начальной вершины равно 0
                distances[startVertex - 1] = 0;

                // Находим кратчайший путь для всех вершин
                for (int count = 0; count < matrix.CountVertex - 1; count++)
                {
                    // Находим вершину с минимальным расстоянием
                    int minDistance = int.MaxValue;
                    int minIndex = -1;
                    for (int v = 0; v < matrix.CountVertex; v++)
                    {
                        if (!visited[v] && distances[v] <= minDistance)
                        {
                            minDistance = distances[v];
                            minIndex = v;
                        }
                    }

                    // Помечаем выбранную вершину как посещенную
                    visited[minIndex] = true;

                    // Обновляем расстояния до смежных вершин
                    for (int v = 0; v < matrix.CountVertex; v++)
                    {
                        if (!visited[v] && matrix.Matrix[minIndex, v] != 0 && distances[minIndex] != int.MaxValue &&
                            distances[minIndex] + matrix.Matrix[minIndex, v] < distances[v])
                        {
                            distances[v] = distances[minIndex] + matrix.Matrix[minIndex, v];
                            previous[v] = minIndex;
                        }
                    }
                }

                // Выводим кратчайшие расстояния до всех вершин
                Console.WriteLine("Кратчайшие расстояния до всех вершин:");
                for (int i = 0; i < matrix.CountVertex; i++)
                {
                    Console.WriteLine($"До вершины {i + 1}: {distances[i]}");
                }

                // Восстанавливаем путь от конечной вершины до начальной
                Console.WriteLine("Кратчайший путь от конечной вершины до начальной:");
                PrintPath(endVertex - 1, previous);
            }

            private static void PrintPath(int currentVertex, int[] previous)
            {
                if (currentVertex == -1)
                {
                    return;
                }

                PrintPath(previous[currentVertex], previous);
                Console.Write($"{currentVertex + 1} ");
            }
        }
        public static class FindGraphMetrics
        {
            public static int Realize(AdjacencyMatrix matrix)
            {
                int[] eArray = new int[matrix.CountVertex];
                int r, d, e = 0;
                for (int i = 0; i < matrix.CountVertex; i++)
                {

                    for (int j = 0; j < matrix.CountVertex; j++)
                    {
                        if (matrix.Matrix[i, j] > e)
                        {
                            e = matrix.Matrix[i, j];
                        }
                    }

                    eArray[i] = e;
                }

                Console.WriteLine($"Числовые характеристики графа:");
                int index = 1;
                foreach (var i in eArray)
                {
                    Console.WriteLine($"{index++} вершина: e = {i}");
                }
                Console.WriteLine($"d = {eArray.Max()}");
                Console.WriteLine($"r = {eArray.Min()}");
                return 0;
            }
        }

        public static class PruferCode
        {
            public static AdjacencyMatrix EncoderFromCode(string code)
            {
                int[] pruferCodeArray = code.Select(c => int.Parse(c.ToString())).ToArray();
                int countVertex = pruferCodeArray.Length + 2;
                AdjacencyMatrix matrix = new AdjacencyMatrix(countVertex);
                int[] degree = new int[countVertex];

                for (int i = 0; i < countVertex; i++) degree[i] = 1;
                for (int i = 0; i < pruferCodeArray.Length; i++) degree[pruferCodeArray[i] - 1]++;

                for (int i = 0; i < pruferCodeArray.Length; i++)
                {
                    for (int j = 0; j < countVertex; j++)
                    {
                        if (degree[j] == 1)
                        {
                            matrix.Matrix[j, pruferCodeArray[i] - 1] = matrix.Matrix[pruferCodeArray[i] - 1, j] = 1;
                            degree[j]--;
                            degree[pruferCodeArray[i] - 1]--;
                            break;
                        }
                    }
                }

                int u = -1, v = -1;
                for (int i = 0; i < countVertex; i++)
                {
                    if (degree[i] == 1)
                    {
                        if (u == -1) u = i;
                        else v = i;
                    }
                }

                matrix.Matrix[u, v] = matrix.Matrix[v, u] = 1;

                return matrix;
            }

            public static string EncoderFromMatrix(AdjacencyMatrix matrix)
            {
                int[] degree = new int[matrix.CountVertex];
                for (int i = 0; i < matrix.CountVertex; i++)
                {
                    for (int j = 0; j < matrix.CountVertex; j++)
                    {
                        if (matrix.Matrix[i, j] == 1)
                        {
                            degree[i]++;
                        }
                    }
                }

                int[] pruferCodeArray = new int[matrix.CountVertex - 2];
                for (int k = 0; k < pruferCodeArray.Length; k++)
                {
                    for (int i = 0; i < matrix.CountVertex; i++)
                    {
                        if (degree[i] == 1)
                        {
                            for (int j = 0; j < matrix.CountVertex; j++)
                            {
                                if (matrix.Matrix[i, j] == 1)
                                {
                                    pruferCodeArray[k] = j + 1;
                                    matrix.Matrix[i, j] = matrix.Matrix[j, i] = 0;
                                    degree[i]--;
                                    degree[j]--;
                                    break;
                                }
                            }
                            break;
                        }
                    }
                }

                return string.Join("", pruferCodeArray);
            }
        }

        #endregion

        public static int Main()
        {
            try
            {
                AdjacencyMatrix adjacencyMatrix = new AdjacencyMatrix(4);
                adjacencyMatrix.CreateMatrix();
                Console.WriteLine();
                adjacencyMatrix.InfoMatrix();
                Console.WriteLine();

                DijkstraAlgorithm.Realize(adjacencyMatrix, 1, 3);
                Console.WriteLine();
                Console.WriteLine();
                FindGraphMetrics.Realize(adjacencyMatrix);
                Console.WriteLine();

                int[] vectorVertex = { 5, 2, 3, 2, 1, 1 };

                adjacencyMatrix = new AdjacencyMatrix(vectorVertex.Length);
                adjacencyMatrix = DegreeSequenceReducer.Realize(vectorVertex);
                adjacencyMatrix.InfoMatrix();
                Console.WriteLine();

                string code = "1123";
                //string code = "234";
                adjacencyMatrix = PruferCode.EncoderFromCode(code);
                Console.WriteLine();
                adjacencyMatrix.InfoMatrix();

                string encoded = PruferCode.EncoderFromMatrix(adjacencyMatrix);
                Console.WriteLine();
                Console.WriteLine(encoded);

                return 0;
            }
            catch (Exception e)
            {
                Console.WriteLine($"Processing failed: {e.Message}");
                return 1;
            }
        }
    }
}