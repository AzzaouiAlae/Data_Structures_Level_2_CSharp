using System.Collections.Generic;

namespace QueueDS
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Queue<string> strings = new();

            AddElement(strings);

            Console.WriteLine($"First in line is: {strings.Peek()}");

            for (int i = 0; i < strings.Count && i < 2; i++) 
                Console.WriteLine($"Served: {strings.Dequeue()}");
          

            PrintElement(strings);
            strings.Clear();
        }
        static void PrintElement(Queue<string> strings)
        {
            foreach (string s in strings)
                Console.WriteLine(s);
        }
        static void AddElement(Queue<string> strings)
        {
            strings.Enqueue("Alice");
            strings.Enqueue("Bob");
            strings.Enqueue("Charlie");
        }

        
    }
}
