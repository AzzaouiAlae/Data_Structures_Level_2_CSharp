using System;
using System.Net;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security.Cryptography;

namespace MinHeapDS
{
    public class Heap<T>(Func<T, T, bool> isGreater)
    {
        readonly List<T> _heap = [];

        readonly Func<T, T, bool> _isGreater = isGreater;

        public int Count
        {
            get { return _heap.Count; }
        }
        public void Insert(T item)
        {
            _heap.Add(item);

            HeapIfyUp(_heap.Count - 1);
        }
        void HeapIfyUp(int index)
        {
            int parentIndex;

            while(true)
            {
                parentIndex = (index - 1) / 2;
                if (_isGreater(_heap[index], _heap[parentIndex]))
                    (_heap[index], _heap[parentIndex]) = (_heap[parentIndex], _heap[index]);
                else
                    break;
                index = parentIndex;
            }
        }
        void HeapIfyDown()
        {
            int leftChild, rightChild, smallestIndex, index = 0;

            while (index < _heap.Count - 1)
            {
                leftChild = 2 * index + 1;
                rightChild = 2 * index + 2;
                smallestIndex = index;

                if (leftChild < _heap.Count &&
                    !_isGreater(_heap[smallestIndex], _heap[leftChild]))
                    smallestIndex = leftChild;
                if (rightChild < _heap.Count && 
                    !_isGreater(_heap[smallestIndex], _heap[rightChild]))
                    smallestIndex = rightChild;

                if (index.Equals(smallestIndex))
                    break;

                (_heap[smallestIndex], _heap[index]) = (_heap[index], _heap[smallestIndex]);
                index = smallestIndex;
            }
        }
        public void PrintHeap()
        {
            foreach(T item in _heap)
                Console.WriteLine($"{item} ");
        }
        public T Peek()
        {
            if (Count == 0)
                throw new InvalidOperationException("Heap is empty");
            return _heap[0];
        }
        public T Extract()
        {
            T item;

            if (Count == 0)
                throw new InvalidOperationException("Heap is empty");
            item = _heap[0];
            _heap[0] = _heap[Count - 1];
            _heap.RemoveAt(Count - 1);
            HeapIfyDown();
            return item;
        }
    }
    internal class Program
    {
        static void Main()
        {
            Func<int, int, bool> isGreater = (n1, n2) => n1 > n2;
            Func<int, int, bool> isLess = (n1, n2) => n1 < n2;
            Heap<int> minHeap = new(isLess);

            minHeap.Insert(1);
            minHeap.Insert(50);
            minHeap.Insert(10);
            minHeap.Insert(52);
            minHeap.Insert(51);
            minHeap.Insert(11);
            minHeap.Insert(12);


            Console.WriteLine("minHeap items: ");
            Console.Write(minHeap.Extract());
            while (0 < minHeap.Count)
                Console.Write($", {minHeap.Extract()}");
            Console.WriteLine();

            
        }
    }
}
