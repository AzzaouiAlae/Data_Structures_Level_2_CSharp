using System.Dynamic;
using System.Runtime.Intrinsics.X86;
using System.Security.Cryptography;

namespace sortedSet;

class sorted_Set()
{
    public static void start()
    {
            SortedSet<int> sSet = new();

            sSet.Add(5);
            sSet.Add(2);
            sSet.Add(8);
            sSet.Add(3);

        Console.Write("SortedSet elements: ");
        Console.WriteLine(string.Join(", ", sSet));

        int target = 3;
        if(sSet.Contains(target))
            Console.WriteLine($"\nNumber {target} exists in the sortedSet.");

        int toRemove = 8;
        sSet.Remove(toRemove);

        Console.WriteLine("\nAfter removing 8: " + string.Join(", ", sSet));
    }
    public static void useLINQ()
    {
        SortedSet<int> sortedSet = [1, 2, 3, 4, 5];

        var filteredSet = sortedSet.Where(x => x > 2);
        Console.WriteLine("Filtered set:\t\t\t" + string.Join(", ", sortedSet));

        var sum = sortedSet.Sum();
        Console.WriteLine("sum of sortedSet Numbers:\t" + sum);

        var mini = sortedSet.Min();
        Console.WriteLine("mini sortedSet value:\t\t" + mini);

        var max = sortedSet.Max();
        Console.WriteLine("Max SortedSet value:\t\t" + max);

        var descedingSet = sortedSet.OrderByDescending(x => x);
        Console.WriteLine("descending Set:\t\t\t" + string.Join(", ", descedingSet));
    }
    public static void find()
    {
       SortedSet<int> nums = [1, 2, 3, 4, 5, 6, 7, 8, 9, 10];

       var evenNums = nums.Where(x => x % 2 == 0).Select(x => x*x);
       Console.WriteLine("even nums: " + string.Join(", ", evenNums));
    }
    public static void union()
    {
        SortedSet<int> set1 = [1, 2, 3, 4, 5];
        SortedSet<int> set2 = [3, 4, 5, 6, 7];

        set1.UnionWith(set2);
        Console.WriteLine("union of set1 and set2: " + string.Join(", ", set1));
    }
    public static void Intersection()
    {
        SortedSet<int> set1 = [1, 2, 3, 4, 5];
        SortedSet<int> set2 = [3, 4, 5, 6, 7];

        set1.IntersectWith(set2);
        Console.WriteLine("Itersection of set1 and set2: " + string.Join(", ", set1));
    }
    public static void Difference()
    {
        SortedSet<int> set1 = [1, 2, 3, 4, 5, 6];
        SortedSet<int> set2 = [4, 5, 6, 7, 8];

        set1.ExceptWith(set2);
        Console.WriteLine("Except set1 with set2: " + string.Join(", ", set1));
    }
    static public void SuperSet()
    {
        SortedSet<int> set1 = [1, 2, 3, 4, 5];
        SortedSet<int> set2 = [1, 2];

        if(set1.IsSupersetOf(set2))
            Console.WriteLine("set1 is superset of set2");
        else 
            Console.WriteLine("set1 is not superset of set2");
    }
    static public void subSetOf()
    {
        SortedSet<int> set1 = [1, 2, 3, 4, 5, 6];
        SortedSet<int> set2 = [3,4,5];

        if(set2.IsSubsetOf(set1))
            Console.WriteLine("set2 is subset of set1");
        else
            Console.WriteLine("Set2 is not subset of set1");
    }
    static public void equality()
    {
        SortedSet<int> set1 = [1, 2, 3, 4, 5];
        SortedSet<int> set2 = [3,4,5,6,7,8];

        Console.WriteLine("Are set1 equal to set2?: " + set1.SetEquals(set2));
    }

}