namespace General_Tree;

public class TreeNode<T>
{
    public T Value { get; set; }
    public List<TreeNode<T>> Children { get; set; }
    public TreeNode(T value)
    {
        Value = value;
        Children = new();
    }
    public void AddChild(TreeNode<T> value)
    {
        Children.Add(value);
    }
}
public class Tree<T>
{
    public TreeNode<T> root;
    public Tree(T value)
    {
        root = new(value);
    }
    public void printTree()
    {
        printTree(root);
    }
    public void printTree(TreeNode<T> node, string space = "")
    {
        if(node == null)
            return;
        Console.WriteLine(space + node.Value);
        foreach(var n in node.Children)
        {
            printTree(n, space + "   ");
        }
    }
}
class Program
{
    static void Main(string[] args)
    {
        Console.Clear();
        Tree<string> CompanyTree = new("CEO");
        TreeNode<string> Finance = new("CFO");
        TreeNode<string> Tech = new("CTO");
        TreeNode<string> Marketing = new("CMO");

        CompanyTree.root.AddChild(Finance);
        CompanyTree.root.AddChild(Tech);
        CompanyTree.root.AddChild(Marketing);

        Finance.AddChild(new("Accountant"));
        Tech.AddChild(new("Developer"));
        Tech.AddChild(new("UX Designer"));
        Marketing.AddChild(new("Social Media Manager"));

        CompanyTree.printTree();

    }
}