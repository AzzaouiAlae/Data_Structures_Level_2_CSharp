namespace CodinGame;
public enum enType {
    WALL = 'W', ROOT = 'R', BASIC = 'B', TENTACLE = 'T', HARVESTER = 'H', SPORER = 'S', A = 'A', B = 'b', C = 'C', D = 'D', non
}
public enum enPlace{
        E, W, N, S
    } 
public class Cell
{
    public int x {get; set;}
    public int y {get; set;}
    public enType Type {get; set;}
    public int owner {get; set;}
    public int organId {get; set;}
    public string organDir {get; set;}
    public int organParentId {get; set;}
    public int organRootId {get; set;}
    public Cell(int x, int y)
    {
        this.x = x;
        this.y = y;
        Type = enType.non;
        owner = -1;
        organId = 0;
        organDir = "X";
        organParentId = -1;
        organRootId = 1;
    }
    public void print(string str = "")
    {
        Console.WriteLine($"{str}{x},{y}");
    }
}
public class game_grid
{
    int Width;
    int Height;
    public Tree<Cell> tree;
    public Dictionary<string, TreeNode<Cell>> nodes;
    public Dictionary<string, TreeNode<Cell>> roots;
    public game_grid(int width , int height)
    {
        Width = width;
        Height = height;
        nodes = new();
        tree = new(new Cell(0,0));
        roots = new();
        Creat_game_grid();
    }
    void Creat_game_grid()
    {
        for(int x = 0; x < Width; x++)
        {
            for(int y = 0; y < Height; y++)
                nodes.Add($"{x},{y}" ,new(new Cell(x, y)));
        }
        creat_Tree();
    }
    void creat_Tree()
    {
        if(nodes.ContainsKey($"{0},{0}"))
            tree.Root = nodes[$"{0},{0}"];
        for(int x = 0; x < Width; x++)
        {
            for(int y = 0; y < Height; y++)
            {
                if(nodes.ContainsKey($"{x},{y}"))
                {
                    if(nodes.ContainsKey($"{x + 1},{y}"))
                        nodes[$"{x},{y}"].AddChild(nodes[$"{x + 1},{y}"], enPlace.E);
                    if(nodes.ContainsKey($"{x - 1},{y}"))
                        nodes[$"{x},{y}"].AddChild(nodes[$"{x - 1},{y}"], enPlace.W);
                    if(nodes.ContainsKey($"{x},{y + 1}"))
                        nodes[$"{x},{y}"].AddChild(nodes[$"{x},{y + 1}"], enPlace.S);
                    if(nodes.ContainsKey($"{x},{y - 1}"))
                        nodes[$"{x},{y}"].AddChild(nodes[$"{x},{y - 1}"], enPlace.N);
                }
            }
        }
    }
    public void Set_cell_data(string data)
    {
        foreach(string node_data in data.Split('\n'))
            set_data(node_data);
    }
    public void set_data(string data)
    {
        if(string.IsNullOrEmpty(data))
            return;
        string []inputs = data.Split(' ');
        Cell c = nodes[$"{inputs[0]},{inputs[1]}"].Value;
        c.Type = inputs[3] == "B" ? (enType)'b' : (enType)inputs[2][0];
        c.owner = int.Parse(inputs[3]);
        c.organId = int.Parse(inputs[4]);
        c.organDir = inputs[5];
        c.organParentId = int.Parse(inputs[6]);
        c.organRootId = int.Parse(inputs[7]);
        if(c.Type == enType.ROOT && c.owner == 1)
            roots.Add($"{c.x},{c.y}", nodes[$"{inputs[0]},{inputs[1]}"]);
    }
    public Dictionary<string, Cell> the_current_way = new();
    void PrintTree(TreeNode<Cell> root, string space = "")
    {
        if(!the_current_way.ContainsKey($"{root.Value.x},{root.Value.y}"))
            the_current_way.Add($"{root.Value.x},{root.Value.y}", root.Value);
        else 
            return ;
        root.Value.print(space);
        foreach (var item in root.Children)
        {
            if(item == null)
                continue;
            PrintTree(item, space + "   ");
        }
        the_current_way.Remove($"{root.Value.x},{root.Value.y}");
    }
}
public class TreeNode<T>
{
    public T Value;
    public TreeNode<T>? W;
    public TreeNode<T>? E;
    public TreeNode<T>? S;
    public TreeNode<T>? N;
    public TreeNode(T value)
    {
        Value = value;
        search_order = [enPlace.E, enPlace.S, enPlace.W, enPlace.N];
    }
    public void AddChild(TreeNode<T> node, enPlace place)
    {
        switch (place)
        {
            case enPlace.W:
                W = node;
                break;
            case enPlace.E:
                E = node;
                break;
            case enPlace.S:
                S = node;
                break;
            case enPlace.N:
                N = node;
                break;
        }
    }
    public TreeNode<T>? Find(T value, int max_depth, bool first_time = true)
    {
        if(first_time)
            count = 0;
        count++;
        if(EqualityComparer<T>.Default.Equals(Value, value))
        {
            current_moves.Push(Value);
            count++;
            return this;
        }
        if(max_depth == 0 || current_moves.Contains(Value))
            return null;
        
        current_moves.Push(Value);
        foreach (var item in Children)
        {
            if(item == null)
                continue;
            var found = item.Find(value, max_depth - 1, false);
            if(found != null)
                return found;
        }
        if(current_moves.Count > 0)
            current_moves.Pop();
        return null;
    }
    public enPlace []search_order;
    void set_order_of_search(enPlace place, ref TreeNode<T>? node)
    {
        switch(place)
        {
            case enPlace.N:
                node = N;
                break;
            case enPlace.E:
                node = E;
                break;
            case enPlace.S:
                node = S;
                break;
            case enPlace.W:
                node = W;
                break;
        }
    }
    public TreeNode<T>? []Children
    {
        get 
        {
            TreeNode<T>? []nodes = new TreeNode<T>[4];
            for(int i = 0; i < nodes.Length; i++)
                set_order_of_search(search_order[i], ref nodes[i]);
            return nodes;
        }
    }
    public static Stack<T> current_moves = new();
    public static int count = 0;
}
public class Tree<T>
{
    public TreeNode<T> Root { get; set; }

