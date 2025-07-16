using System.Collections;

namespace IEnumerableInterface
{
    internal class Program
    {
        public class CustomCollection<T> : IEnumerable<T>
        {
            List<T> Items = new();
            public IEnumerator<T> GetEnumerator()
            {
                for(int i = 0; i < Items.Count; i++)
                    yield return Items[i];
            }
            IEnumerator IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }
            public void Add(T item)
            {
                Items.Add(item);
            }
        }
        static void Main(string[] args)
        {
            CustomCollection<int> myCollection = new() { 1, 2, 3, 5, 8};

            foreach(int i in myCollection)
                Console.WriteLine(i);
        }
    }
}
