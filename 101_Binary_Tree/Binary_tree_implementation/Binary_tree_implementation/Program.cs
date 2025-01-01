namespace Binary_tree_implementation;
public class BinaryTreeNode<T>
{
    public T Value{ get; set; }
    public BinaryTreeNode<T>? Left {get; set;}
    public BinaryTreeNode<T>? Right {get; set;}
    public BinaryTreeNode(T value)
    {
        Value = value;
        Left = null;
        Right = null;
    }
}
public class BinaryTree<T>
{
    public BinaryTreeNode<T> ? Root { get; private set; }
    public BinaryTree(T value)
    {
        Root = new BinaryTreeNode<T>(value);
    }
    public BinaryTree()
    {
        Root = null;
    }
    public void Insert(T value)
    {
        if(Root == null){
            Root = new BinaryTreeNode<T>(value);
            return;
        }
        Queue<BinaryTreeNode<T>> queue = new();
        queue.Enqueue(Root);
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
        PrintTree(Root);
    }
    void PrintTree(BinaryTreeNode<T>? root, int spaces = 0)
    {
        if(root == null)
            return;
        PrintTree(root.Right, spaces + 1);
        for(int i = 0; i < spaces; i++)
            Console.Write("\t");
        Console.WriteLine(root.Value);
        PrintTree(root.Left, spaces + 1);
    }
}
class Program
{
    static void Main(string[] args)
    {
        BinaryTree<int> tree = new();
        int []nums = {10,20,30,40,50,60,70,80,90,100,101,102,103,104,105};
        for(int i = 0; i < nums.Length; i++)
            tree.Insert(nums[i]);
        tree.PrintTree();
    }
}
