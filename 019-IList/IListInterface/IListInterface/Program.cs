using System;
using System.Collections;
using System.Reflection;

namespace IListInterface
{
    public class myList<T> : IList<T>
    {
        List<T> list = new();
        public T this[int index] { get => list[index]; set => list[index] = value; }

        public int Count => list.Count;

        public bool IsReadOnly => false;

        public void Add(T item)
        {
            list.Add(item);
        }

        public void Clear()
        {
            list.Clear();
        }

        public bool Contains(T item)
        {
            return list.Contains(item);
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            list.CopyTo(array, arrayIndex);
        }

        public IEnumerator<T> GetEnumerator()
        {
            return list.GetEnumerator();
        }

        public int IndexOf(T item)
        {
            return list.IndexOf(item);
        }

        public void Insert(int index, T item)
        {
            list.Insert(index, item);
        }

        public bool Remove(T item)
        {
            return list.Remove(item);
        }

        public void RemoveAt(int index)
        {
            list.RemoveAt(index);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
    internal class Program
    {

        static void Main(string[] args)
        {
            myList<int> nums = [1, 2, 3, 4, 9, 9, 10, 90, 4, 54];

            
            foreach (int x in nums) 
                Console.Write($"{x}, ");
            Console.WriteLine("");
            nums.RemoveAt(4);
            Console.Write($"{nums[0]}");
            for (int x = 1; x < nums.Count; x++ )
            {
                Console.Write($", {nums[x]}");
            }
        }
    }
}
