using System.Security.Cryptography;

namespace LinkedListDS
{
    internal class Program
    {
        static void AddNumsToList(LinkedList<int> nums, int[] ints)
        {
            foreach (int i in ints)
            {
                nums.AddLast(i);
            }
        }

        static void PrintNumList(string msg ,LinkedList<int> nums)
        {
            Console.WriteLine(msg);
            foreach(int i in nums)
                Console.Write(i + ", ");
            Console.WriteLine();
        }
        static void IsItemExists(LinkedList<int> nums, int num)
        {
            if (nums.Contains(num))
                Console.WriteLine($"The number {num} is in the list");
            else
                Console.WriteLine($"The number {num} is not in the list");
        }
        static void Main(string[] args)
        {
            LinkedList<int> nums = new();

            AddNumsToList(nums, [1, 2, 3]);
            PrintNumList("Linked List: ",nums);
            nums.Remove(1);
            IsItemExists(nums, 2);
        }
    }
}
