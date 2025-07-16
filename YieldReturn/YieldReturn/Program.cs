namespace YieldReturn
{
    internal class Program
    {
        static int TestYield()
        {
            for (int i = 0; i < 10; i++)
            {
                yield return i;
            }
            yield return -1;
        }
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
        }
    }
}
