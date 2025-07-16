namespace TuplesDS
{
    internal class Program
    {
        static void example1()
        {
            (int, string, double) person = (1, "Alice", 5.5);

            Console.WriteLine($"ID: {person.Item1}");
            Console.WriteLine($"Name: {person.Item2}");
            Console.WriteLine($"Height: {person.Item3} feet");

            var number = GetValue();
            Console.WriteLine($"\nItem1: {number.Item1}");
            Console.WriteLine($"Item2: {number.Item2}");
        }

        static void PrintPeople(List<(int ID, string Name, byte Age)>? people)
        {
            if (people == null) return;
            foreach(var person in people)
            {
                Console.WriteLine($"ID: {person.ID}, Name: {person.Name}, Age: {person.Age}");
            }
        }
        static void example2()
        {
            Console.WriteLine("\nList of people: ");
            List<(int ID, string Name, byte Age)> people = new()
            {
                (1, "Alice", 28),
                (2, "Bob", 25),
                (3, "Charlie", 35),
                (4, "Diana", 28),
                (5, "alae", 30),
            };
            PrintPeople(people);
            Console.WriteLine("\nFilter people equal or above 30: ");
            var peopleAbove30 = people.Where(p => p.Age > 29).Select(p => p).ToList();
            PrintPeople(peopleAbove30);

            var avregeAge = people.Average(p => p.Age);
            Console.WriteLine($"\nThe avrege Age is: {avregeAge}");
        }
        static void Main(string[] args)
        {
            example1();
            example2();

        }

        static (int, string) GetValue()
        {
            return (20, "Twenty");
        }
    }
}
