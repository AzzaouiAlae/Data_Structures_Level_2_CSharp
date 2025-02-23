using System.Data.Common;
using System.Reflection.Metadata;

namespace CodinGame;

public enum enType {
    WALL = 'W', ROOT = 'R', BASIC = 'B', TENTACLE = 'T', HARVESTER = 'H', SPORER = 'S', A = 'A', B = 'b', C = 'C', D = 'D', non
}
public enum enDirection{
        E = 'E', W = 'W', N = 'N', S = 'S', X = 'X'
    } 
public struct Wieght
{
    public byte SPORE_ROOT;
    public byte SPORE;
    public byte BASIC;
    public byte HARVESTER;
    public byte TENTACLE;
}
public class Cell
{
    public int x {get; set;}
    public int y {get; set;}
    public enType Type {get; set;}
    public int owner {get; set;}
    public int organId {get; set;}
    public enDirection organDir {get; set;}
    public int organParentId {get; set;}
    public int organRootId {get; set;}
    public Cell(int x, int y)
    {
        this.x = x;
        this.y = y;
        Type = enType.non;
        owner = -1;
        organId = 0;
        organDir = enDirection.X;
        organParentId = -1;
        organRootId = 1;
    }
}
public class game_grid
{
    public int Width;
    public int Height;
    public GraphMatrix graph;
    List<int> used_roots;
    public MyEntitys myEntitys;
    public game_grid(int width , int height)
    {
        Width = width;
        Height = height;
        graph = new GraphMatrix(width, height);
        myEntitys = new();
        used_roots = new();
        Moves.Game = this;
        GraphMatrix.Check_Wieght = BASIC_moves.Check_Wieght;
        GraphMatrix.Get_Wieght = BASIC_moves.Get_Wieght_Wieght;
    }
    public void set_data(string data)
    {
        if(string.IsNullOrEmpty(data))
            return;
        string []inputs = data.Split(' ');
        Cell cell = set_cell_data(inputs);
        myEntitys.nodes[$"{inputs[0]} {inputs[1]}"] = cell;
    }
    Cell set_cell_data(string []inputs)
    {
        Cell c = new(int.Parse(inputs[0]), int.Parse(inputs[1]));
        c.Type = inputs[2] == "B" ? (enType)'b' : (enType)inputs[2][0];
        c.owner = int.Parse(inputs[3]);
        c.organId = int.Parse(inputs[4]);
        c.organDir = (enDirection)inputs[5][0];
        c.organParentId = int.Parse(inputs[6]);
        c.organRootId = int.Parse(inputs[7]);
        set_cells_to_his_list(c);
        return c;
    }
    void set_cells_to_his_list(Cell c)
    {
        int i = 0;
        if(c.x == 17 && c.y == 2)
            i = i + 2;
        if(c.Type == enType.ROOT && c.owner == 1){
            graph.DeleteInAdge(c.x, c.y);
            myEntitys.organ[$"{c.x} {c.y}"] = c;
            myEntitys.roots[$"{c.x} {c.y}"] = c;}
        else if(c.Type == enType.HARVESTER && c.owner == 1){
            graph.DeleteInAdge(c.x, c.y);
            myEntitys.organ[$"{c.x} {c.y}"] = c;
            myEntitys.HARVESTER[$"{c.x} {c.y}"] = c;}
        else if(c.Type == enType.TENTACLE && c.owner == 1){
            graph.DeleteInAdge(c.x, c.y);
            myEntitys.organ[$"{c.x} {c.y}"] = c;
            myEntitys.TENTACLE[$"{c.x} {c.y}"] = c;}
        else if(c.Type == enType.SPORER && c.owner == 1){
            graph.DeleteInAdge(c.x, c.y);
            myEntitys.organ[$"{c.x} {c.y}"] = c;
            myEntitys.SPORE[$"{c.x} {c.y}"] = c;}
        else if(c.Type == enType.BASIC && c.owner == 1){
            graph.DeleteInAdge(c.x, c.y);
            myEntitys.organ[$"{c.x} {c.y}"] = c;
            myEntitys.BASIC[$"{c.x} {c.y}"] = c;}
        else if(c.Type == enType.A){
            myEntitys.p[$"{c.x} {c.y}"] = c;
            graph.AddHARVESTERAdge(c.x, c.y);
            myEntitys.A[$"{c.x} {c.y}"] = c;}
        else if(c.Type == enType.B){
            myEntitys.p[$"{c.x} {c.y}"] = c;
            graph.AddHARVESTERAdge(c.x, c.y);
            myEntitys.B[$"{c.x} {c.y}"] = c;}
        else if(c.Type == enType.C){
            myEntitys.p[$"{c.x} {c.y}"] = c;
            graph.AddHARVESTERAdge(c.x, c.y);
            myEntitys.C[$"{c.x} {c.y}"] = c;}
        else if(c.Type == enType.D){
            myEntitys.p[$"{c.x} {c.y}"] = c;
            graph.AddHARVESTERAdge(c.x, c.y);
            myEntitys.D[$"{c.x} {c.y}"] = c;}
        else if(c.Type == enType.ROOT || c.Type == enType.HARVESTER || c.Type == enType.TENTACLE || c.Type == enType.SPORER
        || c.Type == enType.BASIC || c.Type == enType.WALL)
        {
            graph.DeleteAdge(c.x, c.y);
            delete_HARVESTE_Cell(c.x, c.y);
            if(c.Type != enType.WALL)
                myEntitys.enmy[$"{c.x} {c.y}"] = c;
        }
        if(c.Type == enType.ROOT || c.Type == enType.HARVESTER || c.Type == enType.TENTACLE || c.Type == enType.SPORER
        || c.Type == enType.BASIC || c.Type == enType.WALL)
        {
            myEntitys.All[$"{c.x} {c.y}"] = c;
        }
    }
    void delete_HARVESTE_Cell(int x, int y)
    {
        if(Moves.Game.myEntitys.HARVESTER_for_a.ContainsKey($"{x} {y}"))
            Moves.Game.myEntitys.HARVESTER_for_a.Remove($"{x} {y}");
        if(Moves.Game.myEntitys.HARVESTER_for_b.ContainsKey($"{x} {y}"))
            Moves.Game.myEntitys.HARVESTER_for_b.Remove($"{x} {y}");
        if(Moves.Game.myEntitys.HARVESTER_for_c.ContainsKey($"{x} {y}"))
            Moves.Game.myEntitys.HARVESTER_for_c.Remove($"{x} {y}");
        if(Moves.Game.myEntitys.HARVESTER_for_d.ContainsKey($"{x} {y}"))
            Moves.Game.myEntitys.HARVESTER_for_d.Remove($"{x} {y}");
    }
    public bool is_used_root(int root_id)
    {
        return used_roots.Contains(root_id);
    }
    public void use_root(int id)
    {
        used_roots.Add(id);
    }
}
public class MyEntitys
{
    public Dictionary<string, Cell> nodes;
    public Dictionary<string, Cell> roots;
    public Dictionary<string, Cell> HARVESTER;
    public Dictionary<string, Cell> TENTACLE;
    public Dictionary<string, Cell> SPORE;
    public Dictionary<string, Cell> BASIC;
    public Dictionary<string, Cell> A;
    public Dictionary<string, Cell> HARVESTER_for_a;
    public Dictionary<string, Cell> B;
    public Dictionary<string, Cell> HARVESTER_for_b;
    public Dictionary<string, Cell> C;
    public Dictionary<string, Cell> HARVESTER_for_c;
    public Dictionary<string, Cell> D;
    public Dictionary<string, Cell> HARVESTER_for_d;
    public Dictionary<string, Cell> p;
    public Dictionary<string, Cell> organ;
    public Dictionary<string, Cell> enmy;
    public Dictionary<string, Cell> All;
    public MyEntitys()
    {
        All = new();
        enmy = new();
        HARVESTER_for_a= new();
        HARVESTER_for_b= new();
        HARVESTER_for_c= new();
        HARVESTER_for_d= new();
        nodes = new();
        roots = new();
        HARVESTER = new();
        TENTACLE = new();
        SPORE = new();
        BASIC = new();
        A = new();
        B = new();
        C = new();
        D = new();
        p = new();
        organ = new();
    }
    public void ClearAll()
    {
        All.Clear();
        enmy.Clear();
        nodes.Clear();
        roots.Clear();
        HARVESTER.Clear();
        TENTACLE.Clear();
        SPORE.Clear();
        BASIC.Clear();
        A.Clear();
        B.Clear();
        C.Clear();
        D.Clear();
        p.Clear();
        organ.Clear();
    }
}
public class Moves
{
    static public string prev_move = "";
    static public string prev_move_id = "";
    static public bool is_debug = false;
    public static int count = 0;
    static public List<int> used_roots {get; set;} = new();
    static public game_grid Game;
    public Moves(game_grid game)
    {
        count++;
        Game = game;
    }
    public void set_up_moves()
    {
        if(Proteins.A > 8 && Proteins.B > 3 && Proteins.C > 5 && Proteins.D > 5)
        {
            GraphMatrix.Check_Wieght = SPORE_moves.Check_Wieght;
            GraphMatrix.Get_Wieght = SPORE_moves.Get_Wieght_Wieght;
            SPORE_moves.set_SPORE_Moves();
            GraphMatrix.Check_Wieght = BASIC_moves.Check_Wieght;
            GraphMatrix.Get_Wieght = BASIC_moves.Get_Wieght_Wieght;
            HARVESTER_moves.Create_HARVESTERS();
        }
        else
        {
            GraphMatrix.Check_Wieght = BASIC_moves.Check_Wieght;
            GraphMatrix.Get_Wieght = BASIC_moves.Get_Wieght_Wieght;
            HARVESTER_moves.Create_HARVESTERS();
            GraphMatrix.Check_Wieght = SPORE_moves.Check_Wieght;
            GraphMatrix.Get_Wieght = SPORE_moves.Get_Wieght_Wieght;
            SPORE_moves.set_SPORE_Moves();
        }
    }
    public void Find_Move()
    {
        if(!Proteins.has_stock())
        {
            Console.WriteLine("WAIT");
            return;
        }
        if(prev_move != "" && Game.myEntitys.organ.ContainsKey(prev_move_id) && !used_roots.Contains(Game.myEntitys.organ[prev_move_id].organRootId))
        {
            Console.WriteLine($"SPORE {Game.myEntitys.organ[prev_move_id].organId} {prev_move}");
            prev_move = "";
            return;
        }
        if(Proteins.TENTACLE && TENTACLE_moves.kill_enmy())
            return;
        if(Proteins.TENTACLE && TENTACLE_moves.any_move())
            return;
        if(Proteins.A > 8 && Proteins.B > 3 && Proteins.C > 5 && Proteins.D > 5)
        {
            if(Proteins.SPORE && SPORE_moves.print_SPORE_moves()){
                return;}
            if(!used_roots.Contains(1) && Proteins.HARVESTER && HARVESTER_moves.print_proteins_way())
                return;
        }
        else
        {
            if(!used_roots.Contains(1) && Proteins.HARVESTER && HARVESTER_moves.print_proteins_way())
                return;
            if(Proteins.SPORE && SPORE_moves.print_SPORE_moves())
                return;
        }
        if(prev_move == "" && Proteins.SPORE && SPORE_moves.any_move()){
            return; }
        if(BASIC_moves.any_move1())
            return;
        if(BASIC_moves.any_move2())
            return;
        Console.WriteLine("WAIT");
    }
    static public string Get_organ_name(Wieght wieght)
    {
        if(wieght.HARVESTER > 0 && Proteins.HARVESTER){
            Proteins.HARVESTER_created();
            return $"HARVESTER {(char)wieght.HARVESTER}";}
        if(wieght.TENTACLE > 0 && Proteins.TENTACLE){
            Proteins.TENTACLE_created();
            return $"TENTACLE {(char)wieght.TENTACLE}";}
        if(wieght.SPORE_ROOT > 0 && Proteins.SPORE_alown){
            Proteins.TENTACLE_created();
            return $"SPORER {(char)wieght.SPORE_ROOT}";}
        if(wieght.BASIC > 0 && Proteins.BASIC){
            Proteins.BASIC_created();
            return $"BASIC";}
        return "";
    }
    static public string use_any_organ(Wieght wieght)
    {
        string str = Get_organ_name(wieght);
        if(str == "")
        {
            Proteins.bigest_organ_stock(ref wieght);
            return Get_organ_name(wieght);
        }
        return str;
    }
    public bool has_A()
    {
        foreach(var node in Game.myEntitys.A)
        {
            if(Proteins.Has_my_HARVESTER(node.Value))
                return true;
        }
        return false;
    }
    public bool has_B()
    {
        foreach(var node in Game.myEntitys.B)
        {
            if(Proteins.Has_my_HARVESTER(node.Value))
                return true;
        }
        return false;
    }
    public bool has_C()
    {
        foreach(var node in Game.myEntitys.C)
        {
            if(Proteins.Has_my_HARVESTER(node.Value))
                return true;
        }
        return false;
    }
    public bool has_D()
    {
        foreach(var node in Game.myEntitys.D)
        {
            if(Proteins.Has_my_HARVESTER(node.Value))
                return true;
        }
        return false;
    }
}
public class BASIC_moves()
{
    static (int, Stack<GraphMatrix.Dijkstart_node>?) moves = new ();
    static MovesBinaryTree Proteins_moves;
    static public bool Check_Wieght(Wieght wieght)
    {
        if(wieght.BASIC > 0 && wieght.BASIC < 2)
            return true;
        return false;
    }
    static public int Get_Wieght_Wieght(Wieght wieght)
    {
        return wieght.BASIC;
    }
    static public bool any_move1()
    {
        GraphMatrix.Check_Wieght = Check_Wieght;
        GraphMatrix.Get_Wieght = Get_Wieght_Wieght;
        foreach(var node in Moves.Game.myEntitys.organ)
        {
            if(Moves.used_roots.Contains(node.Value.organRootId))
                continue;
            if(Moves.Game != null && Moves.Game.graph != null)
            {
                GraphMatrix.Dijkstart_node? result = Moves.Game.graph.Getfirst_move(node.Key);
                if(result != null)
                {
                    if(is_TENTACLE_of_enemy_in_direction(result.Value.vertexName))
                        continue;
                    Wieght W = result.Value.wieght;
                    W.TENTACLE = is_front_enmy(result.Value.vertexName);
                    W.HARVESTER = is_front_p(result.Value.vertexName);
                    string str = Moves.use_any_organ(W);
                    Moves.used_roots.Add(node.Value.organRootId);
                    Console.WriteLine($"GROW {node.Value.organId} {result.Value.vertexName} {str}");
                    return true;
                }
            }
        }
        return false;
    }
    static public bool any_move2()
    {
        Wieght wieght = new();
        wieght.BASIC = 1;
        foreach(var organ in Moves.Game.myEntitys.organ)
        {
            if(Moves.used_roots.Contains(organ.Value.organRootId))
                continue;
            string str = Moves.use_any_organ(wieght);
            if(move2(organ.Value, $"{organ.Value.x + 1} {organ.Value.y}", str)){
                Moves.used_roots.Add(organ.Value.organRootId);
                return true;}
            if(move2(organ.Value, $"{organ.Value.x - 1} {organ.Value.y}", str)){
                Moves.used_roots.Add(organ.Value.organRootId);
                return true;}
            if(move2(organ.Value, $"{organ.Value.x} {organ.Value.y + 1}", str)){
                Moves.used_roots.Add(organ.Value.organRootId);
                return true;}
            if(move2(organ.Value, $"{organ.Value.x} {organ.Value.y - 1}", str)){
                Moves.used_roots.Add(organ.Value.organRootId);
                return true;}
        }
        return false;
    }
    static bool move2(Cell c, string move, string str)
    {
        if(!(int.Parse(move.Split(' ')[0]) >= 0 && int.Parse(move.Split(' ')[0]) < Moves.Game.Width))
            return false;
        if(!(int.Parse(move.Split(' ')[1]) >= 0 && int.Parse(move.Split(' ')[1]) < Moves.Game.Height))
            return false;
        if(is_posible_move(move) && !is_TENTACLE_of_enemy_in_direction(move))
        {
            if(str == "")
                return false;
            Console.WriteLine($"GROW {c.organRootId} {move} {str}");
            return true;
        }
        return false;
    }
    public static bool is_TENTACLE_of_enemy(string vertexName, enDirection dir)
    {
        if(Moves.Game.myEntitys.enmy.ContainsKey(vertexName))
        {
            if(Moves.Game.myEntitys.enmy[vertexName].Type == enType.TENTACLE && Moves.Game.myEntitys.enmy[vertexName].organDir == dir)
                return true;
        }
        return false;
    }
    public static bool is_TENTACLE_of_enemy_in_direction(string vertexName)
    {
        int x = int.Parse(vertexName.Split(' ')[0]);
        int y = int.Parse(vertexName.Split(' ')[1]);
        if(is_TENTACLE_of_enemy($"{x + 1} {y}", enDirection.W))
            return true;
        if(is_TENTACLE_of_enemy($"{x - 1} {y}", enDirection.E))
            return true;
        if(is_TENTACLE_of_enemy($"{x} {y + 1}", enDirection.N))
            return true;
        if(is_TENTACLE_of_enemy($"{x} {y - 1}", enDirection.S))
            return true;
        return false;
    }
    public static bool is_posible_move(string vertexName, bool is_my_TENTACLE = false)
    {
        if(is_my_TENTACLE && !is_TENTACLE_of_enemy_in_direction(vertexName))
            return true;
        if(is_TENTACLE_of_enemy_in_direction(vertexName))
            return false;
        if(Moves.Game.myEntitys.All.ContainsKey(vertexName))
        {
            switch(Moves.Game.myEntitys.All[vertexName].Type)
            {
                case enType.A:
                case enType.B:
                case enType.C:
                case enType.D:
                    return true;
            }
            return false;
        }
        return true;
    }
    static byte is_front_enmy(string vertexName)
    {
        int x = int.Parse(vertexName.Split(' ')[0]);
        int y = int.Parse(vertexName.Split(' ')[1]);
        if(!Proteins.TENTACLE)
            return 0;
        if(Moves.Game.myEntitys.enmy.ContainsKey($"{x + 1} {y}") || Moves.Game.myEntitys.enmy.ContainsKey($"{x + 2} {y}"))
            return (byte)enDirection.E;
        if(Moves.Game.myEntitys.enmy.ContainsKey($"{x - 1} {y}") || Moves.Game.myEntitys.enmy.ContainsKey($"{x - 2} {y}"))
            return (byte)enDirection.W;
        if(Moves.Game.myEntitys.enmy.ContainsKey($"{x} {y - 1}") || Moves.Game.myEntitys.enmy.ContainsKey($"{x} {y - 2}"))
            return (byte)enDirection.N;
        if(Moves.Game.myEntitys.enmy.ContainsKey($"{x} {y + 1}") || Moves.Game.myEntitys.enmy.ContainsKey($"{x} {y + 2}"))
            return (byte)enDirection.S;
        return 0;
    }
    static byte is_front_p(string vertexName)
    {
        int x = int.Parse(vertexName.Split(' ')[0]);
        int y = int.Parse(vertexName.Split(' ')[1]);
        if(Moves.Game.myEntitys.p.ContainsKey($"{x + 1} {y}") && !Proteins.Has_any_HARVESTER(x + 1, y))
            return (byte)enDirection.E;
        if(Moves.Game.myEntitys.p.ContainsKey($"{x - 1} {y}") && !Proteins.Has_any_HARVESTER(x - 1, y))
            return (byte)enDirection.W;
        if(Moves.Game.myEntitys.p.ContainsKey($"{x} {y - 1}") && !Proteins.Has_any_HARVESTER(x, y - 1))
            return (byte)enDirection.N;
        if(Moves.Game.myEntitys.p.ContainsKey($"{x} {y + 1}") && !Proteins.Has_any_HARVESTER(x, y + 1))
            return (byte)enDirection.S;
        return 0;
    }
    static public bool go_to_Proteins()
    {
        GraphMatrix.Check_Wieght = Check_Wieght;
        GraphMatrix.Get_Wieght = Get_Wieght_Wieght;
        MovesBinaryTree move;
        int count = 0;
        foreach(var p in Moves.Game.myEntitys.p)
        {
            move = new(0, new());
            foreach (var Node in Moves.Game.myEntitys.organ)
            {
                var turn = Moves.Game.graph.GetPath(Node.Key, p.Key);
                if(turn.Item2 != null && (move.Distance > turn.Item1 || move.Distance == 0))
                {
                    move.Distance = turn.Item1;
                    move.moves = turn.Item2;
                }
            }
            if(Proteins_moves == null && move.Distance != 0){ count++;
                Proteins_moves = new(move.Distance, move.moves);}
            else if(Proteins_moves != null && move.Distance != 0){ count++;
                Proteins_moves.AddNode(move.Distance, move.moves);}
        }
        move = new(0, new());
        if(Proteins_moves != null)
            Proteins_moves.PrintAllMoves();
        return true;
    }
    static bool Dequeue_moves()
    {
        if(Proteins_moves.queue.Count > 0)
        {
            moves.Item2 = Proteins_moves.queue.Dequeue();
            moves.Item1 = moves.Item2.Count;
            return true;
        }
        return false;
    }
    static public bool print_proteins_way()
    {
        if(moves.Item1 == 0)
        {
            if(!Dequeue_moves())
                return false;
        }
        if(moves.Item1 > 0 && moves.Item2 != null)
        {
            GraphMatrix.Dijkstart_node item;
            while(true)
            {
                if(moves.Item2 == null || moves.Item2.Count == 0)
                {
                    if(!Dequeue_moves())
                        return false;
                }
                item = moves.Item2.Pop();
                if(!Moves.Game.myEntitys.organ.ContainsKey(item.prev_node_Name))
                {
                    moves.Item1 = 0;
                    moves.Item2 = null;
                }
                if(Moves.Game.graph.isEdge(item.prev_node_Name, item.vertexName))
                    break;
            }
            string str = Moves.use_any_organ(item.wieght);
            if(Moves.Game.myEntitys.organ.ContainsKey(item.prev_node_Name)){
                Moves.Game.graph.DeleteInAdge(int.Parse(item.vertexName.Split(' ')[0]), int.Parse(item.vertexName.Split(' ')[1]));
                Console.WriteLine($"GROW {Moves.Game.myEntitys.organ[item.prev_node_Name].organId} {item.vertexName} {str}");}
            else
                return false;
            if(moves.Item2.Count == 0)
            {
                moves.Item1 = 0;
                moves.Item2 = null;
            }
            return true;
        }
        return false;
    }
}
public class MovesBinaryTree
{
    public Queue<Stack<GraphMatrix.Dijkstart_node>> queue = new();
    public int Distance;
    public Stack<GraphMatrix.Dijkstart_node>? moves;
    MovesBinaryTree? Left;
    MovesBinaryTree? Right;
    public MovesBinaryTree(int distance, Stack<GraphMatrix.Dijkstart_node> node)
    {
        Distance = distance;
        moves = node;
        Left = null;
        Right = null;
    }
    public void AddNode(int distance, Stack<GraphMatrix.Dijkstart_node> Moves)
    {
        if(Distance > distance)
            AddNode(ref Left, distance, Moves);
        else
            AddNode(ref Right, distance, Moves);
    }
    void AddNode(ref MovesBinaryTree? node, int distance, Stack<GraphMatrix.Dijkstart_node> Moves)
    {
        if(node == null)
        {
            node = new(distance, Moves);
            return;
        }
        if(distance < node.Distance)
            AddNode(ref node.Left, distance, Moves);
        else
            AddNode(ref node.Right, distance, Moves);
    }
    public void PrintAllMoves()
    {
        PrintAllMoves(Left, queue);
        queue.Enqueue(moves);
        PrintAllMoves(Right, queue);
    }
    int count = 0;
    void PrintAllMoves(MovesBinaryTree? MOVE, Queue<Stack<GraphMatrix.Dijkstart_node>> Queue)
    {
        if(MOVE == null)
            return ;
        count++;
        PrintAllMoves(MOVE.Left, Queue);
        Queue.Enqueue(MOVE.moves);
        PrintAllMoves(MOVE.Right, Queue);
    }
    public void PrintMoves(ref Stack<GraphMatrix.Dijkstart_node>? MOVES)
    {
        while(MOVES != null && MOVES.Count > 0)
        {
            GraphMatrix.Dijkstart_node node = MOVES.Pop();
            string str = Moves.use_any_organ(node.wieght);
            //Console.WriteLine($"GROW id={node.prev_node_Name} {node.vertexName} {str}");
            Console.WriteLine($"GROW {Moves.Game.myEntitys.organ[node.prev_node_Name].organId} {node.vertexName} {str}");
        }
    }
}
public class SPORE_moves()
{
    static (int, Stack<GraphMatrix.Dijkstart_node>?) moves = new ();
    static MovesBinaryTree Proteins_moves;
    static string[] MOVE = new string[] {"", "", ""};
    static bool Dequeue_moves()
    {
        if(Proteins_moves.queue.Count > 0)
        {
            moves.Item2 = Proteins_moves.queue.Dequeue();
            moves.Item1 = moves.Item2.Count;
            return true;
        }
        return false;
    }
    static public bool Check_Wieght(Wieght wieght)
    {
        if(wieght.SPORE_ROOT > 0 || wieght.BASIC > 0)
            return true;
        return false;
    }
    static public  int Get_Wieght_Wieght(Wieght wieght)
    {
        return wieght.SPORE_ROOT + wieght.BASIC;
    }
    public static void set_SPORE_Moves()
    {
        foreach(var root in Moves.Game.myEntitys.roots)
        {
            Proteins_moves = Moves.Game.graph.Get_Table_for_SPORE(root.Key);
            Proteins_moves.PrintAllMoves();
        }
    }
    public static bool any_move()
    {
        foreach(var organ in Moves.Game.myEntitys.organ)
        {
            if(Moves.used_roots.Contains(organ.Value.organRootId))
                continue;
            string str = "";
            string dir = "";
            if(count_Line_of_organ(organ.Value, ref str, ref dir) > 3)
            {
                Moves.prev_move_id = $"{dir.Split(' ')[0]} {dir.Split(' ')[1]}";
                Moves.prev_move = $"{str}";
                Console.WriteLine($"GROW {organ.Value.organId} {dir}");
                Moves.used_roots.Add(organ.Value.organRootId);
                return true;
            }
        }
        return false;
    }
    static int count_Line_of_organ(Cell c, ref string result, ref string dir)
    {
        int i = 0;
        int x_plus = 1;
        bool is_x_plus = true;
        int x_min = -1;
        bool is_x_min = true;
        int y_plus = 1;
        bool is_y_plus = true;
        int y_min = -1;
        bool is_y_min = true;
        while(i < Moves.Game.Width || i < Moves.Game.Height)
        {
            if(i < Moves.Game.Width)
            {
                if(is_x_plus && !is_front_of_enamy($"{c.x + x_plus} {c.y}") && BASIC_moves.is_posible_move($"{c.x + x_plus} {c.y}"))
                    x_plus++;
                else
                    is_x_plus = false;
                if(is_x_min && !is_front_of_enamy($"{c.x + x_min} {c.y}") && BASIC_moves.is_posible_move($"{c.x + x_min} {c.y}"))
                    x_min--;
                else
                    is_x_min = false;
            }
            if(i < Moves.Game.Height)
            {
                if(is_y_plus && !is_front_of_enamy($"{c.x} {c.y + y_plus}") && BASIC_moves.is_posible_move($"{c.x} {c.y + y_plus}"))
                    y_plus++;
                else
                    is_y_plus = false;
                if(is_y_min && !is_front_of_enamy($"{c.x} {c.y + y_min}") && BASIC_moves.is_posible_move($"{c.x} {c.y + y_min}"))
                    y_min--;
                else
                    is_y_min = false;
            }
            i++;
        }
        Cell enmy = Moves.Game.myEntitys.enmy.First().Value;
        int distance_x_p = get_distance(c.x + x_plus, c.y, enmy.x, enmy.y);
        int distance_x_m = get_distance(c.x + x_min, c.y, enmy.x, enmy.y);
        int distance_y_p = get_distance(c.x , c.y + y_plus, enmy.x, enmy.y);
        int distance_y_m = get_distance(c.x, c.y +y_min, enmy.x, enmy.y);

        if(distance_x_p < distance_x_m && distance_x_p < distance_y_p && distance_x_p < distance_y_m)
        {
            dir = $"{c.x + 1} {c.y} SPORER E";
            result = $"{c.x + x_plus} {c.y}";
            return x_plus;
        }
        if(distance_x_m < distance_x_p && distance_x_m < distance_y_p && distance_x_m < distance_y_m)
        {
            dir = $"{c.x - 1} {c.y} SPORER W";
            result = $"{c.x + x_min} {c.y}";
            return x_min * -1;
        }
        if(distance_y_p < distance_x_m && distance_y_p < distance_x_p && distance_y_p < distance_y_m)
        {
            dir = $"{c.x} {c.y + 1} SPORER S";
            result = $"{c.x} {c.y + y_plus}";
            return y_plus ;
        }
        if(distance_y_m < distance_x_m && distance_y_m < distance_y_p && distance_y_m < distance_x_p)
        {
            dir = $"{c.x} {c.y - 1} SPORER N";
            result = $"{c.x} {c.y + y_min}";
            return y_min * -1;
        }
        return 0;
    }
    static int get_distance(int x1, int y1, int x2, int y2)
    {
        int x_dist = x1 - x2;
        int y_dist = y1 - y2;
        if(x_dist < 0)
            x_dist *= -1;
        if(y_dist < 0)
            y_dist *= -1;
        return x_dist + y_dist;
    }
    static bool is_front_of_enamy(string vertexName)
    {
        int x;
        int y;
        string []str =  vertexName.Split(' ');
        if(str.Length < 2)
            return false;
        if(!int.TryParse(str[0], out x))
            return false;
        if(!int.TryParse(str[1], out y))
            return false;
        for(int i = -5; i < 6; i++)
        {
            for(int j = -5; j < 6; j++)
            {
                if(Moves.Game.myEntitys.enmy.ContainsKey($"{x + i} {y + j}"))
                    return true;
            }
        }
        return false;
    }
    static public bool print_SPORE_moves()
    {
        if(MOVE[0] != "")
        {
            if(!Moves.is_debug)
            {
                if(!Moves.Game.myEntitys.organ.ContainsKey(MOVE[1]))
                {
                    MOVE[0] = "";
                    return false;
                }
                Console.WriteLine($"{MOVE[0]} {Moves.Game.myEntitys.organ[MOVE[1]].organId} {MOVE[2]}");
                Moves.used_roots.Add(Moves.Game.myEntitys.organ[MOVE[1]].organRootId);
            }
            else
                Console.WriteLine($"{MOVE[0]} \'{MOVE[1]}\' {MOVE[2]}");
            MOVE[0] = "";
            return true;
        }
        GraphMatrix.Check_Wieght = Check_Wieght;
        GraphMatrix.Get_Wieght = Get_Wieght_Wieght;
        if(moves.Item1 == 0)
        {
            if(!Dequeue_moves())
                return false;
        }
        if(moves.Item1 > 0 && moves.Item2 != null)
        {
            GraphMatrix.Dijkstart_node item = new();
            if(Moves.is_debug)
                item = moves.Item2.Pop(); ///+++
            while(true && !Moves.is_debug)
            {
                if(moves.Item2 == null || moves.Item2.Count == 0)
                {
                    if(!Dequeue_moves())
                        return false;
                }
                item = moves.Item2.Pop();
                if(!Moves.Game.myEntitys.organ.ContainsKey(item.prev_node_Name))
                {
                    moves.Item1 = 0;
                    moves.Item2 = null;
                    continue;
                }
                if(Moves.Game.graph.isEdge(item.prev_node_Name, item.vertexName))
                    break;
            }
            item.wieght.SPORE_ROOT = item.wieght.SPORE;
            string str = Moves.use_any_organ(item.wieght);
            if(Moves.Game.myEntitys.organ.ContainsKey(item.prev_node_Name) || true)
            {
                if(str.Contains("SPORER"))
                    create_SPORER(item, moves.Item2);
                else
                {
                    if(Moves.is_debug)
                        Console.WriteLine($"GROW \'{item.prev_node_Name}\' {item.vertexName} {str}");
                    else
                    {
                        Console.WriteLine($"GROW {Moves.Game.myEntitys.organ[item.prev_node_Name].organId} {item.vertexName} {str}");
                        Moves.Game.graph.DeleteInAdge(int.Parse(item.vertexName.Split(' ')[0]), int.Parse(item.vertexName.Split(' ')[1]));
                    }
                }
            }
            else
                return false;
            if(moves.Item2.Count == 0)
            {
                moves.Item1 = 0;
                moves.Item2 = null;
            }
            if(!Moves.is_debug)
                Moves.used_roots.Add(Moves.Game.myEntitys.organ[item.prev_node_Name].organRootId);
            
            return true;
        }
        return false;
    }
    public static void create_SPORER(GraphMatrix.Dijkstart_node item, Stack<GraphMatrix.Dijkstart_node>? stack)
    {
        int x = int.Parse(item.prev_node_Name.Split(' ')[0]);
        int y = int.Parse(item.prev_node_Name.Split(' ')[1]);
        if(item.wieght.SPORE_ROOT == 'W')
            x--;
        if(item.wieght.SPORE_ROOT == 'S')
            y++;
        if(item.wieght.SPORE_ROOT == 'N')
            y--;
        if(item.wieght.SPORE_ROOT == 'E')
            x++;
        if(Moves.is_debug)
            Console.WriteLine($"GROW \'{item.prev_node_Name}\' {x} {y} SPORER {(char)item.wieght.SPORE_ROOT}");
        else
           Console.WriteLine($"GROW {Moves.Game.myEntitys.organ[item.prev_node_Name].organId} {x} {y} SPORER {(char)item.wieght.SPORE_ROOT}");
        MOVE[0] = "SPORE";
        MOVE[1] = $"{x} {y}";
        MOVE[2] = item.vertexName;
    }
}
public class HARVESTER_moves()
{
    static (int, Stack<GraphMatrix.Dijkstart_node>?) moves = new ();
    static MovesBinaryTree Proteins_moves;
    static public bool Check_Wieght(Wieght wieght)
    {
        if(wieght.HARVESTER > 0 || wieght.BASIC > 0)
            return true;
        return false;
    }
    static public  int Get_Wieght_Wieght(Wieght wieght)
    {
        return wieght.HARVESTER;
    }
    static public bool Create_HARVESTER(Dictionary<string, Cell> place)
    {
        GraphMatrix.Check_Wieght = Check_Wieght;
        GraphMatrix.Get_Wieght = Get_Wieght_Wieght;
        MovesBinaryTree move = new(0, new());
        int count = 0;
        foreach(var p in place)
        {
            foreach (var Node in Moves.Game.myEntitys.organ)
            {
                var turn = Moves.Game.graph.GetPath(Node.Key, p.Key);
                if(turn.Item2 != null && (move.Distance > turn.Item1 || move.Distance == 0))
                {
                    move.Distance = turn.Item1;
                    move.moves = turn.Item2;
                }
            }
        }
        if(Proteins_moves == null && move.Distance != 0){ 
                Proteins_moves = new(move.Distance, move.moves);}
            else if(Proteins_moves != null && move.Distance != 0){
                Proteins_moves.AddNode(move.Distance, move.moves);}
        move = new(0, new());
        return true;
    }
    public static void Create_HARVESTERS()
    {
        Create_HARVESTER(Moves.Game.myEntitys.HARVESTER_for_a);
        Create_HARVESTER(Moves.Game.myEntitys.HARVESTER_for_b);
        Create_HARVESTER(Moves.Game.myEntitys.HARVESTER_for_c);
        Create_HARVESTER(Moves.Game.myEntitys.HARVESTER_for_d);
        if(Proteins_moves != null)
            Proteins_moves.PrintAllMoves();
        moves.Item2 = null;
        moves.Item1 = 0;
    }
    static byte get_HARVESTE_dir(string key)
    {
        enDirection dir;
        if(Moves.Game.myEntitys.HARVESTER_for_a.ContainsKey(key)){
            dir = Moves.Game.myEntitys.HARVESTER_for_a[key].organDir;
            if(has_HARVESTE(key, dir))
                return 0;
            return (byte)dir;}
        if(Moves.Game.myEntitys.HARVESTER_for_b.ContainsKey(key)){
            dir = Moves.Game.myEntitys.HARVESTER_for_b[key].organDir;
            if(has_HARVESTE(key, dir))
                return 0;
            return (byte)dir;}
        if(Moves.Game.myEntitys.HARVESTER_for_c.ContainsKey(key)){
            dir =Moves.Game.myEntitys.HARVESTER_for_c[key].organDir;
            if(has_HARVESTE(key, dir))
                return 0;
            return (byte)dir;}
        if(Moves.Game.myEntitys.HARVESTER_for_d.ContainsKey(key)){
            dir = Moves.Game.myEntitys.HARVESTER_for_d[key].organDir;
            if(has_HARVESTE(key, dir))
                return 0;
            return (byte)dir;}
        return 0;
    }
    static bool has_HARVESTE(string key, enDirection dir)
    {
        int x = int.Parse(key.Split(' ')[0]);
        int y = int.Parse(key.Split(' ')[1]);
        switch(dir)
        {
            case enDirection.N:
            y--; break;
            case enDirection.S:
            y++; break;
            case enDirection.E:
            x++; break;
            case enDirection.W:
            x--; break;
        }
        if(is_HARVESTE($"{x + 1} {y}"))
            return true;
        if(is_HARVESTE($"{x - 1} {y}"))
            return true;
        if(is_HARVESTE($"{x} {y - 1}"))
            return true;
        if(is_HARVESTE($"{x} {y + 1}"))
            return true;
        return false;
    }
    static bool is_HARVESTE(string key)
    {
        if(Moves.Game.myEntitys.organ.ContainsKey(key))
        {
            if(Moves.Game.myEntitys.organ[key].Type == enType.HARVESTER)
                return true;
        }
        return false;
    }
    static bool Dequeue_moves()
    {
        if(Proteins_moves.queue.Count > 0)
        {
            moves.Item2 = Proteins_moves.queue.Dequeue();
            moves.Item1 = moves.Item2.Count;
            return true;
        }
        return false;
    }
    static byte is_front_enmy(string vertexName)
    {
        int x = int.Parse(vertexName.Split(' ')[0]);
        int y = int.Parse(vertexName.Split(' ')[1]);
        if(Moves.Game.myEntitys.enmy.ContainsKey($"{x + 1} {y}") || Moves.Game.myEntitys.enmy.ContainsKey($"{x + 2} {y}"))
            return (byte)enDirection.E;
        if(Moves.Game.myEntitys.enmy.ContainsKey($"{x - 1} {y}") || Moves.Game.myEntitys.enmy.ContainsKey($"{x - 2} {y}"))
            return (byte)enDirection.W;
        if(Moves.Game.myEntitys.enmy.ContainsKey($"{x} {y - 1}") || Moves.Game.myEntitys.enmy.ContainsKey($"{x} {y - 2}"))
            return (byte)enDirection.N;
        if(Moves.Game.myEntitys.enmy.ContainsKey($"{x} {y + 1}") || Moves.Game.myEntitys.enmy.ContainsKey($"{x} {y + 2}"))
            return (byte)enDirection.S;
        return 0;
    }
    static public bool print_proteins_way()
    {
        GraphMatrix.Check_Wieght = BASIC_moves.Check_Wieght;
        GraphMatrix.Get_Wieght = BASIC_moves.Get_Wieght_Wieght;
        if(moves.Item1 == 0)
        {
            if(!Dequeue_moves())
                return false;
        }
        if(moves.Item1 > 0 && moves.Item2 != null)
        {
            GraphMatrix.Dijkstart_node item;
            while(true)
            {
                if(moves.Item2 == null || moves.Item2.Count == 0)
                {
                    if(!Dequeue_moves())
                        return false;
                }
                item = moves.Item2.Pop();
                if(!Moves.Game.myEntitys.organ.ContainsKey(item.prev_node_Name))
                {
                    moves.Item1 = 0;
                    moves.Item2 = null;
                    continue;
                }
                if(Moves.Game.graph.isEdge(item.prev_node_Name, item.vertexName))
                    break;
            }
            item.wieght.TENTACLE = is_front_enmy(item.vertexName);
            item.wieght.HARVESTER = get_HARVESTE_dir(item.vertexName);
            string str = Moves.use_any_organ(item.wieght);
            if(Moves.Game.myEntitys.organ.ContainsKey(item.prev_node_Name)){
                
                Console.WriteLine($"GROW {Moves.Game.myEntitys.organ[item.prev_node_Name].organId} {item.vertexName} {str}");
                Moves.Game.graph.DeleteInAdge(int.Parse(item.vertexName.Split(' ')[0]), int.Parse(item.vertexName.Split(' ')[1]));
                }
            else
                return false;
            if(moves.Item2.Count == 0)
            {
                moves.Item1 = 0;
                moves.Item2 = null;
            }
            Moves.used_roots.Add(Moves.Game.myEntitys.organ[item.prev_node_Name].organRootId);
            return true;
        }
        return false;
    }
}
public class TENTACLE_moves()
{
    public static bool is_TENTACLE_of_enemy(string vertexName, enDirection dir)
    {
        if(Moves.Game.myEntitys.enmy.ContainsKey(vertexName))
        {
            if(Moves.Game.myEntitys.enmy[vertexName].Type == enType.TENTACLE && Moves.Game.myEntitys.enmy[vertexName].organDir == dir)
                return true;
        }
        return false;
    }
    static public bool kill_enmy()
    {
        string str = "";
        int count = 0;
        foreach(var node in Moves.Game.myEntitys.organ)
        {
            count++;
            if(count == 33)
                count = 33;
            if(Moves.used_roots.Contains(node.Value.organRootId))
                continue;
            if(is_enmy_close(node.Value))
            {
                if(is_posible_to_kill_enmy(node.Value, ref str))
                {
                    Console.WriteLine($"GROW {node.Value.organId} {str}");
                    Moves.used_roots.Add(node.Value.organRootId);
                    return true;
                }
            }
        }
        return false;
    }
    static bool is_in_mape(int x, int y)
    {
        return Moves.Game.Width > x && x >= 0 && 0 <= y && y < Moves.Game.Height;
    }
    static public bool is_posible_to_kill_enmy(Cell? cell, ref string str)
    {
        if(cell == null)
            return false;
        if(!Moves.Game.myEntitys.All.ContainsKey($"{cell.x + 1} {cell.y}"))
        {
            if(is_posible_move($"{cell.x + 1} {cell.y}"))
            {
                str = $"{cell.x + 1} {cell.y} TENTACLE W";
                return true;
            }
        }
        if(!Moves.Game.myEntitys.All.ContainsKey($"{cell.x - 1} {cell.y}"))
        {
            if(is_posible_move($"{cell.x - 1} {cell.y}"))
            {
                str = $"{cell.x - 1} {cell.y} TENTACLE E";
                return true;
            }
        }
        if(!Moves.Game.myEntitys.All.ContainsKey($"{cell.x} {cell.y + 1}"))
        {
            if(is_posible_move($"{cell.x} {cell.y + 1}"))
            {
                str = $"{cell.x} {cell.y + 1} TENTACLE S";
                return true;
            }
        }
        if(!Moves.Game.myEntitys.All.ContainsKey($"{cell.x} {cell.y - 1}"))
        {
            if(is_posible_move($"{cell.x} {cell.y - 1}"))
            {
                str = $"{cell.x} {cell.y - 1} TENTACLE N";
                return true;
            }
        }
        return false;
    }
    static public Cell? Get_enmy_cell(Cell cell)
    {
        if(Moves.Game.myEntitys.enmy.ContainsKey($"{cell.x + 1} {cell.y}"))
            return Moves.Game.myEntitys.enmy[$"{cell.x + 1} {cell.y}"];
        if(Moves.Game.myEntitys.enmy.ContainsKey($"{cell.x - 1} {cell.y}"))
            return Moves.Game.myEntitys.enmy[$"{cell.x - 1} {cell.y}"];
        if(Moves.Game.myEntitys.enmy.ContainsKey($"{cell.x} {cell.y - 1}"))
            return Moves.Game.myEntitys.enmy[$"{cell.x} {cell.y - 1}"];
        if(Moves.Game.myEntitys.enmy.ContainsKey($"{cell.x} {cell.y + 1}"))
            return Moves.Game.myEntitys.enmy[$"{cell.x} {cell.y + 1}"];
        return null;
    }
    static public bool is_enmy_close(Cell? cell)
    {
        if(cell == null)
            return false;
        if(Moves.Game.myEntitys.enmy.ContainsKey($"{cell.x + 1} {cell.y}"))
            return true;
        if(Moves.Game.myEntitys.enmy.ContainsKey($"{cell.x - 1} {cell.y}"))
            return true;
        if(Moves.Game.myEntitys.enmy.ContainsKey($"{cell.x} {cell.y - 1}"))
            return true;
        if(Moves.Game.myEntitys.enmy.ContainsKey($"{cell.x} {cell.y + 1}"))
            return true;
        return false;
    }
    public static bool is_TENTACLE_of_enemy_in_direction(string vertexName)
    {
        int x = int.Parse(vertexName.Split(' ')[0]);
        int y = int.Parse(vertexName.Split(' ')[1]);
        if(is_TENTACLE_of_enemy($"{x + 1} {y}", enDirection.W))
            return true;
        if(is_TENTACLE_of_enemy($"{x - 1} {y}", enDirection.E))
            return true;
        if(is_TENTACLE_of_enemy($"{x} {y + 1}", enDirection.N))
            return true;
        if(is_TENTACLE_of_enemy($"{x} {y - 1}", enDirection.S))
            return true;
        return false;
    }
    static public bool any_move()
    {
        foreach(var item in Moves.Game.myEntitys.organ)
        {
            if(Moves.used_roots.Contains(item.Value.organRootId))
                continue;
            string vertexName = "";
            byte dir = is_front_enmy(item.Key, ref vertexName);
            
            if(!is_posible_move(vertexName, is_my_TENTACLE_dir(item.Key, item.Value.organDir, vertexName)))
                continue;
            if(BASIC_moves.is_TENTACLE_of_enemy_in_direction(item.Key))
                continue;
            if(dir != 0)
            {
                Wieght W = new();
                W.TENTACLE = dir;
                string str = Moves.use_any_organ(W);
                Moves.used_roots.Add(item.Value.organRootId);
                Console.WriteLine($"GROW {item.Value.organId} {vertexName} {str}");
                return true;
            }
        }
        return false;
    }
    static bool is_my_TENTACLE_dir(string start, enDirection dir, string dst)
    {
        int x = int.Parse(start.Split(' ')[0]);
        int y = int.Parse(start.Split(' ')[1]);
        if(dir == enDirection.E)
            return dst == $"{x + 1} {y}";
        if(dir == enDirection.N)
            return dst == $"{x} {y - 1}";
        if(dir == enDirection.S)
            return dst == $"{x} {y + 1}";
        if(dir == enDirection.W)
            return dst == $"{x - 1} {y}";
        return false;
    }
    static bool is_posible_move(string vertexName, bool is_my_TENTACLE = false)
    {
        int x;
        int y;
        string []str = vertexName.Split(' ');
        if(str.Length != 2)
            return false;
        if(!int.TryParse(str[0], out x) || !int.TryParse(str[1], out y))
            return false;
        if(!is_in_mape(x, y))
            return false;
        if(is_my_TENTACLE && !is_TENTACLE_of_enemy_in_direction(vertexName))
            return true;
        if(Moves.Game.myEntitys.All.ContainsKey(vertexName) || is_TENTACLE_of_enemy_in_direction(vertexName))
            return false;
        return true;
    }
    static byte is_front_enmy(string vertexName, ref string result)
    {
        int x = int.Parse(vertexName.Split(' ')[0]);
        int y = int.Parse(vertexName.Split(' ')[1]);
        result = $"{x + 1} {y}";
        if(is_posible_move(result) && (Moves.Game.myEntitys.enmy.ContainsKey($"{x + 2} {y}") || Moves.Game.myEntitys.enmy.ContainsKey($"{x + 3} {y}")))
            return (byte)enDirection.E;
        result = $"{x - 1} {y}";
        if(is_posible_move(result) &&  (Moves.Game.myEntitys.enmy.ContainsKey($"{x - 2} {y}") || Moves.Game.myEntitys.enmy.ContainsKey($"{x - 3} {y}")))
            return (byte)enDirection.W;
        result = $"{x} {y - 1}";
        if(is_posible_move(result) && (Moves.Game.myEntitys.enmy.ContainsKey($"{x} {y - 2}") || Moves.Game.myEntitys.enmy.ContainsKey($"{x} {y - 3}")))
            return (byte)enDirection.N;
        result = $"{x} {y + 1}";
        if(is_posible_move(result) && (Moves.Game.myEntitys.enmy.ContainsKey($"{x} {y + 2}") || Moves.Game.myEntitys.enmy.ContainsKey($"{x} {y + 3}")))
            return (byte)enDirection.S;
        result = $"{x + 1} {y + 1}";
        if(Moves.Game.myEntitys.enmy.ContainsKey(result))
        {
            result = $"{x} {y + 1}";
            if(is_posible_move(result))
                return (byte)enDirection.E;
            result = $"{x + 1} {y}";
            if(is_posible_move(result))
                return (byte)enDirection.S;
        }
        result = $"{x + 1} {y - 1}";
        if(Moves.Game.myEntitys.enmy.ContainsKey(result))
        {
            result = $"{x} {y - 1}";
            if(is_posible_move(result))
                return (byte)enDirection.E;
            result = $"{x + 1} {y}";
            if(is_posible_move(result))
                return (byte)enDirection.N;
        }
        result = $"{x - 1} {y - 1}";
        if(Moves.Game.myEntitys.enmy.ContainsKey(result))
        {
            result = $"{x} {y - 1}";
            if(is_posible_move(result))
                return (byte)enDirection.W;
            result = $"{x - 1} {y}";
            if(is_posible_move(result))
                return (byte)enDirection.N;
        }
        result = $"{x - 1} {y + 1}";
        if(Moves.Game.myEntitys.enmy.ContainsKey(result))
        {
            result = $"{x} {y + 1}";
            if(is_posible_move(result))
                return (byte)enDirection.W;
            result = $"{x - 1} {y}";
            if(is_posible_move(result))
                return (byte)enDirection.S;
        }
        return 0;
    }
}
public class Proteins
{
    static int a;
    static int b;
    static int c;
    static int d;
    static public int A 
    { 
        get
        {
            int count = 0;
            foreach(var node in Moves.Game.myEntitys.A)
            {
                if(Has_my_HARVESTER(node.Value))
                    count++;
            } 
            return a + count; 
        } 
        set
        {
            a = value;
        }
    }
    static public int B
    { 
        get
        {
            int count = 0;
            foreach(var node in Moves.Game.myEntitys.B)
            {
                if(Has_my_HARVESTER(node.Value))
                    count++;
            } 
            return b + count; 
        } 
        set
        {
            b = value;
        }
    }
    static public int C{ 
        get
        {
            int count = 0;
            foreach(var node in Moves.Game.myEntitys.C)
            {
                if(Has_my_HARVESTER(node.Value))
                count++;
            } 
            return c + count; 
        } 
        set
        {
            c = value;
        }
    }
    static public int D
    { 
        get
        {
            int count = 0;
            foreach(var node in Moves.Game.myEntitys.D)
            {
                if(Has_my_HARVESTER(node.Value))
                count++;
            } 
            return d + count; 
        } 
        set
        {
            d = value;
        }
    }
    static public void set_stock(int a, int b, int c, int d)
    {
        A = a;
        B = b;
        C = c;
        D = d;
    }
    static public bool BASIC
    {
        get {return A > 0;}
    }
    static public bool HARVESTER
    {
        get {return C > 0 && D > 0;}
    }
    static public bool TENTACLE
    {
        get {return B > 0 && C > 0;}
    }
    static public bool ROOT
    {
        get {return A > 0 && B > 0 && C > 0 && D > 0;}
    }
    static public bool SPORE
    {
        get {return B > 1 && D > 1 && ROOT;}
    }
    static public bool SPORE_alown
    {
        get {return B > 0 && D > 0;}
    }
    static public void BASIC_created()
    {
        a--;
    }
    static public void HARVESTER_created()
    {
        c--;
        d--;
    }
    static public void TENTACLE_created()
    {
        b--;
        c--;
    }
    static public void ROOT_created()
    {
        a--;
        b--;
        c--;
        d--;
    }
    static public void SPORE_created()
    {
        b--;
        d--;
    }
    public static bool is_porotein(Cell? node)
    {
        if(node == null)
            return false;
        switch(node.Type)
        {
            case enType.A:
            case enType.B:
            case enType.C:
            case enType.D:
                return true;
        }
        return false; 
    }
    public static bool Has_any_HARVESTER(int x, int y)
    {
        if(Moves.Game.myEntitys.HARVESTER.ContainsKey($"{x + 1} {y}") && Moves.Game.myEntitys.HARVESTER[$"{x + 1} {y}"].organDir == enDirection.W)
            return true;
        if(Moves.Game.myEntitys.HARVESTER.ContainsKey($"{x - 1} {y}") && Moves.Game.myEntitys.HARVESTER[$"{x - 1} {y}"].organDir == enDirection.E)
            return true;
        if(Moves.Game.myEntitys.HARVESTER.ContainsKey($"{x} {y + 1}") && Moves.Game.myEntitys.HARVESTER[$"{x} {y + 1}"].organDir == enDirection.N)
            return true;
        if(Moves.Game.myEntitys.HARVESTER.ContainsKey($"{x} {y - 1}") && Moves.Game.myEntitys.HARVESTER[$"{x} {y - 1}"].organDir == enDirection.S)
            return true;
        return false;
    }
    public static bool Has_my_HARVESTER(Cell node)
    {
        return false;
    }
    public static void bigest_organ_stock(ref Wieght wieght)
    {
        int basic = A;
        int HARVESTER = C > D ? D : C; 
        int TENTACLE = C > B ? B : C; 
        int SPORE = D > B ? B : D; 
        if(basic >= HARVESTER && basic >= TENTACLE && basic >= SPORE)
            wieght.BASIC = 1;
        else if(TENTACLE >= basic && TENTACLE >= HARVESTER && TENTACLE >= SPORE)
            wieght.TENTACLE = (byte)'S';
        else if(HARVESTER >= basic && HARVESTER >= TENTACLE && HARVESTER >= SPORE)
            wieght.HARVESTER = (byte)'S';
        else if(SPORE > 0)
            wieght.SPORE_ROOT = (byte)'S';
    }
    public static bool has_stock()
    {
        return BASIC || HARVESTER || TENTACLE || SPORE_alown;
    }
}
public class GraphMatrix
{
    int _max_x, _max_y;
    public enum enGraphDirectionType {Directed, unDirected}
    Wieght [,] _adjacencyMatrix;
    Dictionary<string, int> _vertexDictionary;
    Dictionary<int, string> _vertexName;
    public int numberOfVertices;
    enGraphDirectionType _GraphDirectionType = enGraphDirectionType.unDirected;
    public GraphMatrix(int max_x, int max_y)
    {
        _max_x = max_x;
        _max_y = max_y;
        numberOfVertices = max_x * max_y;
        _adjacencyMatrix = new Wieght[numberOfVertices,numberOfVertices];
        _vertexDictionary = new();
        _vertexName = new();
        setAdges();
    }
    void setAdges()
    {
        int num = 0;
        for(int i = 0; i < _max_y; i++)
        {
            for(int j = 0; j < _max_x; j++)
            {
                _vertexDictionary.Add($"{j} {i}", num);
                _vertexName.Add(num, $"{j} {i}");
                Add_SPORE_ROOT_edge(j, i);
                Add_BASIC_Adge(j, i);
                num++;
            }
        }
    }
    void Add_BASIC_Adge(int x, int y)
    {
        Wieght weight = new();
        weight.BASIC = 1;
        string src = $"{x} {y}";
        string dst = "";
        if(x + 1 < _max_x)
        {
            dst = $"{x + 1} {y}";
            AddEdge(src, dst, weight);
        }
        if(x - 1 >= 0)
        {
            dst = $"{x - 1} {y}";
            AddEdge(src, dst, weight);
        }
        if(y + 1 < _max_y)
        {
            dst = $"{x} {y + 1}";
            AddEdge(src, dst, weight);
        }
        if(y - 1 >= 0)
        {
            dst = $"{x} {y - 1}";
            AddEdge(src, dst, weight);
        }
    }
    void Add_SPORE_ROOT_edge(int x, int y)
    {
        Wieght weight = new();
        weight.SPORE_ROOT = 1;
        weight.SPORE = (byte)enDirection.W;
        for(int i = 0; i < _max_x; i++)
        {
            if(i == x || i == x - 1 || i == x - 2 || i == x + 1 || i == x + 2){
                weight.SPORE = (byte)enDirection.E;
                continue;}
            AddEdge($"{x} {y}", $"{i} {y}", weight);
        }
        weight.SPORE = (byte)enDirection.N;
        for(int i = 0; i < _max_y; i++)
        {
            if(i == y || i == y - 1 || i == y - 2 || i == y + 1 || i == y + 2){
                weight.SPORE = (byte)enDirection.S;
                continue;}
            AddEdge($"{x} {y}", $"{x} {i}", weight);
        }
    }
    void Delete_SPORE_ROOT_edge(int x, int y)
    {
        Wieght weight = new();
        for(int i = 0; i < x; i++)
        {
            for(int j = x + 1; j < _max_x; j++)
                AddEdge($"{i} {y}", $"{j} {y}" , weight);
        }
        for(int i = 0; i < y; i++)
        {
            for(int j = y + 1; j < _max_y; j++)
                AddEdge($"{x} {i}", $"{x} {j}" , weight);
        }
    }
    public void AddHARVESTERAdge(int x, int y)
    {
        //DeleteAdge(x, y);
        if(!_vertexDictionary.ContainsKey($"{x} {y}"))
            return;
        int targetIndex = _vertexDictionary[$"{x} {y}"];
        if(_vertexDictionary.ContainsKey($"{x - 1} {y}") && isEdge($"{x - 1} {y}", $"{x} {y}")){
            create_HARVESTE_Cell(x - 1, y, enDirection.E, Moves.Game.myEntitys.p[$"{x} {y}"]);
            SetHARVESTERAdge(_vertexDictionary[$"{x - 1} {y}"], targetIndex, (byte)enDirection.E);}

        if(_vertexDictionary.ContainsKey($"{x + 1} {y}" ) && isEdge($"{x + 1} {y}", $"{x} {y}")){
            create_HARVESTE_Cell(x + 1, y, enDirection.W, Moves.Game.myEntitys.p[$"{x} {y}"]);
            SetHARVESTERAdge(_vertexDictionary[$"{x + 1} {y}"], targetIndex, (byte)enDirection.W);}

        if(_vertexDictionary.ContainsKey($"{x} {y - 1}") && isEdge($"{x} {y - 1}", $"{x} {y}")){
            create_HARVESTE_Cell(x, y - 1, enDirection.S, Moves.Game.myEntitys.p[$"{x} {y}"]);
            SetHARVESTERAdge(_vertexDictionary[$"{x} {y - 1}"], targetIndex, (byte)enDirection.S);}

        if(_vertexDictionary.ContainsKey($"{x} {y + 1}") && isEdge($"{x} {y + 1}", $"{x} {y}")){
            create_HARVESTE_Cell(x, y + 1, enDirection.N, Moves.Game.myEntitys.p[$"{x} {y}"]);
            SetHARVESTERAdge(_vertexDictionary[$"{x} {y + 1}"], targetIndex, (byte)enDirection.N);}
    }
    void create_HARVESTE_Cell(int x, int y, enDirection dir, Cell c)
    {
        switch(c.Type)
        {
            case enType.A:
                Moves.Game.myEntitys.HARVESTER_for_a[$"{x} {y}"] = new(x, y);
                Moves.Game.myEntitys.HARVESTER_for_a[$"{x} {y}"].organDir = dir;
                break;
            case enType.B:
                Moves.Game.myEntitys.HARVESTER_for_b[$"{x} {y}"] = new(x, y);
                Moves.Game.myEntitys.HARVESTER_for_b[$"{x} {y}"].organDir = dir;
                break;
            case enType.C:
                Moves.Game.myEntitys.HARVESTER_for_c[$"{x} {y}"] = new(x, y);
                Moves.Game.myEntitys.HARVESTER_for_c[$"{x} {y}"].organDir = dir;
                break;
            case enType.D:
                Moves.Game.myEntitys.HARVESTER_for_d[$"{x} {y}"] = new(x, y);
                Moves.Game.myEntitys.HARVESTER_for_d[$"{x} {y}"].organDir = dir;
                break;
        }
    }
    void SetHARVESTERAdge(int sourceIndex, int targetIndex, byte dir)
    {
        Wieght weight = new();
        weight.HARVESTER = dir;
        ChangeAdge(sourceIndex, targetIndex, weight);
    }
    void ChangeAdge(int sourceIndex, int targetIndex, Wieght weight)
    {
        _adjacencyMatrix[sourceIndex, targetIndex] = weight;
    }
    public void DeleteAdge(int x, int y)
    {
        int sourceIndex = _vertexDictionary[$"{x} {y}"];
        for(int i = 0; i < numberOfVertices; i++)
        {
            _adjacencyMatrix[sourceIndex, i] = new();
            if(_GraphDirectionType == enGraphDirectionType.unDirected)
                _adjacencyMatrix[i, sourceIndex] = new();
        }
        Delete_SPORE_ROOT_edge(x, y);
    }
    public void DeleteAdge(string name)
    {
        int x = int.Parse(name.Split(' ')[0]);
        int y = int.Parse(name.Split(' ')[0]);
        DeleteAdge(x, y);
    }
    public void DeleteInAdge(int x, int y)
    {
        Wieght weight = new();
        int sourceIndex = _vertexDictionary[$"{x} {y}"];
        for(int i = 0; i < numberOfVertices; i++)
            _adjacencyMatrix[i, sourceIndex] = weight;
        Delete_SPORE_ROOT_edge(x, y);
    }
    public void AddEdge(string source, string destination, Wieght weight)
    {
        if(_vertexDictionary.ContainsKey(source) && _vertexDictionary.ContainsKey(destination))
        {
            int sourceIndex = _vertexDictionary[source];
            int destinationIndex = _vertexDictionary[destination];
            _adjacencyMatrix[sourceIndex, destinationIndex] = weight;
            if(_GraphDirectionType == enGraphDirectionType.unDirected)
                _adjacencyMatrix[destinationIndex, sourceIndex] = weight;
        }
    }
    public bool isEdge(string source, string destination)
    {
        if(_vertexDictionary.ContainsKey(source) && _vertexDictionary.ContainsKey(destination))
        {
            int srcIndex = _vertexDictionary[source];
            int dstIndex = _vertexDictionary[destination];

            return GetWieght(_adjacencyMatrix[srcIndex,dstIndex]) > 0;
        }
        return false;
    }
    public int GetInDegree(string vertex)
    {
        int degree = 0;
        if(_vertexDictionary.ContainsKey(vertex))
        {
            int vertexIndex = _vertexDictionary[vertex];
            for(int i = 0; i < numberOfVertices; i++)
            {
                if(GetWieght(_adjacencyMatrix[i, vertexIndex]) > 0)
                    degree++;
            }
        }
        return degree;
    }
    public int GetOutDegree(string vertex)
    {
        int degree = 0;
        if(_vertexDictionary.ContainsKey(vertex))
        {
            int vertexIndex = _vertexDictionary[vertex];
            for(int i = 0; i < numberOfVertices; i++)
            {
                if(GetWieght(_adjacencyMatrix[vertexIndex, i]) > 0)
                    degree++;
            }
        }
        return degree;
    }
    public struct Dijkstart_node
    {
        public string vertexName;
        public int vertexIndex;
        public byte distance;
        public byte Actule_distance;
        public int prev_node_index;
        public string prev_node_Name;
        public Wieght wieght;
    }
    public Dictionary<int, Dijkstart_node>? Dijkstart(string StratVertex)
    {
        if(!_vertexDictionary.ContainsKey(StratVertex))
            return null;
        Dictionary<int, Dijkstart_node> Dijkstart_table = new();
        Dijkstart_table.Add(_vertexDictionary[StratVertex], create_Dijkstart_node(0, -1, StratVertex, _vertexDictionary[StratVertex], new()));
        bool []visited = new bool[numberOfVertices];
        for(int count = 0; count < numberOfVertices - 1; count++)
        {
            int minIndex = GetminiIndex(visited, Dijkstart_table);
            if(minIndex < 0)
                continue;
            visited[minIndex] = true;
            for(int i = 0; i < numberOfVertices; i++)
            {
                if(i == 56)
                    i = 56;
                if(!visited[i] && CheckWieght(_adjacencyMatrix[minIndex, i]))
                {
                    if(!Dijkstart_table.ContainsKey(i))
                        Dijkstart_table.Add(i, create_Dijkstart_node((byte)(GetWieght(_adjacencyMatrix[minIndex, i]) + Dijkstart_table[minIndex].distance), minIndex, GetVertexName(i), i, _adjacencyMatrix[minIndex, i]));
                    else if(Dijkstart_table[i].distance > GetWieght(_adjacencyMatrix[minIndex, i]) + Dijkstart_table[minIndex].distance)
                        Dijkstart_table[i] = create_Dijkstart_node((byte)(GetWieght(_adjacencyMatrix[minIndex, i]) + Dijkstart_table[minIndex].distance), minIndex, GetVertexName(i), i, _adjacencyMatrix[minIndex, i]);
                }
            }
        }
        return Dijkstart_table;
    }
    int Calculate_Actule_distance(string vertexName, string prev_node_Name)
    {
        return Calculate_Actule_x(vertexName, prev_node_Name) + Calculate_Actule_y(vertexName, prev_node_Name);
    } 
    int Calculate_Actule_x(string vertexName, string prev_node_Name)
    {
        if(prev_node_Name == "")
            return 0;
        int x = 0;
        if(!int.TryParse(vertexName.Split(' ')[0], out x))
            return 0;
        int prev_x = 0;
        if(!int.TryParse(prev_node_Name.Split(' ')[0], out prev_x))
            return 0;
        return x > prev_x ? x - prev_x :  prev_x - x;
    } 
    int Calculate_Actule_y(string vertexName, string prev_node_Name)
    {
        if(prev_node_Name == "")
            return 0;
        int y = 0;
        if(!int.TryParse(vertexName.Split(' ')[1], out y))
            return 0;
        int prev_y = 0;
        if(!int.TryParse(prev_node_Name.Split(' ')[1], out prev_y))
            return 0;
        return y > prev_y ? y - prev_y :  prev_y - y;
    } 
    public MovesBinaryTree Get_Table_for_SPORE(string root)
    {
        var result = Dijkstart(root);
        int most_x_dist = 0;
        int most_y_dist = 0;
        Dijkstart_node most_x = new();
        Dijkstart_node most_y = new();
        Stack<Dijkstart_node> stack = new();
        MovesBinaryTree moves;
        var current_node = most_x;
        foreach(var item in result)
        {
            bool is_not_valid = false;
            current_node = item.Value;
            while(true)
            {
                if(current_node.vertexName == root || current_node.vertexName == null)
                {
                    is_not_valid = true;
                    break;
                }
                current_node = result[current_node.prev_node_index];
                if(current_node.vertexName == root)
                    break;
                if(current_node.Actule_distance > 1)
                {
                    is_not_valid = true;
                    break;
                }
            }
            if(is_not_valid || is_front_of_enamy(item.Value.vertexName))
                continue;
            int dst = Calculate_Actule_x(item.Value.vertexName, item.Value.prev_node_Name);
            if(dst > most_x.Actule_distance && item.Value.distance < 6)
                most_x = item.Value;
            dst = Calculate_Actule_y(item.Value.vertexName, item.Value.prev_node_Name);
            if(dst > most_y.Actule_distance && item.Value.distance < 6)
                most_y = item.Value;
        }
        current_node = most_x;
        while(true)
        {
            if(current_node.vertexName == root || current_node.vertexName == null)
                break;
            stack.Push(current_node);
            Delete_Proteien(current_node.vertexName);
            current_node = result[current_node.prev_node_index];
        }
        moves = new(stack.Count, stack);
        stack = new();
        current_node = most_y;
        while(true)
        {
            if(current_node.vertexName == root || current_node.vertexName == null)
                break;
            stack.Push(current_node);
            Delete_Proteien(current_node.vertexName);
            current_node = result[current_node.prev_node_index];
        }
        moves.AddNode(stack.Count, stack);
        return moves;
    }
    bool is_front_of_enamy(string vertexName)
    {
        int x;
        int y;
        string []str =  vertexName.Split(' ');
        if(str.Length < 2)
            return false;
        if(!int.TryParse(str[0], out x))
            return false;
        if(!int.TryParse(str[1], out y))
            return false;
        for(int i = -2; i < 3; i++)
        {
            for(int j = -2; j < 3; j++)
            {
                if(Moves.Game.myEntitys.enmy.ContainsKey($"{x + i} {y + j}"))
                    return true;
            }
        }
        return false;
    }
    void Delete_Proteien(string key)
    {
        if(key == null)
            return;
        if(Moves.Game.myEntitys.p.ContainsKey(key))
            Moves.Game.myEntitys.p.Remove(key);
        if(Moves.Game.myEntitys.A.ContainsKey(key))
            Moves.Game.myEntitys.A.Remove(key);
        if(Moves.Game.myEntitys.B.ContainsKey(key))
            Moves.Game.myEntitys.B.Remove(key);
        if(Moves.Game.myEntitys.C.ContainsKey(key))
            Moves.Game.myEntitys.C.Remove(key);
        if(Moves.Game.myEntitys.D.ContainsKey(key))
            Moves.Game.myEntitys.D.Remove(key);
    }
    bool CheckWieght(Wieght wieght)
    {
        bool ?result = Check_Wieght?.Invoke(wieght);
        return result != null && result.Value;
    }
    int GetWieght(Wieght wieght)
    {
        int ?r = Get_Wieght?.Invoke(wieght);
        if (r != null)
            return r.Value;
        return 0;
    }
    public (int, Stack<Dijkstart_node>?) GetPath(string start, string end)
    {
        Dictionary<int, Dijkstart_node>? result = Dijkstart(start);
        Stack<Dijkstart_node> stack = new();
        if(result == null)
            return (0, null);
        if(!result.ContainsKey(_vertexDictionary[end]))
            return (0, null);
        var current_node = result[_vertexDictionary[end]];
        int distance;
        while(true)
        {
            if(current_node.vertexName == start)
                break;
            stack.Push(current_node);
            current_node = result[current_node.prev_node_index];
        }
        distance = stack.Count;
        return (distance, stack);
    }
    public Dijkstart_node? Getfirst_move(string start)
    {
        if(!_vertexDictionary.ContainsKey(start))
            return null;
        Wieght wieght = new();
        wieght.BASIC = 1;
        int x = int.Parse(start.Split(' ')[0]);
        int y = int.Parse(start.Split(' ')[1]);
        if(isEdge(start, $"{x + 1} {y}"))
            return create_Dijkstart_node(1, _vertexDictionary[$"{x} {y}"], $"{x + 1} {y}", _vertexDictionary[$"{x + 1} {y}"], wieght);
        if(isEdge(start, $"{x - 1} {y}"))
        return create_Dijkstart_node(1, _vertexDictionary[$"{x} {y}"], $"{x - 1} {y}", _vertexDictionary[$"{x - 1} {y}"], wieght);
        if(isEdge(start, $"{x} {y + 1}"))
        return create_Dijkstart_node(1, _vertexDictionary[$"{x} {y}"], $"{x} {y + 1}", _vertexDictionary[$"{x} {y + 1}"], wieght);
        if(isEdge(start, $"{x} {y - 1}"))
        return create_Dijkstart_node(1, _vertexDictionary[$"{x} {y}"], $"{x} {y - 1}", _vertexDictionary[$"{x} {y - 1}"], wieght);
        return null;
    }
    public int GetminiIndex(bool []visited, Dictionary<int ,Dijkstart_node> Dijkstart_table)
    {
        int minDistance = int.MaxValue;
        int miniIndex = -1;
        foreach(var node in Dijkstart_table)
        {
            if(!visited[node.Key] && minDistance > node.Value.distance)
            {
                minDistance = node.Value.distance;
                miniIndex = node.Value.vertexIndex;
            }
        }
        return miniIndex;
    }
    Dijkstart_node create_Dijkstart_node(byte distance, int prev_node_index, string vertexName, int vertexIndex, Wieght wieght)
    {
        Dijkstart_node node = new();
        node.distance = distance;
        node.prev_node_index = prev_node_index;
        node.vertexName = vertexName;
        node.vertexIndex = vertexIndex;
        node.wieght = wieght;
        node.prev_node_Name = GetVertexName(prev_node_index);
        node.Actule_distance = (byte)Calculate_Actule_distance(vertexName, node.prev_node_Name);
        return node;
    }
    string GetVertexName(int vertexIndex)
    {
        if(_vertexName.ContainsKey(vertexIndex))
            return _vertexName[vertexIndex];
        return "";
    }
    static public Predicate<Wieght>? Check_Wieght;
    static public Func<Wieght, int>? Get_Wieght;
    public void PrintGraph()
    {
        foreach(var vertex in _vertexDictionary)
            Console.Write($"\t{vertex.Key}");
        Console.WriteLine();
        int i = 0;
        foreach(var vertex in _vertexDictionary)
        {
            Console.Write($"{vertex.Key}");
            for(int j = 0; j < numberOfVertices; j++)
                Console.Write($"\t{_adjacencyMatrix[i, j].BASIC},{_adjacencyMatrix[i, j].SPORE_ROOT}");
            Console.WriteLine();
            i++;
        }
    }
}


