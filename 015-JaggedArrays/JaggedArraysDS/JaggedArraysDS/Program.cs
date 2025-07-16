using System.Security.Cryptography;

namespace JaggedArraysDS
{
    internal class Program
    {

        static void example1()
        {
            int[][] jaggedArray = new int[3][];
            jaggedArray[0] = [0, 2, 5, 6, 5, 8];
            jaggedArray[1] = [5, 88, 5, 6, 4];
            jaggedArray[2] = [5, 6, 9, 4, 1, 3, 7];

            for (int i = 0; i < jaggedArray.Length; i++)
            {
                Console.Write($"Array[{i}]: {jaggedArray[i][0]}");
                for (int j = 1; j < jaggedArray[i].Length; j++)
                    Console.Write($", {jaggedArray[i][j]}");
                Console.WriteLine();
            }
        }
        static void example2()
        {
            int[][] map =new int[3][];

            map[0] = [5, 4, 5, 5, 6, 5, 2, 3];
            map[1] = [5, 5, 8, 6, 1, 8, 4];
            map[2] = [9, 7, 3, 6, 65, 14];
            
            for(int i = 0; i < map.Length; i++)
            {
                Console.Write($"Array[{i}]: {map[i][0]}");
                for (int j = 1; j < map[i].Length; j++)
                    Console.Write($", {map[i][j]}");
                Console.WriteLine();
            }

        }
        static void example3(int[][] arr)
        {
            int sum = arr.SelectMany(x => x).Sum();
            Console.WriteLine($"sum: {sum}");
        }
        static void example4(int[][] arr)
        {
            var subArrays = arr.Where(subArr => subArr.Length > 3);
            bool first_time = true;

            Console.WriteLine("Element in row: ");


            foreach(var subArr in subArrays)
            {
                Console.Write($"\nArray: {subArr[0]}");
                first_time = true;
                foreach (var num in subArr)
                {
                    if(!first_time)
                        Console.Write($", {num}");
                    first_time = false;
                }
            }
        }
        static void example5(int[][] arr)
        {
            int max = arr.SelectMany((x) => x).Max();

            Console.WriteLine($"max: {max}");
        }
        static void Main(string[] args)
        {
            int[][] arr =
            {
                [1,2,3,545],
                [4,5,6],
                [7,8,9, 54],
            };

            //example1();
            //example2();
            //example3(arr);
            //example4(arr);
            example5(arr);
        }
    }
}
