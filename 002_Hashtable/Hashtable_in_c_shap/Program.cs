using System.Collections;

internal class Program
{
    private static void Main(string[] args)
    {
        Console.Clear();
        Hashtable myHashtable = new();

        myHashtable.Add("key1", "value1");
        myHashtable.Add("key2", "value2");
        myHashtable.Add("key3", "value3");
        myHashtable.Add(1, "value4");

        Console.WriteLine($"Accessing key1: {myHashtable["key1"]}\n");

        myHashtable["key1"] = "newValue1";

        myHashtable.Remove("key2");

        foreach (DictionaryEntry entry in myHashtable)
            Console.WriteLine($"key={entry.Key} value={entry.Value}");
    }
}