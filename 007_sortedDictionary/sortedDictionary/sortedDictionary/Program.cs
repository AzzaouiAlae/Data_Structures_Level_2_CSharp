namespace sortedDictionary
{
    internal class Program
    {
        static void InitDict(SortedDictionary<string, int> dict)
        {
            dict.Add("Banana", 2);
            dict.Add("cheary", 5);
            dict.Add("Appel", 1);
        }
        static void PrintDictKVP(SortedDictionary<string, int> dict)
        {
            Console.WriteLine("Sorted Dictionary content:");
            foreach(var kvp in dict)         
                Console.WriteLine($"key={kvp.Key}, value={kvp.Value}");            
        }
        static void PrintKeyValue(SortedDictionary<string, int> dict, string key)
        {
            if (dict.ContainsKey(key))
                Console.WriteLine($"key={key}, value={dict[key]}");
            else
                Console.WriteLine($"key: {key} not found");
        }
        static void CheckKeyExists(SortedDictionary<string, int> dict, string key)
        {
            Console.Write($"The key: {key} is ");
            if (dict.ContainsKey(key))
                Console.WriteLine("existe");
            else
                Console.WriteLine("not existe");
        }
        static void Main(string[] args)
        {
            SortedDictionary<string, int> sortedDict = new();

            InitDict(sortedDict);
            PrintDictKVP(sortedDict);
            Console.WriteLine($"Total items: {sortedDict.Values.Sum()}");
            Console.WriteLine("\nAccess value by key:");
            
            PrintKeyValue(sortedDict, "Banana");
            PrintKeyValue(sortedDict, "Orange");
            Console.WriteLine("\nCheck key exists\n");
            CheckKeyExists(sortedDict, "Banana");
            CheckKeyExists(sortedDict, "Orange");
            sortedDict.Remove("Banana");
            Console.WriteLine("\nAfter remove key: Banana");
            PrintDictKVP(sortedDict);
            Console.WriteLine($"Total items: {sortedDict.Values.Sum()}");
        }
    }
}
