using System.Data;
using System.Diagnostics.CodeAnalysis;
using System.Security.Cryptography;
public class Person
{
    public string Name { get; set; }
    public int Age { get; set; }
    public Person(string name, int age)
    {
        Name = name;
        Age = age;
    }
}
public class working_with_List
{
    //count 0
    public static void fisrt_work_with_list()
    {
        List<int> numbers = new();

        numbers.Add(1);
        numbers.Add(2);
        numbers.Add(3);
        numbers.Add(4);
        numbers.Add(5);
        Console.WriteLine($"numbers count={numbers.Count} capacity={numbers.Capacity}");
        
        Console.WriteLine(numbers[0]);
        Console.WriteLine(numbers[1]);
        Console.WriteLine(numbers[2]);  
        Console.WriteLine(numbers[3]);
        Console.WriteLine(numbers[4]);

        numbers[1] = 500;
        Console.WriteLine(numbers[1]);
    }
    public static void insert_in_List()
    {
        var numbers = new List<int>() {1, 2, 3, 4, 5, 6, 7, 8, 9, 10};
        numbers.Add(11);
        Console.WriteLine("After adding 11: " + string.Join(", ", numbers));
        numbers.Insert(0, 0);
        Console.WriteLine("After inserting 0 at 0: " + string.Join(", ", numbers));
        numbers.InsertRange(5, new List<int> {55, 56});
        Console.WriteLine("After inserting 55 and 56 into index 5: " + string.Join(", ", numbers));
    }
    public static void remove_in_List()
    {
        List<int> numbers = new List<int>() {1, 2, 3, 4, 5, 6, 7, 8, 9, 10};

        numbers.Remove(5);
        Console.WriteLine("After removing 5: " + string.Join(", ", numbers));

        numbers.RemoveAt(0);
        Console.WriteLine("After removing first element: " + string.Join(", ", numbers));

        numbers.RemoveAll(n => n % 3 == 0);
        Console.WriteLine("After removing all multiples of 3: " + string.Join(", ", numbers));

        numbers.Clear();
        Console.WriteLine("After clearing the list, count: " + numbers.Count);
    }
    public static void Looping_through_list()
    {
        List<int> numbers = new List<int>() {1, 2, 3, 4, 5};
        Console.WriteLine("Number of items in the list: " + numbers.Count);

        Console.Write("For Loop: ");
        for(int i = 0; i < numbers.Count; i++)
          Console.Write($"{numbers[i]}, ");

        Console.WriteLine();

        Console.Write("Foreach: ");
        foreach(int i in numbers)
            Console.Write($"{i}, ");

        Console.WriteLine("");
        
        Console.Write("List.ForEach: ");
        numbers.ForEach(n => Console.Write($"{n}, "));
        Console.WriteLine("");
    }
    public static void Linq_with_list()
    {
        List<int> numbers = new List<int>() {1, 2, 3, 4, 5, 6, 7, 8, 9, 10};

        Console.WriteLine("Sum: " + numbers.Sum());
        Console.WriteLine("Average: " + numbers.Average());
        Console.WriteLine("Minimum: " + numbers.Min());
        Console.WriteLine("Maximum: " + numbers.Max());
        Console.WriteLine("Count: " + numbers.Count());
    }
    public static void Filtering_data_with_linq()
    {
        List<int> numbers = new List<int>{1, 2, 3,4,5,6,7,8,9,10};

        Console.WriteLine("Even Numbers: " + string.Join(", ", numbers.Where(n => n % 2 == 0)));
        Console.WriteLine("Odd Numbers: " + string.Join(", ", numbers.Where(n => n % 2 != 0)));
        Console.WriteLine("Numbers Greater Than 5: " + string.Join(", ", numbers.Where(n => n > 5)));
        Console.WriteLine("Evey Second Number: " + string.Join(", ", numbers.Where((n, i) => i % 2 == 1)));
        Console.WriteLine("Numbers betwee 3 and 8: " + string.Join(", ", numbers.Where(n => n > 3 && n < 8)));
    }
    public static void sorting_in_List()
    {
        List<int> numbers = new List<int>() {44, 22, 55, 666, 9, -6, 345, 11, 3, 3};

        numbers.Sort();
        Console.WriteLine("Sorted in ascending order: " + string.Join(", ", numbers));

        numbers.Reverse();
        Console.WriteLine("Sorted in descending order: " + string.Join(", ", numbers));

        numbers = new List<int>() {44, 22, 55, 666, 9, -6, 345, 11, 3, 3};
        Console.WriteLine("Sorted ascending with LINQ: " + string.Join(", ", numbers.OrderBy(N => N)));
        Console.WriteLine("Sorted Descending with LINQ: " + string.Join(", ", numbers.OrderByDescending(n => n) ) );
    }
    public static void contains_exists_find_findAll_any()
    {
        List<int> numbers = new List<int>() {44, 22, -55, 666, 9, -6, 345, 11, 3, 3};
        
        Console.WriteLine("List contains 9: " + numbers.Contains(9));

        Console.WriteLine("List contains negative numbers: " + numbers.Exists(n => n < 0));

        Console.WriteLine("First negative number: " + numbers.Find(n => n < 0));

        Console.WriteLine("All negative numbers: " + string.Join(", ", numbers.FindAll(n => n < 0)));

        Console.WriteLine("Any numbers greater than 100: " + numbers.Any(n => n > 100));
    }
    public static void contains_exists_find_findAll_any_string()
    {
        List<string> words = new List<string>(){"apple", "banana", "cherry", "date", "elderberry", "fig", "grape", "honeydew"};
    
        Console.WriteLine("List contains 'apple': " + words.Contains("apple"));
        Console.WriteLine("List contains a word of length 5: " + words.Exists(word => word.Length == 5));
        Console.WriteLine("Find word longer than 5 characters: " + words.Find(word => word.Length > 5));
        Console.WriteLine("Words longer than 5 characters: " + string.Join(", ", words.FindAll(word => word.Length > 5)));
        Console.WriteLine("Any words starting with 'a': " + words.Any(word => word.StartsWith("a")));
        Console.WriteLine("Any words Ends with 'a': " + words.Any(word => word.EndsWith("a")));
    }
    public static void custom_objects()
    {
        List<Person> people = new List<Person>
        {
            new Person("Alice", 30),
            new Person("Bob", 25),
            new Person("Charlie", 35),
            new Person("David", 40),
            new Person("Eve", 29),
            new Person("Frank", 31),
            new Person("Grace", 24),
            new Person("Helen", 27)
        };

        Console.WriteLine("Curent state of the people list:");
        foreach (Person person in people)
        {
            Console.WriteLine($"Name: {person.Name},\tAge: {person.Age}");
        }

        Person? foundPerson = people.Find(p => p.Name == "David");
        if(foundPerson != null)
            Console.WriteLine($"\nFound Person: Name: {foundPerson.Name}\tAge: {foundPerson.Age}");
        
        
        Person? searchResult = people.FirstOrDefault(p => p.Name ==  "Alice");
        if(searchResult != null)
        {
            searchResult.Age = 31;
            Console.WriteLine($"\nUpdated {searchResult.Name}'s age to {searchResult.Age}");
        }
        List<Person> peopleOver30 = people.FindAll(p => p.Age > 30);
        Console.WriteLine("\nPeople over 30: ");
        peopleOver30.ForEach(p => Console.WriteLine($"Name: {p.Name},\tAge: {p.Age}"));

        bool containsAlice = people.Any(p => p.Name == "Alice");
        Console.WriteLine($"\nList contains a person named 'Alice': {containsAlice}");

        people.RemoveAll(p => p.Age < 30);
        Console.WriteLine("\nRemoved people under the age of 30.");

        Console.WriteLine("\nCurrent state of the people list:");
        people.ForEach(p => Console.WriteLine($"Name: {p.Name},\tAge: {p.Age}"));
    }
    public static void to_array()
    {
        List<int> numbers = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10};
        int[] numbersArray = numbers.ToArray();
        Console.WriteLine("Array elements: " + string.Join(", ", numbersArray));
    }
    public static void array_to_list()
    {
        int []arr = {1, 2, 3, 4, 5, 6, 7, 8, 9, 10};

        List<int> list = new List<int>(arr);
        list.ForEach(n => Console.Write($"{n}, "));
        Console.WriteLine();
    }
}

