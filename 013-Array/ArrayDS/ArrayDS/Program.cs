using System.Security.Cryptography;
using System.Xml.Linq;

namespace ArrayDS
{
    internal class Program
    {
        static void example1()
        {
            int[] nums1 = new int[5];

            string[] names = { "Alice", "Bob", "Charlie" };

            Console.WriteLine($"Names Array: ");
            foreach (string name in names)
                Console.WriteLine(name);
        }
        static void example2()
        {
            int[] nums = { 1, 2, 3, 4, 5, 6 };

            nums[0] = 10;
            for(int i = 0; i < nums.Length; i++)
                Console.WriteLine($"Item in index {i} is: {nums[i]}");
        }
        static void example3()
        {
            int[] nums = { 5, 8, 1, 6, 3, 7, 9 };

            Console.WriteLine("Array befor sorting: ");
            foreach (int i in nums)
                Console.Write($"{i}, ");
            Array.Sort(nums);
            Console.WriteLine("\nArray after sorting: ");
            foreach (int i in nums)
                Console.Write($"{i}, ");

            int valueToFound = 6;
            int index = Array.IndexOf(nums, valueToFound);
            Console.WriteLine($"\nthe value {valueToFound} is in index: {index}");
        }
        static void example4()
        {
            int[,] matrix = { { 1, 2, 3 }, { 4, 5, 6 }, {7, 8, 9} };

            Console.WriteLine("Multidimensional Arrays: ");
            for(int i = 0; i < matrix.GetLength(0);i++)
            {
                for (int j = 0; j < matrix.GetLength(1);j++)
                {
                    Console.Write(matrix[i,j] + " ");
                }
                Console.WriteLine();
            }
        }
        static void example5()
        {
            int[] original = { 1, 2, 3, 4, 5, 6, 7 };
            int[] copy = new int[original.Length];

            Console.WriteLine("Copy Array:");
            Array.Copy(original, copy, original.Length);

            foreach (int i in copy) 
                Console.Write($"{i}, ");
            Console.WriteLine();
        }
        static void example6()
        {
            int[] nums = { 1, 2, 3, 4, 5, 6, 7, 8, 9};

            Console.WriteLine("Filter the even numbers and calculate square: ");

            var evenNums  = nums.Where(num => num % 2 == 0).Select(num => num * num);

            foreach (int i in evenNums)
                Console.Write($"{i}, ");
            Console.WriteLine();
        }
        static void example7()
        {
            var people = new[]
            {
                new {Name = "Alice", Age = 30},
                new {Name = "Bob", Age = 25},
                new {Name = "Charlie", Age = 35},
                new {Name = "Diana", Age = 30},
                new {Name = "Ethan", Age = 25}
            };

            var groupByAge = people.GroupBy(x => x.Age).Select(group => new {
                Age = group.Key,
                People = group.OrderBy(p => p.Name)
            });

            foreach(var group in groupByAge)
            {
                Console.WriteLine($"People in Age: {group.Age}");
                foreach (var person in group.People)
                    Console.WriteLine($"{person.Name}");
            }
        }
        static void example8()
        {
            int[] numbers = {1, 2, 3, 4, 5, 6, 7, 8, 9, 10};

            var evenNums = numbers.Where(num => num % 2 == 0).Select(num => num);

            int evenNumberSum = evenNums.Sum();

            foreach(int num in evenNums)
            {
                Console.Write($"{num}, ");
            }
            Console.WriteLine($"\n\n{evenNumberSum}");
        }
        static void example9()
        {
            var employees = new[]
            {
                new {ID = 1, Name = "Alice", DepartmentID = 2},
                new {ID = 2, Name = "Bob", DepartmentID = 1}
            };

            var departments = new[]
            {
                new {ID = 1, Name = "Human Resources"},
                new {ID = 2, Name = "Development"}
            };

            var empDetails = employees.Join(departments, 
                e => e.DepartmentID, 
                d => d.ID,
                (e, d) =>  new { ID = e.ID, Name = e.Name, Department = d.Name }
            );

            foreach (var emp in empDetails)
                Console.Write($"ID: {emp.ID}, Name: {emp.Name}, Department: {emp.Department}\n");
        }
        static void PrintExamples(int exNum, Action fun)
        {
            Console.WriteLine($"example {exNum}");
            Console.WriteLine($"-------------------------------------------------------");
            fun();
            Console.WriteLine($"=======================================================");
        }
        static void Main(string[] args)
        {
            PrintExamples(1, example1);
            PrintExamples(2, example2);
            PrintExamples(3, example3);
            PrintExamples(4, example4);
            PrintExamples(5, example5);
            PrintExamples(6, example6);
            PrintExamples(7, example7);
            PrintExamples(8, example8);
            PrintExamples(9, example9);

        }
    }
}
