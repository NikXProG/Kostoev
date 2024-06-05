using System;


namespace sort_application
{
    class SortAndSearch
    {
        public static void BubbleSort(int[] array)
        {
            for (int i = 0; i < array.Length - 1; i++)
            {
                for (int j = 0; j < array.Length - i - 1; j++)
                {
                    if (array[j] > array[j + 1])
                    {
                        array[j] ^= array[j + 1];
                        array[j + 1] ^= array[j];
                        array[j] ^= array[j + 1];
                    }
                }
            }
        }

        public static void SelectionSort(int[] array)
        {


            for (int i = 0; i < array.Length - 1; i++)
            {
                int minIndex = i;
                for (int j = i + 1; j < array.Length; j++)
                {
                    if (array[j] < array[minIndex])
                    {
                        minIndex = j;
                    }
                }

                if (minIndex != i)
                {
                    array[i] ^= array[minIndex];
                    array[minIndex] ^= array[i];
                    array[i] ^= array[minIndex];
                }
            }
        }

        public static void InsertionSort(int[] array)
        {
            for (int i = 1; i < array.Length; i++)
            {
                int current = array[i];
                int j = i - 1;

                while (j >= 0 && array[j] > current)
                {
                    array[j + 1] = array[j];
                    j--;
                }

                array[j + 1] = current;
            }
        }

        public static int BinarySearch(int[] array, int value)

        {
            int min = 0;
            int max = array.Length - 1;
            while (min <= max)
            {
                int median = (min + max) / 2;
                if (value == array[median])
                {
                    return median;
                }

                else if (value < array[median])
                {
                    max = median - 1;
                }
                else
                {
                    min = median + 1;
                }
            }
            return -1;
        }

    }

    class Program
    {
        public static void Main(string[] args)
        {


            int[] ArrayUnsorted1 = new int[] { 3, 4, 2, 1, 5 }; // Bubble Sorted
            SortAndSearch.BubbleSort(ArrayUnsorted1);

            int[] ArrayUnsorted2 = new int[] { 3, 4, 2, 1, 5 }; // Selection Sorted
            SortAndSearch.SelectionSort(ArrayUnsorted2);

            int[] ArrayUnsorted3 = new int[] { 3, 4, 2, 1, 5 }; // Insertion Sorted
            SortAndSearch.InsertionSort(ArrayUnsorted3);

            int[] ArrayUnsorted4 = new int[] { 1, 2, 3, 4, 5, 6, 8, 12 }; // binary search algorithm for sorted array
            int valueSearch = 2;
            int index = SortAndSearch.BinarySearch(ArrayUnsorted4, valueSearch);

            Console.WriteLine("Sorted Bubble Array:");
            foreach (int num in ArrayUnsorted1) Console.Write(num + " ");
            Console.WriteLine("\nSorted Selection Array:");
            foreach (int num in ArrayUnsorted2) Console.Write(num + " ");
            Console.WriteLine("\nSorted Insertion Array:");
            foreach (int num in ArrayUnsorted3) Console.Write(num + " ");
            Console.WriteLine("\nbinary search algorithm: \nindex in array element {0}: {1}", valueSearch, index);

        }
    }
}