    public Tree(T root)
    {
        Root = new TreeNode<T>(root);
    }
}
class Program
{
    static void Main(string[] args)
    {
        Console.Clear();
        TreeNode<Cell>? node = null;
        TreeNode<Cell>? node_result = null;
        game_grid game = new game_grid(16, 8);
        game.Set_cell_data("3 0 WALL -1 0 X 0 0\n6 0 WALL -1 0 X 0 0\n10 0 WALL -1 0 X 0 0\n15 0 WALL -1 0 X 0 0\n0 1 A -1 0 X 0 0\n6 1 B -1 0 X 0 0\n7 1 C -1 0 X 0 0\n9 1 C -1 0 X 0 0\n10 1 WALL -1 0 X 0 0\n14 1 B -1 0 X 0 0\n2 2 WALL -1 0 X 0 0\n3 2 WALL -1 0 X 0 0\n6 2 WALL -1 0 X 0 0\n7 2 WALL -1 0 X 0 0\n14 2 A -1 0 X 0 0\n2 3 WALL -1 0 X 0 0\n3 3 ROOT 1 1 N 0 1\n4 3 D -1 0 X 0 0\n5 3 WALL -1 0 X 0 0\n6 3 D -1 0 X 0 0\n8 3 WALL -1 0 X 0 0\n7 4 WALL -1 0 X 0 0\n9 4 D -1 0 X 0 0\n10 4 WALL -1 0 X 0 0\n11 4 D -1 0 X 0 0\n12 4 ROOT 0 2 N 0 2\n13 4 WALL -1 0 X 0 0\n1 5 A -1 0 X 0 0\n8 5 WALL -1 0 X 0 0\n9 5 WALL -1 0 X 0 0\n12 5 WALL -1 0 X 0 0\n13 5 WALL -1 0 X 0 0\n1 6 B -1 0 X 0 0\n5 6 WALL -1 0 X 0 0\n6 6 C -1 0 X 0 0\n8 6 C -1 0 X 0 0\n9 6 B -1 0 X 0 0\n15 6 A -1 0 X 0 0\n0 7 WALL -1 0 X 0 0\n5 7 WALL -1 0 X 0 0\n9 7 WALL -1 0 X 0 0\n12 7 WALL -1 0 X 0 0");
        
        for(int i = 0; i < 20; i++)
        {
            int count = 0;
            foreach(var r in game.roots)
            {
                count = TreeNode<Cell>.count;
                int x = r.Value.Value.x > 14 ? r.Value.Value.x - 14 : 14 - r.Value.Value.x;
                int y = r.Value.Value.y > 2 ? r.Value.Value.x - 2 : 2 - r.Value.Value.x;
                r.Value.search_order = [enPlace.E, enPlace.S, enPlace.W, enPlace.N];
                node = r.Value.Find(game.nodes[$"{14},{2}"].Value, x+y + i);
                if(count == 0 || count > TreeNode<Cell>.count)
                {
                    count = TreeNode<Cell>.count;
                    node_result = node;
                }
                if(node != null)
                    break;
            }
            if(node != null)
                    break;
        }
        
        //if(node != null )
            //Console.WriteLine($"{node.Value.x} {node.Value.y}");
        foreach(var m in TreeNode<Cell>.current_moves)
        {
            Console.WriteLine($"{m.x} {m.y} -> ");
        }
        TreeNode<Cell>.current_moves.Clear();
        Console.WriteLine($"{TreeNode<Cell>.count}");
        //Console.WriteLine("3 0 WALL -1 0 X 0 0\n6 0 WALL -1 0 X 0 0\n10 0 WALL -1 0 X 0 0\n15 0 WALL -1 0 X 0 0\n0 1 A -1 0 X 0 0\n6 1 B -1 0 X 0 0\n7 1 C -1 0 X 0 0\n9 1 C -1 0 X 0 0\n10 1 WALL -1 0 X 0 0\n14 1 B -1 0 X 0 0\n2 2 WALL -1 0 X 0 0\n3 2 WALL -1 0 X 0 0\n6 2 WALL -1 0 X 0 0\n7 2 WALL -1 0 X 0 0\n14 2 A -1 0 X 0 0\n2 3 WALL -1 0 X 0 0\n3 3 ROOT 1 1 N 0 1\n4 3 D -1 0 X 0 0\n5 3 WALL -1 0 X 0 0\n6 3 D -1 0 X 0 0\n8 3 WALL -1 0 X 0 0\n7 4 WALL -1 0 X 0 0\n9 4 D -1 0 X 0 0\n10 4 WALL -1 0 X 0 0\n11 4 D -1 0 X 0 0\n12 4 ROOT 0 2 N 0 2\n13 4 WALL -1 0 X 0 0\n1 5 A -1 0 X 0 0\n8 5 WALL -1 0 X 0 0\n9 5 WALL -1 0 X 0 0\n12 5 WALL -1 0 X 0 0\n13 5 WALL -1 0 X 0 0\n1 6 B -1 0 X 0 0\n5 6 WALL -1 0 X 0 0\n6 6 C -1 0 X 0 0\n8 6 C -1 0 X 0 0\n9 6 B -1 0 X 0 0\n15 6 A -1 0 X 0 0\n0 7 WALL -1 0 X 0 0\n5 7 WALL -1 0 X 0 0\n9 7 WALL -1 0 X 0 0\n12 7 WALL -1 0 X 0 0");
    }
}