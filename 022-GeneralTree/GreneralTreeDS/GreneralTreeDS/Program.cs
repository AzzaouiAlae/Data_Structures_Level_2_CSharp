using System.Collections;
using System.ComponentModel.Design.Serialization;
using System.Security.Cryptography;

namespace GreneralTreeDS
{

    public class TreeNode<T> : IEnumerable<TreeNode<T>>
    {
        public T Value { get; set; }
        public List<TreeNode<T>> Children { get; set; }
        public TreeNode(T value) 
        { 
            Value = value;
            Children = new();
        }
        public IEnumerator<TreeNode<T>> GetEnumerator()
        {
            return Children.GetEnumerator();
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
        public void Add(TreeNode<T> node)
        {
            Children.Add(node);
        }
        public void Add(T value)
        {
            Children.Add(new TreeNode<T>(value));
        }
        public void PrintTree(string prefix = "")
        {
            if (Value != null)
                Console.WriteLine(prefix + Value.ToString());
            foreach(TreeNode<T> node in Children)
                node.PrintTree(prefix + "   ");
        }
        public TreeNode<T>? Find(T value)
        {
            TreeNode<T>? find;

            if (Equals(Value, value)) 
                return this;
            foreach(TreeNode<T> node in Children)
            {
                find = node.Find(value);
                if (find != null)
                    return find;
            }
            return null;
        }
    }

    class Program
    {
        static void PrintSubTree(TreeNode<string> root, string name)
        {
            var find = root.Find(name);
            if (find != null)
            {
                Console.WriteLine($"\n{name} found!!\nit's children is: ");
                find.PrintTree();
            }
            else
                Console.WriteLine($"{name} found!!");
        }
        static void Main(string[] args)
        {
            TreeNode<string> root = new("CEO")
            {
                new TreeNode<string>("CFO")
                {
                    "Acountant"
                },
                new TreeNode<string>("CTO")
                {
                    "Developer", "UX Designer"
                },
                new TreeNode<string>("CMO")
                {
                    "Social Media Manager"
                },
            };
            root.PrintTree();
            PrintSubTree(root, "Acountant");
            PrintSubTree(root, "CFO");
            PrintSubTree(root, "CEO");
        }
        
    }
}
