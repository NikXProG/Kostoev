using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace BaseAlgorithmGraph
{
    public static class MatrixMethods
    {

        public class AdjacencyMatrix
        {
            
            public readonly int CountVertex;
            public readonly int[,] Matrix;
            
            //constructors
            public AdjacencyMatrix() : this(3) { }
            public AdjacencyMatrix(int countVertex)  
            {
                this.CountVertex = countVertex;
                Matrix = new int[countVertex, countVertex];
            }

            public void CreateMatrix()
            {
                // matrix indexing
                for (int i = 0; i < this.CountVertex; i++)
                {

                    this.Matrix[i, i] = 0;
                    for (int j = i + 1; j < this.CountVertex; j++)
                    {
                        Console.WriteLine($"distance between {i + 1} -> {j + 1}");

                        this.Matrix[j, i] = this.Matrix[i, j] = Convert.ToInt32(Console.ReadLine());
                    }

                }
            }
            public void InfoMatrix()
            {
                if (!this.IsEmpty())
                {
                    // matrix output        
                    Console.WriteLine("  " + string.Join(" ", Enumerable.Range(1, this.CountVertex)));
                    for (int i = 0; i < this.CountVertex; i++)
                    {
                        Console.Write(i + 1 + " ");
                        for (int j = 0; j < this.CountVertex; j++) Console.Write(this.Matrix[j, i] + " ");
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
                for (int i = 0; i < this.CountVertex; i++)
                {
                    for (int j = 0; j < this.CountVertex; j++)
                    {
                        if (this.Matrix[i, j] != 0)
                        {
                            return false; // Если найден ненулевой элемент, матрица не пуста
                        }
                    }
                }
                return true; // Если не найдено ненулевых элементов, матрица пуста
            }           

        }

        #region AlgorithmsBase
        
        public static class DegreeSequenceReducer
        {
            /*public static void ok()
            {
                int[] minSumVertex = new int[matrix.CountVertex];
                
                for (int i = 0; i < matrix.CountVertex; i++)
                {
                    int sum = 0;
                    for (int j = 0; j < matrix.CountVertex; j++)
                    {
                        sum += matrix.Matrix[i, j];
                    }
                    minSumVertex[i] = sum;
                }
                Console.WriteLine(string.Join(" ", minSumVertex));
            }*/
            public static AdjacencyMatrix Realize(int[] vectorDegreeVertex)
            {
                if (vectorDegreeVertex.Sum() % 2 == 1) {return null;}
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
                    
                    if (vectorDegreeVertex.Sum() % 2 == 1) {return null;}
                    // Console.WriteLine();
                }
                
                return matrix;


            }
        }
        public static class PruferCode
        {
            public static AdjacencyMatrix EncoderFromCode(string code)
            {
                char maxVertex = code.Max();
                int[] pruferCodeArray = code.Select(c => int.Parse(c.ToString())).ToArray();
                int countVertex = (maxVertex - '0') + code.Count(c => c == maxVertex) - 1;
                AdjacencyMatrix matrix = new AdjacencyMatrix(countVertex);               
                
                int[] vertices = Enumerable.Range(1, countVertex).ToArray();
                
                while (pruferCodeArray.Length != 0)
                {
                    int u = pruferCodeArray[0];
                    
                    int minVertex = vertices.Except(pruferCodeArray).Min();
                    
                    matrix.Matrix[u-1, minVertex-1] = matrix.Matrix[minVertex-1,u-1] = 1;

                    pruferCodeArray = pruferCodeArray.Skip(1).ToArray();
                    
                    var tmp = new List<int>(vertices); 
                    tmp.RemoveAt(Array.IndexOf(vertices, minVertex)); 
                    vertices = tmp.ToArray(); 
            
                }
                return matrix;
            }
            public static string EncoderFromMatrix(AdjacencyMatrix matrix)
            {
                int[] pruferCodeArray = new int[matrix.CountVertex-1];
                int index = 0;
                int i;
                for (i = 0; i < matrix.CountVertex-2; i++)
                {                    

                    int minHangingVertex = -1; 
                    int indexHangingVertex = 10000;
                    int minHanging = 10000;
                    for (int j = 0; j < matrix.CountVertex; j++)
                    {
                        int sum = 0;

                        for (int k = 0; k < matrix.CountVertex; k++)
                        {
                            sum+= matrix.Matrix[j,k]; 

                        }

                        if ((sum == 1) && i < minHanging)
                        {
                            minHanging = i;
                            minHangingVertex = j;

                        }
                    }

                    
                    for (int j = 0; j < matrix.CountVertex; j++)
                    {
                        if (matrix.Matrix[minHangingVertex, j] == 1)
                        {
                            pruferCodeArray[i] = j+1;
                            matrix.Matrix[minHangingVertex, j] = 0;
                            matrix.Matrix[j, minHangingVertex] = 0;
                        }
                    }
                    
                    
                    
                }
                for (int i_last = 0; i_last < matrix.CountVertex; i_last++)
                {
                    for (int j_last = 0; j_last < matrix.CountVertex; j_last++)
                    {
                        if (matrix.Matrix[i_last, j_last] == 1)
                        {
                            
                            pruferCodeArray[i] = j_last+1;
                            break;
                        }
                    }

                    if (pruferCodeArray[i] != 0)
                    {
                        break;
                    }
                }



                return string.Join("",pruferCodeArray.Where(d => d != 0));
            }
        }   
        public static class DijkstraAlgorithm
        {
            public static int Realize(AdjacencyMatrix matrix, int startVertex, int endVertex)
            {
                if (matrix.IsEmpty()) {Console.WriteLine("Матрица пуста"); return 1;}
                
                int[] distances = new int[matrix.CountVertex];
                int[] vertices = new int[matrix.CountVertex];
                
                for (int i = 0; i <matrix.CountVertex; i++)
                {
                    distances[i] = 10000;
                    vertices[i] = 1;
                }

                distances[startVertex-1] = 0;
                int minIndex, min, temp;
                do
                {

                    min = minIndex = 10000;
                    for (int i = 0; i < matrix.CountVertex; i++)
                    {
                        if ((vertices[i] == 1) && (distances[i] < min))
                        {
                            min = distances[i];

                            minIndex = i;


                        }

                    }

                    if (minIndex != 10000)
                    {
                        for (int i = 0; i < matrix.CountVertex; i++)
                        {
                            if (matrix.Matrix[minIndex, i] != 0)
                            {

                                temp = min + matrix.Matrix[minIndex, i];

                                if (temp < distances[i])
                                {

                                    distances[i] = temp;
                                }

                            }
                        }

                        vertices[minIndex] = 0;
                    }
                } while (minIndex < 10000);

                foreach (int i in distances)
                {
                    Console.Write(i + " ");
                }

                Console.WriteLine();
                
                #region path_restoration
                
                int[] ver = new int[matrix.CountVertex];

                int endIndex = endVertex - 1;
                ver[0] = endVertex;
                int k = 1;
                int weight = distances[endIndex];

                while (endIndex != startVertex)
                {
                    for (int i = 0; i < matrix.CountVertex; i++)
                        if (matrix.Matrix[i, endIndex] != 0)
                        {
                            temp = weight - matrix.Matrix[i, endIndex];
                            if (temp == distances[i])
                            {
                                weight = temp;
                                endIndex = i;
                                ver[k] = i + 1;
                                k++;
                            }
                        }
                }

                for (int i = k - 1; i >= 0; i--)
                {
                    Console.Write(" " + ver[i]);                   
                }
                Console.WriteLine();
                #endregion
                
                return 0;
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
       
        #endregion
        
        public static int Main()
        {
            try
            {
                
                const int countVertex = 3;
                const int startVertex = 1;
                const int endVertex = 2;                
                
                //AdjacencyMatrix adjacencyMatrix = new AdjacencyMatrix(6);
                //adjacencyMatrix.CreateMatrix();
                //adjacencyMatrix.InfoMatrix();  

                //DijkstraAlgorithm.Realize(adjacencyMatrix, 0, 3);
                //FindGraphMetrics.Realize(adjacencyMatrix);

                int[] vectorVertex = {5, 2, 3, 2, 1, 1};
                //AdjacencyMatrix adjacencyMatrix = new AdjacencyMatrix(vectorVertex.Length);
                //adjacencyMatrix = DegreeSequenceReducer.Realize(vectorVertex);
                //adjacencyMatrix.InfoMatrix();
                AdjacencyMatrix adjacencyMatrix = PruferCode.EncoderFromCode("234");
                
                Console.WriteLine(PruferCode.EncoderFromMatrix(adjacencyMatrix));

                
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
