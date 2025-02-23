using System.Runtime.Intrinsics.X86;
using System.Security.Cryptography;

class clsHashset
{
    static public void start()
    {
        HashSet<string> fruits = new();

        fruits.Add("Apple");
        fruits.Add("Banana");
        fruits.Add("Cherry");

        fruits.Add("Apple");
        fruits.Add("Apple");
        fruits.Add("Apple");

        foreach (string fruit in fruits)
        {
            Console.WriteLine(fruit);
        }
    }
    static public void checking_existence()
    {
        HashSet<string> fruits = new()
        {
            "Apple", "Banana", "Cherry"
        };

        if(fruits.Contains("Apple"))
            Console.WriteLine("Apple is in the hashSet");
        else
            Console.WriteLine("Apple is not in the HashSet");

        if(fruits.Contains("Orange"))
            Console.WriteLine("Orange is in the HashSet");
        else
            Console.WriteLine("Orange is not in the HashSet");
    }
    static public void Removing_element()
    {
        HashSet<string> fruits = new HashSet<string> {"Apple", "Banana", "Cherry"};
        Console.WriteLine("Hashset Item count = " + fruits.Count);

        fruits.Remove("Banana");

        Console.WriteLine("\nHashset Item count after removing banana = " + fruits.Count);
        foreach (string fruit in fruits)
            Console.WriteLine(fruit);
        
        fruits.Clear();
        Console.WriteLine("\nHashst Item count after clear = " + fruits.Count);
    }
    static public void Remove_duplicates_from_arr()
    {
        int []arr = {1, 2, 2, 3, 4, 4, 5};

        HashSet<int> uniqueNumbers = [.. arr];

        foreach (int i in uniqueNumbers)
            Console.Write($"{i}, ");
        Console.WriteLine();
    }
    static public void Using_LINQ()
    {
        HashSet<int> numbers =  [1, 2, 3, 4, 5, 6, 7, 8, 9, 10];

        var evenNumers = numbers.Where(n => n % 2 == 0);

        Console.WriteLine("Even Numbers:");
        
        foreach (int i in evenNumers)
            Console.WriteLine($"{i}, ");
        
        var numbersGreaterThanFive = numbers.Where(n => n > 5);

        Console.WriteLine("\nNumbers Greater than 5:");
        foreach(int i in numbersGreaterThanFive)
            Console.WriteLine($"{i}, ");
    }
    static public void Using_LINQ2()
    {
        HashSet<string> names = new()
        {"Alice", "Bob", "Charlie", "Daisy", "Ethan", "Fiona"};

        var namesStrtingWithC = names.Where(n => n.StartsWith("C"));
        Console.WriteLine("Names start with c");
        foreach(string name in namesStrtingWithC)
            Console.WriteLine("  " + name);
        
        var namesLogerThanFour = names.Where(n => n.Length > 4);
        Console.WriteLine("\nnames Loger than four");
        foreach(var name in namesLogerThanFour)
            Console.WriteLine("  " + name);
    }
    static public void union_ops()
    {
        HashSet<int> set1 = [1, 2, 3];
        HashSet<int> set2 = [3, 4, 5];

        set1.UnionWith(set2);
        Console.WriteLine("union of sets:");
        foreach (int i in set1)
            Console.Write($"{i}, ");
        Console.WriteLine();
    }
    static public void intersection_ops()
    {
        HashSet<int> set1 = new HashSet<int> {1, 2, 3};
        HashSet<int> set2 = new HashSet<int> {3, 4, 5};

        set1.IntersectWith(set2);
        Console.WriteLine("Intersection of sets:");
        foreach(int item in set1)
            Console.Write($"{item}, ");
        Console.WriteLine();
    }
    static public void Difference_ops()
    {
        HashSet<int> set1 = new HashSet<int> {1, 2, 3};
        HashSet<int> set2 = new HashSet<int> {3, 4, 5};

        set1.ExceptWith(set2);

        Console.WriteLine("Difference of sets (set1 - set2)");
        foreach(int item in set1)
            Console.Write($"{item}, ");
        Console.WriteLine();
    }
    static public void symmetric_difference_ops()
    {
        HashSet<int> set1 = new HashSet<int> {1, 2, 3};
        HashSet<int> set2 = new HashSet<int> {3, 4, 5};

        set1.SymmetricExceptWith(set2);

        Console.WriteLine("Symmetric difference of sets:");
        foreach (int item in set1)
            Console.WriteLine(item);
        Console.WriteLine();
    }
    static public void using_setEquals()
    {
        HashSet<int> set1 = [1, 2, 3];
        HashSet<int> set2 = [1, 2, 3];
        HashSet<int> set3 = [3, 4, 5];

        Console.WriteLine("set1 equals set2: " + set1.SetEquals(set2));
        Console.WriteLine("set1 equals set3: " + set1.SetEquals(set3));
    }
    static public void using_isSubSetof()
    {
        HashSet<int> set1 = [1, 2];
        HashSet<int> set2 = [1, 2, 3, 4, 5];

        Console.WriteLine("set1 is a subset of set2: " + set1.IsSubsetOf(set2));
        Console.WriteLine("set2 is a subset of set1: " + set2.IsSubsetOf(set1));
    }
    static public void using_Is_SuperSetOf()
    {
        HashSet<int> set1 = [1, 2, 3, 4, 5];
        HashSet<int> set2 = [1, 3];

        Console.WriteLine("set1 is a superset of set2: " + set1.IsSupersetOf(set2));
    }
    static public void usig_isoverlaps()
    {
        HashSet<int> set1 = [1, 2, 3];
        HashSet<int> set2 = [3, 4, 5];
        HashSet<int> set3 = [6, 7, 8];

        Console.WriteLine("set1 overlaps set2: " + set1.Overlaps(set2));
        Console.WriteLine("set1 overlaps set3: " + set1.Overlaps(set3));
    }
}