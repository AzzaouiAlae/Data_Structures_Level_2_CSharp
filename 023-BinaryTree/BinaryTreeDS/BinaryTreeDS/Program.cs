using System.Xml.Linq;

namespace BinaryTreeDS
{
    public class BinaryTreeNode<T>(T value)
    {
        public T Value { get; set; } = value;
        public BinaryTreeNode<T>? Left { get; set; }
        public BinaryTreeNode<T>? Right { get; set; }
        
    }
    public class BinaryTree<T>
    {
        public BinaryTree()
        {

        }
        public BinaryTree(T value)
        {
            Root = new(value);
        }
        public BinaryTreeNode<T>? Root { get; set; }
        static bool LevelOrderrderInsert(Queue<BinaryTreeNode<T>> nodes, BinaryTreeNode<T> current, T value)
        {
            if (current.Left == null)
            {
                current.Left = new BinaryTreeNode<T>(value);
                return true;
            }
            else
                nodes.Enqueue(current.Left);
            if (current.Right == null)
            {
                current.Right = new BinaryTreeNode<T>(value);
                return true;
            }
            else
                nodes.Enqueue(current.Right);
            return false;
        }
        public void Insert(T value)
        {
            Queue<BinaryTreeNode<T>> nodes;
            BinaryTreeNode<T> current;
            if (Root == null)
            {
                Root = new BinaryTreeNode<T>(value);
                return;
            }
            nodes = new();
            nodes.Enqueue(Root);
            while (nodes.Count > 0)
            {
                current = nodes.Dequeue();
                if (current == null)
                    break;
                if (LevelOrderrderInsert(nodes, current, value))
                    break;
            }
        }
        static void SortedOrderInsert(T value, BinaryTreeNode<T> current, Func<T, T, bool> isGreater)
        {
            while (true)
            {
                if (isGreater(current.Value, value))
                {
                    if (current.Left == null)
                    {
                        current.Left = new(value);
                        return;
                    }
                    current = current.Left;
                }
                else
                {
                    if (current.Right == null)
                    {
                        current.Right = new(value);
                        return;
                    }
                    current = current.Right;
                }
            }
        }
        public void Insert(T value, Func<T, T, bool> isGreater)
        {
            BinaryTreeNode<T> current;
            if (Root == null)
            {
                Root = new BinaryTreeNode<T>(value);
                return;
            }
            current = Root;
            SortedOrderInsert(value, current, isGreater);
        }
        public void PrintTree()
        {
            PrintTree(Root);
        }
        static void PrintTree(BinaryTreeNode<T>? node, int level = 0)
        {
            if (node == null)
                return;
            PrintTree(node.Right, level + 1);
            Console.WriteLine($"{new string('\t', level)}{node.Value}");
            PrintTree(node.Left, level + 1);
        }
        public void PreorderTraversal(Action<BinaryTreeNode<T>, int> action)
        {
            PreorderTraversal(Root, action);
        }     
        static void PreorderTraversal(BinaryTreeNode<T>? node, Action<BinaryTreeNode<T>, int> action, int level = 0)
        {
            if (node == null)
                return;
            action(node, level);
            PreorderTraversal(node.Left, action, level + 1);
            PreorderTraversal(node.Right, action, level + 1);
        }
        public void PostorderTraversal(Action<BinaryTreeNode<T>, int> action)
        {
            PostorderTraversal(Root, action);
        }
        static void PostorderTraversal(BinaryTreeNode<T>? node, Action<BinaryTreeNode<T>, int> action, int level = 0)
        {
            if (node == null)
                return;
            PostorderTraversal(node.Left, action, level + 1);
            PostorderTraversal(node.Right, action, level + 1);
            action(node, level);
        }
        public void InorderTraversal(Action<BinaryTreeNode<T>, int> action)
        {
            InorderTraversal(Root, action);
        }
        static void InorderTraversal(BinaryTreeNode<T>? node, Action<BinaryTreeNode<T>, int> action, int level = 0)
        {
            if (node == null)
                return;
            InorderTraversal(node.Left, action, level + 1);
            action(node, level);
            InorderTraversal(node.Right, action, level + 1);
        }
    }
    internal class Program
    {
        static bool IsGreater(string val1, string val2)
        {
            int num1, num2;
            if (int.TryParse(val1, out num1) 
                && int.TryParse(val2, out num2))
                return num1 > num2;
            return false;
        }
        static void Print(BinaryTreeNode<string> node, int level)
        {
            Console.Write($"{node.Value}, ");
        }
        static void Main(string[] args)
        {
            var tree = new BinaryTree<string>("40");

            tree.Insert("50", IsGreater);
            tree.Insert("30", IsGreater);
            tree.Insert("25", IsGreater);
            tree.Insert("35", IsGreater);
            tree.Insert("15", IsGreater);
            tree.Insert("28", IsGreater);
            tree.Insert("45", IsGreater);
            tree.Insert("60", IsGreater);
            tree.Insert("55", IsGreater);
            tree.Insert("70", IsGreater);
            tree.PrintTree();

            Console.WriteLine("Preordre Traversal with Print function: ");
            tree.PreorderTraversal(Print);

            Console.WriteLine("\n\nPostorder Traversal with Print function: ");
            tree.PostorderTraversal(Print);

            Console.WriteLine("\n\nInorder Traversal with Print function: ");
            tree.InorderTraversal(Print);
        }
    }
}
