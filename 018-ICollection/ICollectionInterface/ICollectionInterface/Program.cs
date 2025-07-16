using System.Collections;
using static ICollectionInterface.Program;

namespace ICollectionInterface
{
    internal class Program
    {
        public class ComplexCollection<T> : ICollection<T>
        {
            T[] array = new T[1];

            int _count = 0;
            int _capacity = 1;

            void _createNewArray()
            {
                if (Count + 1 < _capacity)
                    return;
                _capacity *= 2;
                T[] array2 = new T[_capacity];
                for (int i = 0; i < _count; i++)
                {
                    array2[i] = array[i];
                }
                array = array2;
            }

            public int Count => _count;

            public bool IsReadOnly => false;

            public void Add(T item)
            {
                _createNewArray();
                array[_count] = item;
                _count++;
            }

            public void Clear()
            {
                array = new T[1];
                _capacity = 1;
                _count = 0;
            }

            public bool Contains(T item)
            {
                return array.Contains(item);
            }

            public void CopyTo(T[] array, int arrayIndex)
            {
                for(int i = 0; i < array.Length; i++)
                    array[i + arrayIndex] = this.array[i];
            }

            public IEnumerator<T> GetEnumerator()
            {
                for(int i = 0; i < _count;i++)
                    yield return array[i];
            }

            public bool Remove(T item)
            {
                if (item == null)
                    return false;
                for(int i = 0; i < _count;i++)
                {
                    if (item.Equals(array[i]))
                    {
                        for(; i + 1 < _count; i++)
                        {
                            array[i] = array[i + 1];
                        }
                        _count--;
                        return true;
                    }
                }
                return false;
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }
            static public void PrintItems(string msg, ICollection<T> collection)
            {
                Console.WriteLine(msg);
                foreach (var item in collection)
                    Console.WriteLine(item);
            }
        }

        public class SimpleCollection<T> : ICollection<T>
        {
            List<T> items = new();

            public int Count => items.Count;

            public bool IsReadOnly => false;

            public void Add(T item)
            {
                items.Add(item);
            }

            public void Clear()
            {
                items.Clear();
            }

            public bool Contains(T item)
            {
                return items.Contains(item);
            }

            public void CopyTo(T[] array, int arrayIndex)
            {
                for (int i = 0; i < items.Count; i++)
                    array[arrayIndex + i] = items[i];
            }

            public IEnumerator<T> GetEnumerator()
            {
                for (int i = 0; i < items.Count; ++i)
                    yield return items[i];
            }

            public bool Remove(T item)
            {
                return items.Remove(item);
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }
        }

        
        static void Main(string[] args)
        {
            ComplexCollection<string> cart = new();

            cart.Add("Apple");
            cart.Add("Banana");
            cart.Add("Carrot");

            ComplexCollection<string>.PrintItems("ComplexCollection items: ", cart);
            Console.WriteLine($"\nComplexCollection items count: {cart.Count}");
            cart.Remove("Banana");
            ComplexCollection<string>.PrintItems("\nComplexCollection items: ", cart);
            Console.WriteLine($"\nComplexCollection items count: {cart.Count}");
        }
    }
}
