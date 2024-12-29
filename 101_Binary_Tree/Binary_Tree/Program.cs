using System.Runtime.InteropServices;
using System.Transactions;

namespace Binary_Tree;
class BinaryTreeNode<T>
{
    public T Value { get; set; }
    public BinaryTreeNode<T>? Left { get; set; }
    public BinaryTreeNode<T>? Right { get; set; }
    public BinaryTreeNode(T value)
    {
        Value = value;
        Left = null;
        Right = null;
    }
}
class BinaryTree<T> 
{
    public BinaryTreeNode<T>? Root {get; private set;}
    public BinaryTree()
    {
        Root = null;
    }
    public void Insert(T value)
    {
        var newNode = new BinaryTreeNode<T>(value);
        if(Root == null)
        {
            Root = newNode;
            return;
        }
        Queue<BinaryTreeNode<T>> queue = new();
        queue.Enqueue(Root);
        while(queue.Count > 0)
        {
            var current = queue.Dequeue();
            if(current.Left == null)
            {
                current.Left = newNode;
                break;
            }
            else
                queue.Enqueue(current.Left);
            if(current.Right == null)
            {
                current.Right = newNode;
                break;
            }
            else
                queue.Enqueue(current.Right);
        }
    }
    public void PrintTree()
    {
        PrintTree(Root, 0);
    }
    private void PrintTree(BinaryTreeNode<T>? root, int space)
    {
        int Count = 1;
        if(root == null)
            return;
        space += Count;
        PrintTree(root.Right, space);

        Console.WriteLine();
        for(int i = Count; i < space; i++)
            Console.Write("\t");
        Console.WriteLine(root.Value);

        PrintTree(root.Left, space);
    }
}
class MyBinaryTreeNode<T>
{
    public T Value {get; set;}
    public MyBinaryTreeNode<T>? Left {get; set;}
    public MyBinaryTreeNode<T>? Right {get; set;}
    public MyBinaryTreeNode(T value)
    {
        Value = value;
    }
}
class myBinaryTree<T>
{
    public MyBinaryTreeNode<T>? root {get; set;}
    public void Insert(T value)
    {
        if(root == null)
        {
            root = new(value);
            return;
        }
        Queue<MyBinaryTreeNode<T>> queue = new();
        queue.Enqueue(root);
        while(queue.Count > 0)
        {
            var current = queue.Dequeue();
            if(current.Left == null)
            {
                current.Left = new(value);
                break;
            }
            else
                queue.Enqueue(current.Left);
            if(current.Right == null)
            {
                current.Right = new(value);
                break;
            }
            else
                queue.Enqueue(current.Right);
        }
    }
    public void PrintTree()
    {
        PrintTree(root, 0);
    }
    void PrintTree(MyBinaryTreeNode<T>? Root, int space)
    {
        if(Root == null)
            return;
        PrintTree(Root.Right, space + 1);
        for(int i = 0; space > i; i++)
            Console.Write("\t");
        Console.WriteLine($"{Root.Value}");
        PrintTree(Root.Left, space + 1);
    }
    public void PreOrderTreeTraversal()
    {
        PreOrderTreeTraversal(root);
    }
    void PreOrderTreeTraversal(MyBinaryTreeNode<T>? Root)
    {
        if(Root == null)
            return;
        Console.WriteLine(Root.Value);
        PreOrderTreeTraversal(Root.Left);
        PreOrderTreeTraversal(Root.Right);
    }
    public void PostOrderTreeTraversal()
    {
        PostOrderTreeTraversal(root);
    }
    void PostOrderTreeTraversal(MyBinaryTreeNode<T>? Root)
    {
        if(Root == null)
            return;
        PostOrderTreeTraversal(Root.Left);
        PostOrderTreeTraversal(Root.Right);
        Console.WriteLine(Root.Value);
    }
    public void InOrderTreeTraversal()
    {
        InOrderTreeTraversal(root);
    }
    void InOrderTreeTraversal(MyBinaryTreeNode<T>? Root)
    {
        if(Root == null)
            return;
        InOrderTreeTraversal(Root.Left);
        Console.WriteLine(Root.Value);
        InOrderTreeTraversal(Root.Right);
    }
}
class Program
{
    void tree1()
    {
        int []Values = {5, 3, 8, 1, 4, 6, 9};
        BinaryTree<int> tree = new();
        foreach(int value in Values)
            tree.Insert(value);
        tree.PrintTree();
    }
    static void Main(string[] args)
    {
        Console.Clear();
        int []Values = {40,30,50,25,35,45,60,15,28};
        myBinaryTree<int> tree = new();
            foreach(var value in Values)
        tree.Insert(value);
        tree.PrintTree();
        //tree.PreOrderTreeTraversal();
        //tree.PostOrderTreeTraversal();
        tree.InOrderTreeTraversal();
    }
}
