using System.Collections;

namespace ArrayListDS;

class Program
{
    static void ArrayListCountingOccurrences()
    {
        ArrayList list = new() { 1, 2, 8, 9, 2, 65, 5, 4, 5, 2 };

        int target = 2;

        int count = list.Cast<int>().Count(n => n == target);
        Console.WriteLine($"The number {target} is found in {count} place");
    }
    static void ArrayListWithLinq()
    {
        ArrayList list = new() { 1, 2, 3, 4, 5, 6, 7, 8, 9 };

        var evenNums = list.Cast<int>().Where(n => n % 2 == 0);
        Console.WriteLine("Even numbers:");
        foreach (int num in evenNums)
            Console.WriteLine($"{num}");
    }
    static void ArrayListAggregateFunc()
    {
        ArrayList list = new() { 1, 2, 87, 4, 45, 6, 7, 21, 9 };

        int minVal = list.Cast<int>().Min();
        int maxVal = list.Cast<int>().Max();
        double avg = list.Cast<int>().Average();
        int sum = list.Cast<int>().Sum();
        int count = list.Cast<int>().Count();

        foreach (int num in list)
            Console.Write($"{num}, ");
        Console.WriteLine();
        Console.WriteLine($"minVal: {minVal}");
        Console.WriteLine($"maxVal: {maxVal}");
        Console.WriteLine($"Average: {avg}");
        Console.WriteLine($"Sum: {sum}");
        Console.WriteLine($"count: {count}");

        list.Sort();
        Console.WriteLine($"Items after sort:");
        foreach (int num in list)
            Console.Write($"{num}, ");
        Console.WriteLine();
    }
    static void UserArrayList()
    {
        ArrayList arrList = new() { 10, 15, 60, "hello" };

        Console.WriteLine($"{arrList.Count}");
    }
    static void AddElemet(ArrayList arrList)
    {
        arrList.Add(10);
        arrList.Add(15);
        arrList.Add("hello");
    }
    static void AddDifferentElement(ArrayList arrList)
    {
        arrList.Add(10);
        arrList.Add("Hello");
        arrList.Add(true);
    }
    static void PrintArrayList(string msg, ArrayList arrList)
    {
        Console.WriteLine(msg);
        foreach (object obj in arrList)
        {
            Console.WriteLine($"{obj}");
            // if (obj is int num)
            //     Console.WriteLine($"{num}");
            // else if (obj is string str)
            //     Console.WriteLine($"{str}");
        }
    }
    static void PrintArrayListWithIndex(string msg, ArrayList arrList)
    {
        Console.WriteLine(msg);
        for (int i = 0; i < arrList.Count; i++)
        {
            Console.WriteLine($"Index: {i}, {arrList[i]}");
            // if (obj is int num)
            //     Console.WriteLine($"{num}");
            // else if (obj is string str)
            //     Console.WriteLine($"{str}");
        }
    }
    static void Main(string[] args)
    {
        // // UserArrayList();
        // ArrayList arrList = new();
        // ArrayList arrList2 = new();

        // AddElemet(arrList);
        Console.Clear();
        // PrintArrayList("Element in ArrayList:", arrList);
        // arrList.Remove(15);
        // PrintArrayList("\nElement after remove 15:", arrList);
        // PrintArrayListWithIndex("Content of ArrayList", arrList);
        // AddDifferentElement(arrList2);
        // PrintArrayList("\nPrint Different Elements:", arrList2);
        // ArrayListWithLinq();
        //ArrayListAggregateFunc();
        ArrayListCountingOccurrences();
    }
}
