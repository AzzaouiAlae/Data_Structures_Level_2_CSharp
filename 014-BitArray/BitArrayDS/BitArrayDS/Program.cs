using System.Collections;
using System.Security.Cryptography;
public class Program
{
    static string BitArrayToString(BitArray bits)
    {
        char[] chars = new char[bits.Length];
        for (int i = 0; i < bits.Length; i++) 
            chars[i] = bits[i] ? '1' : '0';
        return new string(chars);
    }
    static void PrintBitArray(BitArray bits)
    {
        for(int i = 0; i < bits.Length; ++i)        
            Console.WriteLine($"Item {i} is {bits[i]}");
    }
    static void example1()
    {
        BitArray bits = new(8);
        Console.WriteLine("bits1 content: {0}", BitArrayToString(bits));
        PrintBitArray(bits);
    }
    static void example2()
    {
        bool[] initValues = { true, false, true, true, false };
        BitArray bits2 = new(initValues);
        Console.WriteLine("\n\nbits2 content: {0}", BitArrayToString(bits2));
        PrintBitArray(bits2);
    }
    static void example3()
    {
        byte[] byteArray = { 0xAA, 0x55 };
        BitArray bits3 = new(byteArray);
        Console.WriteLine("\n\nbits3 content: {0}", BitArrayToString(bits3));
        PrintBitArray(bits3);
    }
    static void example4()
    {
        BitArray bits = new(8);

        bits.Set(2, true);
        bits.Set(5, true);
        bits[7] = true;
        Console.WriteLine("\n\nbits4 content: {0}", BitArrayToString(bits));
        PrintBitArray(bits);

        bits.SetAll(true);
        Console.WriteLine("bits4 after SetAll(true) content: {0}", BitArrayToString(bits));

        bits.SetAll(false);
        Console.WriteLine("bits4 after SetAll(false) content: {0}", BitArrayToString(bits));
    }
    static void example5()
    {
        BitArray bits = new(8);

        bits.SetAll(true);
        Console.WriteLine("\n\nbits5 content: {0}", BitArrayToString(bits));
        PrintBitArray(bits);
    }
    
    static void PrintResult(string title, BitArray bits1, BitArray? bits2, BitArray result)
    {
        Console.WriteLine(title);
        Console.WriteLine(BitArrayToString(bits1));
        if (bits2 != null) 
            Console.WriteLine(BitArrayToString(bits2));
        Console.WriteLine("-------------");
        Console.WriteLine(BitArrayToString(result));
    }
    static void example6(BitArray bits1, BitArray bits2)
    {
        BitArray resultAnd = new(bits1);
        resultAnd.And(bits2);

        PrintResult("\nBitwise AND Result: ",
            bits1, bits2, resultAnd);
    }
    static void example7(BitArray bits1, BitArray bits2)
    {
        BitArray resultOr = new(bits1);
        resultOr.Or(bits2);

        PrintResult("\nBitwise Or Result: ",
            bits1, bits2, resultOr);
    }
    static void example8(BitArray bits1, BitArray bits2)
    {
        BitArray resultNot = new(bits1);
        resultNot.Not();

        PrintResult("\nBitwise Not result: ",
            bits1, null, resultNot);
    }
    static void example9(BitArray bits1, BitArray bits2)
    {
        BitArray resultXor = new(bits1);
        resultXor.Xor(bits2);

        PrintResult("\nBitwise Xor result: ", bits1, bits2, resultXor);
    }
    private static void Main(string[] args)
    {
        BitArray bits1 = new(new bool[] {true, false, true, false});
        BitArray bits2 = new(new bool[] { true, true, true, false });

        //example1();
        //example2();
        //example3();
        //example4();
        //example5();
        Console.WriteLine("bits1: " + BitArrayToString(bits1));
        Console.WriteLine("bits2: " + BitArrayToString(bits2));
        Console.WriteLine("BitWise operators: ");
        //example6(bits1, bits2);
        //example7(bits1, bits2);
        //example8(bits1, bits2);
        example9(bits1, bits2);

    }
}