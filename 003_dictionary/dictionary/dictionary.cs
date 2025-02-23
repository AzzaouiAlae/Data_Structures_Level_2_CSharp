class working_with_dictionary
{
    static public void start()
    {
        Dictionary<string, int> fruitBasket = new();

        fruitBasket.Add("Apple", 5);
        fruitBasket.Add("Banana", 2);
        
        //fruitBasket.Add("Banana", 5); //runtime error
        fruitBasket.Add("Orange", 12);

        fruitBasket["Apple"] = 10;

        Console.WriteLine("Dictionary content:");
        foreach(KeyValuePair<string, int> item in fruitBasket)
            Console.WriteLine($"Fruit: {item.Key}\tQuantity: {item.Value}");
        
        fruitBasket.Remove("Banana");
        Console.WriteLine("\nDictionary content after removing banana:");
        foreach(KeyValuePair<string, int> item in fruitBasket)
            Console.WriteLine($"Fruit: {item.Key}\tQuantity: {item.Value}");
        Console.WriteLine(".");
    }
    static public void use_try_get_value()
    {
        Dictionary<string, int> fruitBasket = new()
        {
            {"Apple", 5},
            {"Banana", 2},
        };

        if(fruitBasket.TryGetValue("Apple", out int appleQuantity))
            Console.WriteLine($"Apple quanntity: {appleQuantity}");
        else   
            Console.WriteLine("Apple not found in the basket.");
        
        //Console.WriteLine($"Orrange quanntity: {fruitBasket["Orange"]}");

        if(fruitBasket.TryGetValue("Orange", out int OrangeQuantity))
            Console.WriteLine($"Orange quanntity: {OrangeQuantity}");
        else   
            Console.WriteLine("Orange not found in the basket.");
    }
    static public void use_LINQ()
    {
        Dictionary<string, int> fruitBasket = new()
        {
            {"Apple", 5},
            {"Banana", 2},
            {"Orange", 7},
        };

        var fruitInfo = fruitBasket.Select(kpv => new {kpv.Key, kpv.Value});
        Console.WriteLine("Transformed Items:");
        foreach(var item in fruitInfo)
            Console.WriteLine($"Fruit: {item.Key},\tQuantity: {item.Value}");

        Console.WriteLine();

        var filterFruit = fruitBasket.Where(kpv => kpv.Value > 3);
        Console.WriteLine("Filtered Items > 3:");
        foreach(var item in filterFruit)
            Console.WriteLine($"Fruit: {item.Key},\tQuantity: {item.Value}");

        Console.WriteLine();

        var sortedByQuantity = fruitBasket.OrderBy(kpv => kpv.Value);
        Console.WriteLine("Sorted Items:");
        foreach (var item in sortedByQuantity)
            Console.WriteLine($"Fruit: {item.Key},\tQuqntity: {item.Value}");  

        int totalQuantity = fruitBasket.Sum(kpv => kpv.Value);
        Console.WriteLine($"\nTotal Quantity: {totalQuantity}");
    }
    static public void use_advanced_LINQ()
    {
        Dictionary<string, string> fruitsCategory = new()
        {
            {"Apple", "Tree"},
            {"Banana", "Herb"},
            {"Cherry", "Tree"},
            {"Strawberry", "Bush"},
            {"Raspberry", "Bush"}
        };

        var groupedFruits = fruitsCategory.GroupBy(k => k.Value);
        foreach (var group in groupedFruits)
        {
            Console.WriteLine($"Category: {group.Key}");
            foreach(var fruit in group)
                Console.WriteLine($" - {fruit.Key}");
        }

        Dictionary<string, int> fruitBasket = new()
        {
            {"Apple", 5},
            {"Banana", 2},
            {"Orange", 7}
        };
        var sortedFilteredFruits = fruitBasket
            .Where(kp => kp.Value > 3)
            .OrderBy(kp => kp.Key);

        Console.WriteLine("\nSorted and Filtered Fuits:");
        foreach (var fruit in sortedFilteredFruits)
            Console.WriteLine($"  Fuit: {fruit.Key}\tQuantity: {fruit.Value}");
    }
}