namespace IComparableInCustomClasses
{
    public class ClsPerson : IComparable
    {
        public required string Name { get; set; }
        public byte Age { get; set; }
        public int CompareTo(object? obj)
        {
            if (obj == null) 
                return 1;
            if (obj is ClsPerson other)
                return Age.CompareTo(other.Age);
            return 1;
        }
        static public void PrintList(IEnumerable<ClsPerson> Pepole)
        {
            foreach (ClsPerson p in Pepole)
                Console.WriteLine(p);
        }
        static public void PrintList(string msg, IEnumerable<ClsPerson> Pepole)
        {
            Console.WriteLine(msg);
            foreach (ClsPerson p in Pepole)
                Console.WriteLine(p);
        }
        public override string ToString()
        {
            return $"Name: {Name}, Age: {Age}";
        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            List<ClsPerson> people = new()
            {
                new ClsPerson {Name = "John", Age = 30},
                new ClsPerson {Name = "Jane", Age = 25},
                new ClsPerson {Name = "Doe", Age = 28},
            };

            ClsPerson.PrintList("People Before sort:", people);
            people.Sort();
            ClsPerson.PrintList("\nPeople After sort:", people);
        }
    }
}
