using System.Collections;

namespace PriorityQueueDS
{
    public class PriorityQueueNode<T>(int priority, T value)
    {
        public int Priority { get; set; } = priority;
        public T Value { get; set; } = value;
    }

    public class PriorityQueue<T> : MinHeap<PriorityQueueNode<T>>
    {
        public PriorityQueue() : base((e1, e2) => e1.Priority < e2.Priority)
        {

        }
        public T Dequeue()
        {
            var item = Extract();
            
            return item.Value;
        }
        public void Enqueue(T value, int priority)
        {
            Insert(new(priority, value));
        }
    }
    public class MinHeap<T>(Func<T, T, bool> predicate)
    {
        readonly protected List<T> _heap = [];

        readonly Func<T, T,bool> _predicate = predicate;
        public int Count
        {
            get { return _heap.Count; }
        }
        protected void Insert(T item)
        {
            _heap.Add(item);

            HeapIfyUp(Count - 1);
        }
        void HeapIfyUp(int index)
        {
            int parentIndex;

            while (true)
            {
                parentIndex = (index - 1) / 2;
                if (_predicate(_heap[index], _heap[parentIndex]))
                    (_heap[index], _heap[parentIndex]) = (_heap[parentIndex], _heap[index]);
                else
                    break;
                index = parentIndex;
            }
        }
        void HeapIfyDown(int index)
        {
            int leftChild, rightChild, targetIndex;

            while (true)
            {
                targetIndex = index;
                leftChild = index * 2 + 1;
                rightChild = leftChild + 1;

                if (leftChild < Count &&
                    _predicate(_heap[leftChild], _heap[targetIndex]))
                    targetIndex = leftChild;
                if (rightChild < Count &&
                    _predicate(_heap[rightChild], _heap[targetIndex]))
                    targetIndex = rightChild;

                if (targetIndex == index)
                    break;
                (_heap[targetIndex], _heap[index]) = (_heap[index], _heap[targetIndex]);
            }
        }
        protected T Extract()
        {
            T item;

            item = _heap[0];
            (_heap[0], _heap[Count - 1]) = (_heap[Count - 1], _heap[0]);
            _heap.RemoveAt(Count - 1);
            HeapIfyDown(0);
            return item;
        }
    }
    internal class Program
    {
        static void Main()
        {
            PriorityQueue<string> queue = new();

            queue.Enqueue("a test for queue", 3);
            queue.Enqueue("this", 1);
            queue.Enqueue("Hello",0);
            queue.Enqueue("is",2);

            Console.Write($"{queue.Dequeue()}");
            while (queue.Count > 0)
            {
                Console.Write($" {queue.Dequeue()}");
            }
        }
    }
}
