using System.Drawing;

namespace thisInClass
{
    public class myInt<T>
    {
        T? _value;
        public myInt(T? value)
        { 
            this._value = value; 
        }
        public T? Value
        {
            get { return _value; }
            set { _value = value; }
        }
        public static implicit operator T?(myInt<T> myInt) => myInt.Value;
        public static implicit operator myInt<T>(T? value) => new(value);
        public override string ToString()
        {
            return _value?.ToString() ?? string.Empty;
        }

    }

    internal class Program
    {
        static void Main(string[] args)
        {
            myInt<int> num = 50;
            Console.WriteLine(num);
        }
    }
}
