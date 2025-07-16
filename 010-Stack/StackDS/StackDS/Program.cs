namespace StackDS
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Stack<int> nums = new();

            AddNumbers(nums);
            
            Console.WriteLine($"The first number in the stack is: {nums.Peek()}");

            Console.WriteLine($"First element {nums.Pop()}");
            Console.WriteLine($"Second element {nums.Pop()}");

            if (nums.Count == 0)
                Console.WriteLine($"Stack is empty");
            else
                Console.WriteLine($"The top element is: {nums.Peek()}");
            
            nums.Clear();
        }

        static void AddNumbers(Stack<int> nums)
        {
            nums.Push(10);
            nums.Push(30);
            nums.Push(50);
            nums.Push(60);
            nums.Push(70);
            nums.Push(80);
        }
    }
}
