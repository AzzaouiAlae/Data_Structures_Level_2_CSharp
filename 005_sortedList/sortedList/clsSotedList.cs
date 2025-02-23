using System.Data.SqlTypes;

class clsSortedList
{
    public static void start()
    {
        var sortedList = new SortedList<string, int>();

        sortedList.Add("banana", 2);
        sortedList.Add("Orange",3);
        sortedList.Add("apple",3);

        Console.WriteLine("\nQuantity of apples: " + sortedList["apple"]);

        Console.WriteLine("\nIterating SortedList Elements:");
        foreach (var item in sortedList)
        {
            Console.WriteLine($"Key: {item.Key}\tvalue: {item.Value}");
        }
        sortedList.Remove("apple");

        Console.WriteLine("\nAfter remmoving item:");
        foreach(var item in sortedList)
            Console.WriteLine($"key: {item.Key}\tvalue: {item.Value}");
    }
    public static void LINQ()
    {
        SortedList<int, string> sorted_List = new()
        {
            {1, "One"},
            {3, "Three"},
            {2, "Two"},
            {4, "Four"}
        };

        var queryExpressionSyntax = from kvp in sorted_List where kvp.Key > 1 select kvp;
        Console.WriteLine("Query Expression sytax results:");
        foreach(var item in queryExpressionSyntax)
            Console.WriteLine($"{item.Key}: {item.Value}");
        
        var methodSyntax = sorted_List.Where(kvp => kvp.Key > 1);
        Console.WriteLine("\nMethod Syntax Results: ");
        foreach(var item in methodSyntax)
            Console.WriteLine($"{item.Key}: {item.Value}");
        
        int specificValue = 2;
        var filterdByKey = sorted_List.Where(kvp => kvp.Key > specificValue);
        Console.WriteLine($"\nEntries with keys greater than {specificValue}:");
        foreach(var item in filterdByKey)
            Console.WriteLine($"{item.Key}: {item.Value}");
    }
    public static void GroupBy()
    {
        SortedList<int, string> sortedList = new()
        {
            {1, "Apple"},
            {2, "Banana"},
            {3, "Chery"},
            {4, "Date"},
            {5, "Grape"},
            {6, "Fig"},
            {7, "Elderberry"}
        };

        Console.WriteLine("Grouping by the length of the value:");
        var groups = sortedList.GroupBy(kvp => kvp.Value.Length);
        foreach(var group in groups)
        Console.WriteLine($"Length {group.Key}: {string.Join(", ", group.Select(kvp => kvp.Value))}");

    }
    public static void advace_complex_object_ops()
    {
        SortedList<int, Employee> emp = new()
        {
            {1, new Employee("Alice", "HR", 50000 )},
            {2, new Employee("Bob", "IT", 70000)},
            {3, new Employee("Charlie", "HR", 52000)},
            {4, new Employee("Daisy", "IT", 80000)},
            {5, new Employee("Ethan", "Marketing", 45000)}
        };

        var query = emp
            .Where(e => e.Value.Department == "IT")
            .OrderBy(e => e.Value.Salary)
            .Select(e => e.Value.Name);

        Console.WriteLine("IT Department Employees sorted y salary (Descending):");
        foreach(var e in query)
            Console.WriteLine(e);
    }
}

public class Employee
{
    public string Name {get; set;}
    public string Department {get; set;}
    public decimal Salary {get; set;}
    public Employee(string name, string department, decimal salary)
    {
        Name = name;
        Department = department;
        Salary = salary;
    } 
}