class Program
{
    static public void Set_cell_data(game_grid game, string data)
    {
        foreach(string node_data in data.Split('\n'))
            game.set_data(node_data);
    }
    
    static void  Main(string[] args)
    {
        var watch = System.Diagnostics.Stopwatch.StartNew();
        // the code that you want to measure comes here

        //Console.Clear();
        
        /*GraphMatrix graph = new GraphMatrix(5, 5);
        graph.DeleteAdge(2, 2);
        graph.DeleteAdge(2, 0);
        graph.DeleteAdge(1, 1);
        graph.GetPath("2 1", "2 3");
        //graph.PrintGraph();
        //graph.BFS_print_longer_distance("1 1", enWieghtType.BASIC);
        
        */

        //2 , 3, 6, 3
        game_grid game = new game_grid(20, 10);
        //Set_cell_data(game, "5 3 SPORER 1 6 S 1 1\n7 3 A 1 4 S 1 1\n6 3 SPORER 1 6 S 1 1\n6 0 WALL -1 0 X 0 0\n10 0 WALL -1 0 X 0 0\n15 0 WALL -1 0 X 0 0\n0 1 A -1 0 X 0 0\n6 1 B -1 0 X 0 0\n7 1 C -1 0 X 0 0\n9 1 C -1 0 X 0 0\n10 1 WALL -1 0 X 0 0\n14 1 B -1 0 X 0 0\n2 2 WALL -1 0 X 0 0\n3 2 WALL -1 0 X 0 0\n6 2 WALL -1 0 X 0 0\n7 2 WALL -1 0 X 0 0\n14 2 A -1 0 X 0 0\n2 3 WALL -1 0 X 0 0\n3 3 ROOT 1 1 N 0 1\n4 3 D -1 0 X 0 0\n6 3 D -1 0 X 0 0\n8 3 WALL -1 0 X 0 0\n7 4 WALL -1 0 X 0 0\n9 4 D -1 0 X 0 0\n10 4 WALL -1 0 X 0 0\n11 4 D -1 0 X 0 0\n12 4 ROOT 0 2 N 0 2\n13 4 WALL -1 0 X 0 0\n1 5 A -1 0 X 0 0\n8 5 WALL -1 0 X 0 0\n9 5 WALL -1 0 X 0 0\n12 5 WALL -1 0 X 0 0\n13 5 WALL -1 0 X 0 0\n1 6 B -1 0 X 0 0\n5 6 WALL -1 0 X 0 0\n6 6 C -1 0 X 0 0\n8 6 C -1 0 X 0 0\n9 6 B -1 0 X 0 0\n15 6 A -1 0 X 0 0\n0 7 WALL -1 0 X 0 0\n5 7 WALL -1 0 X 0 0\n9 7 WALL -1 0 X 0 0\n12 7 WALL -1 0 X 0 0");
        Set_cell_data(game, "0 0 WALL -1 0 X 0 0\n1 0 WALL -1 0 X 0 0\n9 0 WALL -1 0 X 0 0\n10 0 WALL -1 0 X 0 0\n12 0 WALL -1 0 X 0 0\n15 0 WALL -1 0 X 0 0\n17 0 B -1 0 X 0 0\n19 0 WALL -1 0 X 0 0\n0 1 ROOT 0 1 N 0 1\n1 1 WALL -1 0 X 0 0\n2 1 WALL -1 0 X 0 0\n4 1 WALL -1 0 X 0 0\n14 1 C -1 0 X 0 0\n7 2 WALL -1 0 X 0 0\n8 2 B -1 0 X 0 0\n9 2 WALL -1 0 X 0 0\n10 2 WALL -1 0 X 0 0\n16 2 WALL -1 0 X 0 0\n17 2 A -1 0 X 0 0\n4 3 WALL -1 0 X 0 0\n5 3 WALL -1 0 X 0 0\n6 3 WALL -1 0 X 0 0\n12 3 WALL -1 0 X 0 0\n13 3 WALL -1 0 X 0 0\n14 3 WALL -1 0 X 0 0\n15 3 WALL -1 0 X 0 0\n19 3 C -1 0 X 0 0\n0 4 WALL -1 0 X 0 0\n4 4 D -1 0 X 0 0\n5 4 WALL -1 0 X 0 0\n6 4 A -1 0 X 0 0\n10 4 WALL -1 0 X 0 0\n13 4 WALL -1 0 X 0 0\n14 4 D -1 0 X 0 0\n17 4 WALL -1 0 X 0 0\n18 4 WALL -1 0 X 0 0\n19 4 WALL -1 0 X 0 0\n0 5 WALL -1 0 X 0 0\n1 5 WALL -1 0 X 0 0\n2 5 WALL -1 0 X 0 0\n5 5 D -1 0 X 0 0\n6 5 WALL -1 0 X 0 0\n9 5 WALL -1 0 X 0 0\n13 5 A -1 0 X 0 0\n14 5 WALL -1 0 X 0 0\n15 5 D -1 0 X 0 0\n19 5 WALL -1 0 X 0 0\n0 6 C -1 0 X 0 0\n4 6 WALL -1 0 X 0 0\n5 6 WALL -1 0 X 0 0\n6 6 WALL -1 0 X 0 0\n7 6 WALL -1 0 X 0 0\n13 6 WALL -1 0 X 0 0\n14 6 WALL -1 0 X 0 0\n15 6 WALL -1 0 X 0 0\n2 7 A -1 0 X 0 0\n3 7 WALL -1 0 X 0 0\n9 7 WALL -1 0 X 0 0\n10 7 WALL -1 0 X 0 0\n11 7 B -1 0 X 0 0\n12 7 WALL -1 0 X 0 0\n5 8 C -1 0 X 0 0\n15 8 WALL -1 0 X 0 0\n17 8 WALL -1 0 X 0 0\n18 8 WALL -1 0 X 0 0\n19 8 ROOT 1 2 N 0 2\n0 9 WALL -1 0 X 0 0\n2 9 B -1 0 X 0 0\n4 9 WALL -1 0 X 0 0\n7 9 WALL -1 0 X 0 0\n9 9 WALL -1 0 X 0 0\n10 9 WALL -1 0 X 0 0\n18 9 WALL -1 0 X 0 0\n19 9 WALL -1 0 X 0 0");
        Proteins.set_stock(3 , 6, 7, 7);
        Moves m = new Moves(game);
        m.set_up_moves();
        //string str = "2 2 BASIC 1 3 X 1 1\n2 3 SPORER 1 5 S 1 1\n2 6 ROOT 1 7 N 0 7\n3 6 SPORER 1 9 E 0 7\n6 6 ROOT 1 11 N 0 11\n2 5 BASIC 1 15 X 1 7\n6 7 SPORER 1 8 S 0 1";
        
        m.Find_Move();
        m.Find_Move();
        //m.Find_Move();
        //m.Find_Move();
        /*for(int i = 0; i < str.Split('\n').Length; i++)
        {
            int routs_num = Moves.Game.myEntitys.roots.Count;
            m.Find_Move();
            game.set_data(str.Split('\n')[i]);
            if(Moves.count == routs_num)
            {
                Moves.count = 0;
                Moves.used_roots.Clear();
            }
        }*/
        watch.Stop();
        var elapsedMs = watch.ElapsedMilliseconds;
        Console.WriteLine(elapsedMs);
        //Console.WriteLine("3 0 WALL -1 0 X 0 0\n6 0 WALL -1 0 X 0 0\n10 0 WALL -1 0 X 0 0\n15 0 WALL -1 0 X 0 0\n0 1 A -1 0 X 0 0\n6 1 B -1 0 X 0 0\n7 1 C -1 0 X 0 0\n9 1 C -1 0 X 0 0\n10 1 WALL -1 0 X 0 0\n14 1 B -1 0 X 0 0\n2 2 WALL -1 0 X 0 0\n3 2 WALL -1 0 X 0 0\n6 2 WALL -1 0 X 0 0\n7 2 WALL -1 0 X 0 0\n14 2 A -1 0 X 0 0\n2 3 WALL -1 0 X 0 0\n3 3 ROOT 1 1 N 0 1\n4 3 D -1 0 X 0 0\n5 3 WALL -1 0 X 0 0\n6 3 D -1 0 X 0 0\n8 3 WALL -1 0 X 0 0\n7 4 WALL -1 0 X 0 0\n9 4 D -1 0 X 0 0\n10 4 WALL -1 0 X 0 0\n11 4 D -1 0 X 0 0\n12 4 ROOT 0 2 N 0 2\n13 4 WALL -1 0 X 0 0\n1 5 A -1 0 X 0 0\n8 5 WALL -1 0 X 0 0\n9 5 WALL -1 0 X 0 0\n12 5 WALL -1 0 X 0 0\n13 5 WALL -1 0 X 0 0\n1 6 B -1 0 X 0 0\n5 6 WALL -1 0 X 0 0\n6 6 C -1 0 X 0 0\n8 6 C -1 0 X 0 0\n9 6 B -1 0 X 0 0\n15 6 A -1 0 X 0 0\n0 7 WALL -1 0 X 0 0\n5 7 WALL -1 0 X 0 0\n9 7 WALL -1 0 X 0 0\n12 7 WALL -1 0 X 0 0");
        
    }
}