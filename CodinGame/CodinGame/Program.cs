﻿using System.Reflection.Metadata;

namespace CodinGame;
public enum enType {
    WALL = 'W', ROOT = 'R', BASIC = 'B', TENTACLE = 'T', HARVESTER = 'H', SPORER = 'S', A = 'A', B = 'b', C = 'C', D = 'D', non
}
public enum enPlace{
        E = 'E', W = 'W', N = 'N', S = 'S', X = 'X'
    } 
public class Cell
{
    public int x {get; set;}
    public int y {get; set;}
    public enType Type {get; set;}
    public int owner {get; set;}
    public int organId {get; set;}
    public enPlace organDir {get; set;}
    public int organParentId {get; set;}
    public int organRootId {get; set;}
    public Cell(int x, int y)
    {
        this.x = x;
        this.y = y;
        Type = enType.non;
        owner = -1;
        organId = 0;
        organDir = enPlace.X;
        organParentId = -1;
        organRootId = 1;
    }
}
public class game_grid
{
    int Width;
    int Height;
    List<int> used_roots;
    public Tree<Cell> tree;
    public MyEntitys myEntitys;
    public game_grid(int width , int height)
    {
        Width = width;
        Height = height;
        myEntitys = new();
        tree = new(new Cell(0,0));
        Creat_game_grid();
        used_roots = new();
        TreeNode<Cell>.is_posible_move = is_posible_move_only_non;
        TreeNode<Cell>.set_order = set_order;
    }
    void Creat_game_grid()
    {
        for(int x = 0; x < Width; x++)
        {
            for(int y = 0; y < Height; y++){
                TreeNode<Cell> node = new(new Cell(x, y));
                myEntitys.nodes.Add($"{x},{y}" , node);
            }
        }
        creat_Tree();
    }
    void creat_Tree()
    {
        if(myEntitys.nodes.ContainsKey($"{0},{0}"))
            tree.Root = myEntitys.nodes[$"{0},{0}"];
        for(int x = 0; x < Width; x++)
        {
            for(int y = 0; y < Height; y++)
            {
                if(myEntitys.nodes.ContainsKey($"{x},{y}"))
                {
                    if(myEntitys.nodes.ContainsKey($"{x + 1},{y}"))
                        myEntitys.nodes[$"{x},{y}"].AddChild(myEntitys.nodes[$"{x + 1},{y}"], enPlace.E);
                    if(myEntitys.nodes.ContainsKey($"{x - 1},{y}"))
                        myEntitys.nodes[$"{x},{y}"].AddChild(myEntitys.nodes[$"{x - 1},{y}"], enPlace.W);
                    if(myEntitys.nodes.ContainsKey($"{x},{y + 1}"))
                        myEntitys.nodes[$"{x},{y}"].AddChild(myEntitys.nodes[$"{x},{y + 1}"], enPlace.S);
                    if(myEntitys.nodes.ContainsKey($"{x},{y - 1}"))
                        myEntitys.nodes[$"{x},{y}"].AddChild(myEntitys.nodes[$"{x},{y - 1}"], enPlace.N);
                }
            }
        }
    }
    public void set_data(string data)
    {
        if(string.IsNullOrEmpty(data))
            return;
        string []inputs = data.Split(' ');
        set_cell_data(ref myEntitys.nodes[$"{inputs[0]},{inputs[1]}"].Value, inputs);
    }
    void set_cell_data(ref Cell c, string []inputs)
    {
        c.Type = inputs[2] == "B" ? (enType)'b' : (enType)inputs[2][0];
        c.owner = int.Parse(inputs[3]);
        c.organId = int.Parse(inputs[4]);
        c.organDir = (enPlace)inputs[5][0];
        c.organParentId = int.Parse(inputs[6]);
        c.organRootId = int.Parse(inputs[7]);
        set_cells_to_his_list(ref c);
    }
    void set_cells_to_his_list(ref Cell c)
    {
        if(c.Type == enType.ROOT && c.owner == 1){
            myEntitys.organ.Add($"{c.x},{c.y}", myEntitys.nodes[$"{c.x},{c.y}"]);
            myEntitys.roots.Add($"{c.x},{c.y}", myEntitys.nodes[$"{c.x},{c.y}"]);}
        else if(c.Type == enType.HARVESTER && c.owner == 1){
            myEntitys.organ.Add($"{c.x},{c.y}", myEntitys.nodes[$"{c.x},{c.y}"]);
            myEntitys.HARVESTER.Add($"{c.x},{c.y}", myEntitys.nodes[$"{c.x},{c.y}"]);}
        else if(c.Type == enType.TENTACLE && c.owner == 1){
            myEntitys.organ.Add($"{c.x},{c.y}", myEntitys.nodes[$"{c.x},{c.y}"]);
            myEntitys.TENTACLE.Add($"{c.x},{c.y}", myEntitys.nodes[$"{c.x},{c.y}"]);}
        else if(c.Type == enType.SPORER && c.owner == 1){
            myEntitys.organ.Add($"{c.x},{c.y}", myEntitys.nodes[$"{c.x},{c.y}"]);
            myEntitys.SPORE.Add($"{c.x},{c.y}", myEntitys.nodes[$"{c.x},{c.y}"]);}
        else if(c.Type == enType.BASIC && c.owner == 1){
            myEntitys.organ.Add($"{c.x},{c.y}", myEntitys.nodes[$"{c.x},{c.y}"]);
            myEntitys.BASIC.Add($"{c.x},{c.y}", myEntitys.nodes[$"{c.x},{c.y}"]);}
        else if(c.Type == enType.A){
            myEntitys.p.Add($"{c.x},{c.y}", myEntitys.nodes[$"{c.x},{c.y}"]);
            myEntitys.A.Add($"{c.x},{c.y}", myEntitys.nodes[$"{c.x},{c.y}"]);}
        else if(c.Type == enType.B){
            myEntitys.p.Add($"{c.x},{c.y}", myEntitys.nodes[$"{c.x},{c.y}"]);
            myEntitys.B.Add($"{c.x},{c.y}", myEntitys.nodes[$"{c.x},{c.y}"]);}
        else if(c.Type == enType.C){
            myEntitys.p.Add($"{c.x},{c.y}", myEntitys.nodes[$"{c.x},{c.y}"]);
            myEntitys.C.Add($"{c.x},{c.y}", myEntitys.nodes[$"{c.x},{c.y}"]);}
        else if(c.Type == enType.D){
            myEntitys.p.Add($"{c.x},{c.y}", myEntitys.nodes[$"{c.x},{c.y}"]);
            myEntitys.D.Add($"{c.x},{c.y}", myEntitys.nodes[$"{c.x},{c.y}"]);}
    }
    void set_order(enPlace []search_order, Cell root, Cell dst)
    {
        int x = root.x - dst.x;
        int y = root.y - dst.y;
        if(x > 0)
        {
            if(y > 0)
            {
                if(x > y)
                {
                    search_order[0] = enPlace.W;
                    search_order[1] = enPlace.N;
                    search_order[2] = enPlace.S;
                    search_order[3] = enPlace.E;
                }
                else
                {
                    search_order[0] = enPlace.N;
                    search_order[1] = enPlace.W;
                    search_order[2] = enPlace.E;
                    search_order[3] = enPlace.S;
                }
            }
            else
            {
                if(x > y*-1)
                {
                    search_order[0] = enPlace.W;
                    search_order[1] = enPlace.S;
                    search_order[2] = enPlace.N;
                    search_order[3] = enPlace.E;
                }
                else
                {
                    search_order[0] = enPlace.S;
                    search_order[1] = enPlace.W;
                    search_order[2] = enPlace.E;
                    search_order[3] = enPlace.N;
                }
            }
        }
        else
        {
            if(y > 0)
            {
                if(x*-1 > y)
                {
                    search_order[0] = enPlace.E;
                    search_order[1] = enPlace.N;
                    search_order[2] = enPlace.S;
                    search_order[3] = enPlace.W;
                }
                else
                {
                    search_order[0] = enPlace.N;
                    search_order[1] = enPlace.E;
                    search_order[2] = enPlace.W;
                    search_order[3] = enPlace.S;
                }
            }
            else
            {
                if(x*-1 > y)
                {
                    search_order[0] = enPlace.E;
                    search_order[1] = enPlace.S;
                    search_order[2] = enPlace.N;
                    search_order[3] = enPlace.W;
                }
                else
                {
                    search_order[0] = enPlace.S;
                    search_order[1] = enPlace.E;
                    search_order[2] = enPlace.W;
                    search_order[3] = enPlace.N;
                }
            }
        }
    }
    public bool is_posible_move_only_non(TreeNode<Cell> node)
    {
        return node.Value.Type == enType.non;
    }
    public void is_get_closer(bool []b, TreeNode<Cell> current, TreeNode<Cell> dst, TreeNode<Cell> next)
    {
        int x_cur_to_dst = dst.Value.x > current.Value.x ? dst.Value.x - current.Value.x : current.Value.x - dst.Value.x;
        int y_cur_to_dst = dst.Value.y > current.Value.y ? dst.Value.y - current.Value.y : current.Value.y - dst.Value.y;

        int x_next_to_dst = dst.Value.x > next.Value.x ? dst.Value.x - next.Value.x : next.Value.x - dst.Value.x;
        int y_next_to_dst = dst.Value.y > next.Value.y ? dst.Value.y - next.Value.y : next.Value.y - dst.Value.y;

        b[0] = x_cur_to_dst + y_cur_to_dst > x_next_to_dst + y_next_to_dst || x_cur_to_dst + y_cur_to_dst == 1;
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
    public Dictionary<string, TreeNode<Cell>> nodes;
    public Dictionary<string, TreeNode<Cell>> roots;
    public Dictionary<string, TreeNode<Cell>> HARVESTER;
    public Dictionary<string, TreeNode<Cell>> TENTACLE;
    public Dictionary<string, TreeNode<Cell>> SPORE;
    public Dictionary<string, TreeNode<Cell>> BASIC;
    public Dictionary<string, TreeNode<Cell>> A;
    public Dictionary<string, TreeNode<Cell>> B;
    public Dictionary<string, TreeNode<Cell>> C;
    public Dictionary<string, TreeNode<Cell>> D;
    public Dictionary<string, TreeNode<Cell>> p;
    public Dictionary<string, TreeNode<Cell>> organ;
    public MyEntitys()
    {
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
    void swap_moves()
    {
        Stack<T> current_moves2 = new();
        while(0 < current_moves.Count)
            current_moves2.Push(current_moves.Pop());
        current_moves = current_moves2;
    }
    bool is_p_found(TreeNode<T> node)
    {
        if(E != null && EqualityComparer<T>.Default.Equals(node.Value, E.Value))
            return true;
        if(N != null && EqualityComparer<T>.Default.Equals(node.Value, N.Value))
            return true;
        if(W != null && EqualityComparer<T>.Default.Equals(node.Value, W.Value))
            return true;
        if(S != null && EqualityComparer<T>.Default.Equals(node.Value, S.Value))
            return true;
        return false;
    }
    public TreeNode<T>? Find_with_pre_order_p(TreeNode<T> value, int max_depth, bool first_time = true)
    {
        if(first_time)
            count = 0;
        else
        {
            bool? b = is_posible_move?.Invoke(this);
            bool make_move = true;
            if(b == null || b.Value == false)
                make_move = false;
            if(!make_move || current_moves.Contains(Value))
                return null;
        }
        if(count == 300)
            return null;
        count++;
        TreeNode<T>? found = null;
        foreach(TreeNode<T> node in Children)
        {
            if(!first_time && is_p_found(value))
            {
                current_moves.Push(Value);
                count++;
                return this;
            }
        }
        if(max_depth < 0)
            return null;
        current_moves.Push(Value);
        set_order?.Invoke(search_order, Value, value.Value);
        if(Children[0] != null)
            found = Children[0].Find_with_pre_order_p(value, max_depth - 1,false);
        if(found == null && Children[1] != null)
            found = Children[1].Find_with_pre_order_p(value, max_depth - 1,false);
        if(found == null && Children[2] != null)
            found = Children[2].Find_with_pre_order_p(value, max_depth - 1,false);
        if(found == null && Children[3] != null)
            found = Children[3].Find_with_pre_order_p(value, max_depth - 1,false);
        if(found == null && current_moves.Count > 0)
            current_moves.Pop();
        return found;
    }
    public TreeNode<T>? Find_with_pre_order(TreeNode<T> value, int max_depth, bool first_time = true)
    {
        if(first_time)
            count = 0;
        else
        {
            bool? b = is_posible_move?.Invoke(this);
            bool make_move = true;
            if(b == null || b.Value == false)
                make_move = false;
            if(!make_move || current_moves.Contains(Value))
                return null;
        }
        if(count == 1000)
            return null;
        count++;
        TreeNode<T>? found = null;
        if(EqualityComparer<T>.Default.Equals(Value, value.Value))
        {
            current_moves.Push(Value);
            count++;
            swap_moves();
            return this;
        }
        if(max_depth < 0)
            return null;
        current_moves.Push(Value);
        set_order?.Invoke(search_order, Value, value.Value);
        if(Children[0] != null)
            found = Children[0].Find_with_pre_order(value, max_depth - 1,false);
        if(found == null && Children[1] != null)
            found = Children[1].Find_with_pre_order(value, max_depth - 1,false);
        if(found == null && Children[2] != null)
            found = Children[2].Find_with_pre_order(value, max_depth - 1,false);
        if(found == null && Children[3] != null)
            found = Children[3].Find_with_pre_order(value, max_depth - 1,false);
        if(found == null && current_moves.Count > 0)
            current_moves.Pop();
        return found;
    }
    public (TreeNode<T>?, TreeNode<T>?, enPlace) Find_with_one_direction(int max_depth)
    {
        TreeNode<T>? found = null;
        if(E != null){
            found = Find_with_one_direction(max_depth, enPlace.E);}
        if(found != null)
            return (E, found, enPlace.E);
        if(W != null){
            found = Find_with_one_direction(max_depth, enPlace.W);}
        if(found != null)
            return (W, found, enPlace.W);
        if(S != null){
            found = Find_with_one_direction(max_depth, enPlace.S);}
        if(found != null)
            return (S, found, enPlace.S);
        if(N != null){
            found = Find_with_one_direction(max_depth, enPlace.N);}
        if(found != null)
            return (N, found, enPlace.N);
        return (null, null, enPlace.E); 
    }
    TreeNode<T>? get_node_by_dir(enPlace place)
    {
        switch (place)
        {
            case enPlace.W:
                return W;
            case enPlace.E:
                return E;
            case enPlace.S:
                return S;
            case enPlace.N:
                return N;
        }
        return null;
    }
    public TreeNode<T>? Find_with_one_direction(int max_depth, enPlace place)
    {
        TreeNode<T>? node = get_node_by_dir(place);
        if(node == null)
            return null;
        bool? b = is_posible_move?.Invoke(node);
        if(b == null || b.Value == false)
            return null;
        if(max_depth == 1)
            return this;
        if(node != null)
            return node.Find_with_one_direction(max_depth - 1, place);
        return null;
    }
    bool is_found(TreeNode<T> value)
    {
        bool? is_found = is_Level_order_found?.Invoke(value);
        return is_found != null && is_found.Value;
    }
    TreeNode<T>? find_value(TreeNode<T> obj)
    {
        if(obj.E != null && is_found(obj.E)){
            search_order[0] = enPlace.E;
            return this;}
        if(obj.W != null && is_found(obj.W)){
            search_order[0] = enPlace.W;
            return this;}
        if(obj.S != null && is_found(obj.S)){
            search_order[0] = enPlace.S;
            return this;}
        if(obj.N != null && is_found(obj.N)){
            search_order[0] = enPlace.N;
            return this;}
        return null;
    }
    public TreeNode<T>? Level_order_search(int max_depth, bool first_time = true)
    {
        TreeNode<T>? found = null;
        if(max_depth == 0)
            return null;
        if(first_time)
            count = 0;
        else 
        {
            bool? b = is_posible_move?.Invoke(this);
            bool make_move = true;
            if(b == null || b.Value == false)
                make_move = false;
            if(!make_move || current_moves.Contains(Value))
                return null;
            found = find_value(this);
            if(found != null) 
                return found;
        }
        count++;
        current_moves.Push(Value);
        if(S != null)
            found = S.Level_order_search(max_depth - 1, false);
        if(found == null && W != null)
            found = W.Level_order_search(max_depth - 1, false);
        if(found == null && E != null)
            found = E.Level_order_search(max_depth - 1, false);
        if(found == null && N != null)
            found = N.Level_order_search(max_depth - 1, false);
        if(found == null && current_moves.Count > 0) {
            count--;
            current_moves.Pop();}
        return found;
    }
    public enPlace []search_order;
    static public enPlace []global_order = [enPlace.E, enPlace.S, enPlace.W, enPlace.N];
    void set_order_of_search(enPlace place, ref TreeNode<T> node)
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
    public TreeNode<T> []Children
    {
        get 
        {
            TreeNode<T> []nodes = new TreeNode<T>[4];
            for(int i = 0; i < nodes.Length; i++)
                set_order_of_search(search_order[i], ref nodes[i]);
            return nodes;
        }
    }
    static public Action<enPlace [], T, T>? set_order;
    static public Predicate<TreeNode<T>>? is_posible_move;
    static public Predicate<TreeNode<T>>? is_Level_order_found;
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
public class Moves
{
    public int moves_num { get; set; }
    public Queue<Cell> my_moves {get; set;}
    static public game_grid Game;
    public Moves(game_grid game, int Moves_num)
    {
        my_moves = new();
        Game = game;
        moves_num = Moves_num;
        Find_Move();
    }
    public void Find_Move()
    {
        for(int i = 0; i < Game.myEntitys.roots.Count; i++)
        {
            if(moves.Count - i > 0) {
            continue;}
            if(Proteins.HARVESTER && HARVESTER_moves.move1(Game)){
            continue;}
            if(Select_protein()){
            continue;}
            if(Proteins.ROOT && SPORE_moves.Move1(Game)){
            continue;}
            if(Proteins.SPORE && SPORE_moves.Move2(Game)){
            continue;}
            if(Proteins.BASIC && BASIC_moves.move3(Game)){
            continue;}
        }
        if(moves.Count == 0)
        {
            for(int i = 0; i < Game.myEntitys.roots.Count; i++)
            {
                if(Proteins.BASIC && BASIC_moves.move2(Game)){
                    continue;}
                if(Proteins.BASIC && BASIC_moves.move8(Game)){
                    continue;}
            }
        }
        for(int i = moves.Count; i < moves_num; i++)
            moves.Enqueue("WAIT");
    }
    bool Select_protein()
    {
        foreach(var p in Game.myEntitys.p)
        {
            if(p.Value.Value.Type == enType.A && !has_A() && Proteins.BASIC)
            {
                if(BASIC_moves.move4_pA(Game))
                    return true;
            }
            else if(p.Value.Value.Type == enType.B && !has_B() && Proteins.BASIC)
            {
                if(BASIC_moves.move5_pB(Game))
                    return true;
            }
            else if(p.Value.Value.Type == enType.C && !has_C() && Proteins.BASIC)
            {
                if(BASIC_moves.move6_pC(Game))
                    return true;
            }
            else if(p.Value.Value.Type == enType.D && !has_D() && Proteins.BASIC)
            {
                if(BASIC_moves.move7_pD(Game))
                    return true;
            }
            if(Proteins.D > Proteins.C )
            {
                if(SPORE_move(p.Value))
                    return true;
                if(TENTACLE_move(p.Value))
                    return true;
            }
            else
            {
                if(TENTACLE_move(p.Value))
                    return true;
                if(SPORE_move(p.Value))
                    return true;
            }
            if(p.Value.Value.Type == enType.A && !has_A() && Proteins.HARVESTER)
            {
                if(HARVESTER_moves.move4_pA(Game))
                    return true;
            }
            else if(p.Value.Value.Type == enType.B && !has_B() && Proteins.HARVESTER)
            {
                if(HARVESTER_moves.move5_pB(Game))
                    return true;
            }
            else if(p.Value.Value.Type == enType.C && !has_C() && Proteins.HARVESTER)
            {
                if(HARVESTER_moves.move6_pC(Game))
                    return true;
            }
            else if(p.Value.Value.Type == enType.D && !has_D() && Proteins.HARVESTER)
            {
                if(HARVESTER_moves.move7_pD(Game))
                    return true;
            }
        }
        return false;
    }
    public bool TENTACLE_move(TreeNode<Cell> node)
    {
        if(node.Value.Type == enType.A && !has_A() && Proteins.TENTACLE)
        {
            if(TENTACLE_moves.move4_pA(Game))
                return true;
        }
        else if(node.Value.Type == enType.B && !has_B() && Proteins.TENTACLE)
        {
            if(TENTACLE_moves.move5_pB(Game))
                return true;
        }
        else if(node.Value.Type == enType.C && !has_C() && Proteins.TENTACLE)
        {
            if(TENTACLE_moves.move6_pC(Game))
                return true;
        }
        else if(node.Value.Type == enType.D && !has_D() && Proteins.TENTACLE)
        {
            if(TENTACLE_moves.move7_pD(Game))
                return true;
        }
        return false;
    }
    public bool SPORE_move(TreeNode<Cell> node)
    {
        if(node.Value.Type == enType.A && !has_A() && Proteins.SPORE_for_Proteins)
        {
            if(SPORE_moves.move4_pA(Game))
                return true;
        }
        else if(node.Value.Type == enType.B && !has_B() && Proteins.SPORE_for_Proteins)
        {
            if(SPORE_moves.move5_pB(Game))
                return true;
        }
        else if(node.Value.Type == enType.C && !has_C() && Proteins.SPORE_for_Proteins)
        {
            if(SPORE_moves.move6_pC(Game))
                return true;
        }
        else if(node.Value.Type == enType.D && !has_D() && Proteins.SPORE_for_Proteins)
        {
            if(SPORE_moves.move7_pD(Game))
                return true;
        }
        return false;
    }
    public static Queue<string> moves = new();
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
public class SPORE_moves()
{
    public static bool is_posible_move(TreeNode<Cell> node)
    {
        if(node == null || node.Value == null)
            return false;
        switch(node.Value.Type)
        {
            case enType.non:
                return true;
        }
        return false; 
    }
    public static bool Move1(game_grid Game)
    {
        TreeNode<Cell>.is_posible_move = is_posible_move;
        foreach(var SPORE in Game.myEntitys.SPORE)
        {
            if(Game.is_used_root(SPORE.Value.Value.organRootId))
                continue;
            TreeNode<Cell>? newRoot;
            newRoot = SPORE.Value.Find_with_one_direction(4, SPORE.Value.Value.organDir);
            if(newRoot == null)
                continue;
            string str = $"SPORE {SPORE.Value.Value.organId} {newRoot.Value.x} {newRoot.Value.y}";
            Game.use_root(SPORE.Value.Value.organRootId);
            Moves.moves.Enqueue(str);
            Proteins.SPORE_created();
            return true;
        }
        return false;
    }
    public static bool Move2(game_grid Game)
    {
        foreach(var organ in Game.myEntitys.organ)
        {
            TreeNode<Cell>.is_posible_move = is_posible_move;
            if(Game.is_used_root(organ.Value.Value.organRootId))
                continue;
            TreeNode<Cell>? sp, newRoot;
            enPlace dir;
            (sp, newRoot, dir) = organ.Value.Find_with_one_direction(5);
            if(sp == null || newRoot == null)
                continue;
            string str = $"GROW {organ.Value.Value.organId} {sp.Value.x} {sp.Value.y} SPORER {dir}";
            Game.use_root(organ.Value.Value.organRootId);
            Moves.moves.Enqueue(str);
            Proteins.ROOT_created();
            return true;
        }
        return false;
    }

    public static bool move4_pA(game_grid Game)
    {
        TreeNode<Cell>.is_posible_move = is_posible_move;
        foreach(var p in Game.myEntitys.A)
        {
            foreach(var organ in Game.myEntitys.organ)
            {
                if(Game.is_used_root(organ.Value.Value.organRootId))
                    continue;
                if(Proteins.Has_any_HARVESTER(p.Value))
                    continue;
                TreeNode<Cell>? NODE = organ.Value.Find_with_pre_order_p(p.Value, 10);
                if(NODE == null)
                    continue;
                string str = $"GROW {organ.Value.Value.organId} {NODE.Value.x} {NODE.Value.y} SPORER S";
                Game.use_root(organ.Value.Value.organRootId);
                Moves.moves.Enqueue(str);
                Proteins.BASIC_created();
                return true;
            }
        }
        return false;
    }
    public static bool move5_pB(game_grid Game)
    {
        TreeNode<Cell>.is_posible_move = is_posible_move;
        foreach(var p in Game.myEntitys.B)
        {
            foreach(var organ in Game.myEntitys.organ)            
            {   
                if(Game.is_used_root(organ.Value.Value.organRootId))
                    continue;
                if(Proteins.Has_any_HARVESTER(p.Value))
                    continue;
                TreeNode<Cell>? NODE = organ.Value.Find_with_pre_order_p(p.Value, 10);
                if(NODE == null)
                    continue;
                string str = $"GROW {organ.Value.Value.organId} {NODE.Value.x} {NODE.Value.y} SPORER S";
                Game.use_root(organ.Value.Value.organRootId);
                Moves.moves.Enqueue(str);
                Proteins.BASIC_created();
                return true;
            }
        }
        return false;
    }
    public static bool move6_pC(game_grid Game)
    {
        TreeNode<Cell>.is_posible_move = is_posible_move;
        foreach(var p in Game.myEntitys.C)
        {
            foreach(var organ in Game.myEntitys.organ)
            {
                if(Game.is_used_root(organ.Value.Value.organRootId))
                continue;
                if(Proteins.Has_any_HARVESTER(p.Value))
                    continue;
                TreeNode<Cell>? NODE = organ.Value.Find_with_pre_order_p(p.Value, 10);
                if(NODE == null)
                    continue;
                string str = $"GROW {organ.Value.Value.organId} {NODE.Value.x} {NODE.Value.y} SPORER S";
                Game.use_root(organ.Value.Value.organRootId);
                Moves.moves.Enqueue(str);
                Proteins.BASIC_created();
                return true;
            }
        }
        return false;
    }
    public static bool move7_pD(game_grid Game)
    {
        TreeNode<Cell>.is_posible_move = is_posible_move;
        foreach(var p in Game.myEntitys.D)
        {
            foreach(var organ in Game.myEntitys.organ)
            {
                if(Game.is_used_root(organ.Value.Value.organRootId))
                continue;
                if(Proteins.Has_any_HARVESTER(p.Value))
                    continue;
                TreeNode<Cell>? NODE = organ.Value.Find_with_pre_order_p(p.Value, 10);
                if(NODE == null)
                    continue;
                string str = $"GROW {organ.Value.Value.organId} {NODE.Value.x} {NODE.Value.y} SPORER S";
                Game.use_root(organ.Value.Value.organRootId);
                Moves.moves.Enqueue(str);
                Proteins.BASIC_created();
                return true;
            }
        }
        return false;
    }
}
public class BASIC_moves()
{
    public static bool is_posible_move(TreeNode<Cell> node)
    {
        if(node == null || node.Value == null)
            return false;
        switch(node.Value.Type)
        {
            case enType.non:
                return true;
        }
        return false; 
    }
    public static bool is_posible_move2(TreeNode<Cell> node)
    {
        if(node == null || node.Value == null || Proteins.Has_my_HARVESTER(node))
            return false;
        switch(node.Value.Type)
        {
            case enType.A:
            case enType.B:
            case enType.C:
            case enType.D:
            case enType.non:
                return true;
        }
        return false; 
    }
    public static bool is_posible_move3(TreeNode<Cell> node)
    {
        if(node == null || node.Value == null)
            return false;
        switch(node.Value.Type)
        {
            case enType.A:
            case enType.B:
            case enType.C:
            case enType.D:
            case enType.non:
                return true;
        }
        return false; 
    }
    public static bool move3(game_grid Game)
    {
        TreeNode<Cell>.is_posible_move = is_posible_move2;
        foreach(var organ in Game.myEntitys.organ)
        {
            if(Game.is_used_root(organ.Value.Value.organRootId))
                continue;
            foreach(var p in Game.myEntitys.p)
            {
                TreeNode<Cell>? NODE = organ.Value.Find_with_pre_order(p.Value, 2);
                if(NODE == null)
                    continue;
                string str = $"GROW {organ.Value.Value.organId} {NODE.Value.x} {NODE.Value.y} BASIC";
                Game.use_root(organ.Value.Value.organRootId);
                Moves.moves.Enqueue(str);
                Proteins.BASIC_created();
                return true;
            }
        }
        return false;
    }
    public static bool move8(game_grid Game)
    {
        TreeNode<Cell>.is_posible_move = is_posible_move3;
        foreach(var organ in Game.myEntitys.organ)
        {
            if(Game.is_used_root(organ.Value.Value.organRootId))
                continue;
            foreach(var p in Game.myEntitys.p)
            {
                TreeNode<Cell>? NODE = organ.Value.Find_with_pre_order(p.Value, 2);
                if(NODE == null)
                    continue;
                string str = $"GROW {organ.Value.Value.organId} {NODE.Value.x} {NODE.Value.y} BASIC";
                Game.use_root(organ.Value.Value.organRootId);
                Moves.moves.Enqueue(str);
                Proteins.BASIC_created();
                return true;
            }
        }
        return false;
    }
    public static bool move4_pA(game_grid Game)
    {
        TreeNode<Cell>.is_posible_move = is_posible_move;
        foreach(var p in Game.myEntitys.A)
        {
            foreach(var organ in Game.myEntitys.organ)
            {
                if(Game.is_used_root(organ.Value.Value.organRootId))
                    continue;
                if(Proteins.Has_any_HARVESTER(p.Value))
                    continue;
                TreeNode<Cell>? NODE = organ.Value.Find_with_pre_order_p(p.Value, 10);
                if(NODE == null)
                    continue;
                string str = $"GROW {organ.Value.Value.organId} {NODE.Value.x} {NODE.Value.y} BASIC";
                Game.use_root(organ.Value.Value.organRootId);
                Moves.moves.Enqueue(str);
                Proteins.BASIC_created();
                return true;
            }
        }
        return false;
    }
    public static bool move5_pB(game_grid Game)
    {
        TreeNode<Cell>.is_posible_move = is_posible_move;
        foreach(var p in Game.myEntitys.B)
        {
            foreach(var organ in Game.myEntitys.organ)            
            {   
                if(Game.is_used_root(organ.Value.Value.organRootId))
                    continue;
                if(Proteins.Has_any_HARVESTER(p.Value))
                    continue;
                TreeNode<Cell>? NODE = organ.Value.Find_with_pre_order_p(p.Value, 10);
                if(NODE == null)
                    continue;
                string str = $"GROW {organ.Value.Value.organId} {NODE.Value.x} {NODE.Value.y} BASIC";
                Game.use_root(organ.Value.Value.organRootId);
                Moves.moves.Enqueue(str);
                Proteins.BASIC_created();
                return true;
            }
        }
        return false;
    }
    public static bool move6_pC(game_grid Game)
    {
        TreeNode<Cell>.is_posible_move = is_posible_move;
        foreach(var p in Game.myEntitys.C)
        {
            foreach(var organ in Game.myEntitys.organ)
            {
                if(Game.is_used_root(organ.Value.Value.organRootId))
                continue;
                if(Proteins.Has_any_HARVESTER(p.Value))
                    continue;
                TreeNode<Cell>? NODE = organ.Value.Find_with_pre_order_p(p.Value, 10);
                if(NODE == null)
                    continue;
                string str = $"GROW {organ.Value.Value.organId} {NODE.Value.x} {NODE.Value.y} BASIC";
                Game.use_root(organ.Value.Value.organRootId);
                Moves.moves.Enqueue(str);
                Proteins.BASIC_created();
                return true;
            }
        }
        return false;
    }
    public static bool move7_pD(game_grid Game)
    {
        TreeNode<Cell>.is_posible_move = is_posible_move;
        foreach(var p in Game.myEntitys.D)
        {
            foreach(var organ in Game.myEntitys.organ)
            {
                if(Game.is_used_root(organ.Value.Value.organRootId))
                continue;
                if(Proteins.Has_any_HARVESTER(p.Value))
                    continue;
                TreeNode<Cell>? NODE = organ.Value.Find_with_pre_order_p(p.Value, 10);
                if(NODE == null)
                    continue;
                string str = $"GROW {organ.Value.Value.organId} {NODE.Value.x} {NODE.Value.y} BASIC";
                Game.use_root(organ.Value.Value.organRootId);
                Moves.moves.Enqueue(str);
                Proteins.BASIC_created();
                return true;
            }
        }
        return false;
    }
    public static bool is_Level_order_found(TreeNode<Cell> node)
    {
        if(TreeNode<Cell>.count == 1 && !Proteins.is_porotein(node))
            return true;
        return false;
    }
    public static bool move2(game_grid Game)
    {
        TreeNode<Cell>.is_posible_move = is_posible_move;
        TreeNode<Cell>.is_Level_order_found = is_Level_order_found;
        foreach(var organ in Game.myEntitys.organ)
        {
            if(Game.is_used_root(organ.Value.Value.organRootId))
                continue;
            TreeNode<Cell>? NODE = organ.Value.Level_order_search(15);
            if(NODE == null)
                continue;
            string str = $"GROW {organ.Value.Value.organId} {NODE.Value.x} {NODE.Value.y} BASIC";
            Game.use_root(organ.Value.Value.organRootId);
            Moves.moves.Enqueue(str);
            Proteins.BASIC_created();
            return true;
        }
        return false;
    }
}
public class HARVESTER_moves()
{
    public static bool is_any_HARVESTER(TreeNode<Cell>? node)
    {
        if(node == null || node.Value == null)
            return false;
        switch(node.Value.Type)
        {
            case enType.HARVESTER:
                return true;
        }
        return false; 
    }
    public static bool is_my_HARVESTER(TreeNode<Cell>? node)
    {
        if(node == null || node.Value == null)
            return false;
        if(node.Value.Type == enType.HARVESTER && node.Value.owner == 1)
            return true;
        return false; 
    }
    public static bool is_posible_move(TreeNode<Cell> node)
    {
        if(node == null || node.Value == null)
            return false;
        switch(node.Value.Type)
        {
            case enType.A:
            case enType.B:
            case enType.C:
            case enType.D:
            case enType.non:
                return true;
        }
        return false; 
    }
    public static bool is_Level_order_found(TreeNode<Cell> node)
    {
        if(Proteins.is_porotein(node) && !Proteins.Has_any_HARVESTER(node))
            return true;
        return false;
    }
    public static bool move1(game_grid Game)
    {
        TreeNode<Cell>.is_Level_order_found = is_Level_order_found;
        foreach(var organ in Game.myEntitys.organ)
        {
            if(Game.is_used_root(organ.Value.Value.organRootId))
                continue;
            TreeNode<Cell>? NODE = organ.Value.Level_order_search(2);
            if(NODE == null)
                continue;
            string str = $"GROW {organ.Value.Value.organId} {NODE.Value.x} {NODE.Value.y} HARVESTER {NODE.search_order[0]}";
            Game.use_root(organ.Value.Value.organRootId);
            Moves.moves.Enqueue(str);
            return true;
        }
        return false;
    }
    public static bool move4_pA(game_grid Game)
    {
        TreeNode<Cell>.is_posible_move = is_posible_move;
        foreach(var p in Game.myEntitys.A)
        {
            foreach(var organ in Game.myEntitys.organ)
            {
                if(Game.is_used_root(organ.Value.Value.organRootId))
                    continue;
                if(Proteins.Has_any_HARVESTER(p.Value))
                    continue;
                TreeNode<Cell>? NODE = organ.Value.Find_with_pre_order_p(p.Value, 10);
                if(NODE == null)
                    continue;
                string str = $"GROW {organ.Value.Value.organId} {NODE.Value.x} {NODE.Value.y} HARVESTER S";
                Game.use_root(organ.Value.Value.organRootId);
                Moves.moves.Enqueue(str);
                Proteins.BASIC_created();
                return true;
            }
        }
        return false;
    }
    public static bool move5_pB(game_grid Game)
    {
        TreeNode<Cell>.is_posible_move = is_posible_move;
        foreach(var p in Game.myEntitys.B)
        {
            foreach(var organ in Game.myEntitys.organ)            
            {   
                if(Game.is_used_root(organ.Value.Value.organRootId))
                    continue;
                if(Proteins.Has_any_HARVESTER(p.Value))
                    continue;
                TreeNode<Cell>? NODE = organ.Value.Find_with_pre_order_p(p.Value, 10);
                if(NODE == null)
                    continue;
                string str = $"GROW {organ.Value.Value.organId} {NODE.Value.x} {NODE.Value.y} HARVESTER S";
                Game.use_root(organ.Value.Value.organRootId);
                Moves.moves.Enqueue(str);
                Proteins.BASIC_created();
                return true;
            }
        }
        return false;
    }
    public static bool move6_pC(game_grid Game)
    {
        TreeNode<Cell>.is_posible_move = is_posible_move;
        foreach(var p in Game.myEntitys.C)
        {
            foreach(var organ in Game.myEntitys.organ)
            {
                if(Game.is_used_root(organ.Value.Value.organRootId))
                continue;
                if(Proteins.Has_any_HARVESTER(p.Value))
                    continue;
                TreeNode<Cell>? NODE = organ.Value.Find_with_pre_order_p(p.Value, 10);
                if(NODE == null)
                    continue;
                string str = $"GROW {organ.Value.Value.organId} {NODE.Value.x} {NODE.Value.y} HARVESTER S";
                Game.use_root(organ.Value.Value.organRootId);
                Moves.moves.Enqueue(str);
                Proteins.BASIC_created();
                return true;
            }
        }
        return false;
    }
    public static bool move7_pD(game_grid Game)
    {
        TreeNode<Cell>.is_posible_move = is_posible_move;
        foreach(var p in Game.myEntitys.D)
        {
            foreach(var organ in Game.myEntitys.organ)
            {
                if(Game.is_used_root(organ.Value.Value.organRootId))
                continue;
                if(Proteins.Has_any_HARVESTER(p.Value))
                    continue;
                TreeNode<Cell>? NODE = organ.Value.Find_with_pre_order_p(p.Value, 10);
                if(NODE == null)
                    continue;
                string str = $"GROW {organ.Value.Value.organId} {NODE.Value.x} {NODE.Value.y} HARVESTER S";
                Game.use_root(organ.Value.Value.organRootId);
                Moves.moves.Enqueue(str);
                Proteins.BASIC_created();
                return true;
            }
        }
        return false;
    }
}
public class TENTACLE_moves()
{
    public static bool is_posible_move(TreeNode<Cell> node)
    {
        if(node == null || node.Value == null)
            return false;
        switch(node.Value.Type)
        {
            case enType.non:
                return true;
        }
        return false; 
    }
    public static bool move4_pA(game_grid Game)
    {
        TreeNode<Cell>.is_posible_move = is_posible_move;
        foreach(var p in Game.myEntitys.A)
        {
            foreach(var organ in Game.myEntitys.organ)
            {
                if(Game.is_used_root(organ.Value.Value.organRootId))
                    continue;
                if(Proteins.Has_any_HARVESTER(p.Value))
                    continue;
                TreeNode<Cell>? NODE = organ.Value.Find_with_pre_order_p(p.Value, 10);
                if(NODE == null)
                    continue;
                string str = $"GROW {organ.Value.Value.organId} {NODE.Value.x} {NODE.Value.y} TENTACLE S";
                Game.use_root(organ.Value.Value.organRootId);
                Moves.moves.Enqueue(str);
                Proteins.BASIC_created();
                return true;
            }
        }
        return false;
    }
    public static bool move5_pB(game_grid Game)
    {
        TreeNode<Cell>.is_posible_move = is_posible_move;
        foreach(var p in Game.myEntitys.B)
        {
            foreach(var organ in Game.myEntitys.organ)            
            {   
                if(Game.is_used_root(organ.Value.Value.organRootId))
                    continue;
                if(Proteins.Has_any_HARVESTER(p.Value))
                    continue;
                TreeNode<Cell>? NODE = organ.Value.Find_with_pre_order_p(p.Value, 10);
                if(NODE == null)
                    continue;
                string str = $"GROW {organ.Value.Value.organId} {NODE.Value.x} {NODE.Value.y} TENTACLE S";
                Game.use_root(organ.Value.Value.organRootId);
                Moves.moves.Enqueue(str);
                Proteins.BASIC_created();
                return true;
            }
        }
        return false;
    }
    public static bool move6_pC(game_grid Game)
    {
        TreeNode<Cell>.is_posible_move = is_posible_move;
        foreach(var p in Game.myEntitys.C)
        {
            foreach(var organ in Game.myEntitys.organ)
            {
                if(Game.is_used_root(organ.Value.Value.organRootId))
                continue;
                if(Proteins.Has_any_HARVESTER(p.Value))
                    continue;
                TreeNode<Cell>? NODE = organ.Value.Find_with_pre_order_p(p.Value, 10);
                if(NODE == null)
                    continue;
                string str = $"GROW {organ.Value.Value.organId} {NODE.Value.x} {NODE.Value.y} TENTACLE S";
                Game.use_root(organ.Value.Value.organRootId);
                Moves.moves.Enqueue(str);
                Proteins.BASIC_created();
                return true;
            }
        }
        return false;
    }
    public static bool move7_pD(game_grid Game)
    {
        TreeNode<Cell>.is_posible_move = is_posible_move;
        foreach(var p in Game.myEntitys.D)
        {
            foreach(var organ in Game.myEntitys.organ)
            {
                if(Game.is_used_root(organ.Value.Value.organRootId))
                continue;
                if(Proteins.Has_any_HARVESTER(p.Value))
                    continue;
                TreeNode<Cell>? NODE = organ.Value.Find_with_pre_order_p(p.Value, 10);
                if(NODE == null)
                    continue;
                string str = $"GROW {organ.Value.Value.organId} {NODE.Value.x} {NODE.Value.y} TENTACLE S";
                Game.use_root(organ.Value.Value.organRootId);
                Moves.moves.Enqueue(str);
                Proteins.BASIC_created();
                return true;
            }
        }
        return false;
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
            a += count;
            return a; 
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
            b += count;
            return b; 
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
            c += count;
            return c; 
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
            d += count;
            return d; 
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
    static public bool SPORE_for_Proteins
    {
        get {return B > 1 && D > 1;}
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
        D--;
    }
    public static bool is_porotein(TreeNode<Cell>? node)
    {
        if(node == null || node.Value == null)
            return false;
        switch(node.Value.Type)
        {
            case enType.A:
            case enType.B:
            case enType.C:
            case enType.D:
                return true;
        }
        return false; 
    }
    public static bool Has_any_HARVESTER(TreeNode<Cell> node)
    {
        if(node == null)
            return false;
        if(node.N != null && HARVESTER_moves.is_any_HARVESTER(node.N) && node.N.Value.organDir == enPlace.S)
            return true;
        if(node.E != null && HARVESTER_moves.is_any_HARVESTER(node.E) && node.E.Value.organDir == enPlace.W)
            return true;
        if(node.W != null && HARVESTER_moves.is_any_HARVESTER(node.W) && node.W.Value.organDir == enPlace.E)
            return true;
        if(node.S != null && HARVESTER_moves.is_any_HARVESTER(node.S) && node.S.Value.organDir == enPlace.N)
            return true;
        return false;
    }
    public static bool Has_my_HARVESTER(TreeNode<Cell> node)
    {
        if(node == null)
            return false;
        if(node.N != null && HARVESTER_moves.is_my_HARVESTER(node.N) && node.N.Value.organDir == enPlace.S)
            return true;
        if(node.E != null && HARVESTER_moves.is_my_HARVESTER(node.E) && node.E.Value.organDir == enPlace.W)
            return true;
        if(node.W != null && HARVESTER_moves.is_my_HARVESTER(node.W) && node.W.Value.organDir == enPlace.E)
            return true;
        if(node.S != null && HARVESTER_moves.is_my_HARVESTER(node.S) && node.S.Value.organDir == enPlace.N)
            return true;
        return false;
    }
}

class Program
{
    static public void Set_cell_data(game_grid game, string data)
    {
        foreach(string node_data in data.Split('\n'))
            game.set_data(node_data);
    }
    static void Main(string[] args)
    {
        var watch = System.Diagnostics.Stopwatch.StartNew();
        // the code that you want to measure comes here

        Console.Clear();
        //2 , 3, 6, 3
        game_grid game = new game_grid(24, 12);
        //Set_cell_data(game, "5 3 SPORER 1 6 S 1 1\n7 3 A 1 4 S 1 1\n6 3 SPORER 1 6 S 1 1\n6 0 WALL -1 0 X 0 0\n10 0 WALL -1 0 X 0 0\n15 0 WALL -1 0 X 0 0\n0 1 A -1 0 X 0 0\n6 1 B -1 0 X 0 0\n7 1 C -1 0 X 0 0\n9 1 C -1 0 X 0 0\n10 1 WALL -1 0 X 0 0\n14 1 B -1 0 X 0 0\n2 2 WALL -1 0 X 0 0\n3 2 WALL -1 0 X 0 0\n6 2 WALL -1 0 X 0 0\n7 2 WALL -1 0 X 0 0\n14 2 A -1 0 X 0 0\n2 3 WALL -1 0 X 0 0\n3 3 ROOT 1 1 N 0 1\n4 3 D -1 0 X 0 0\n6 3 D -1 0 X 0 0\n8 3 WALL -1 0 X 0 0\n7 4 WALL -1 0 X 0 0\n9 4 D -1 0 X 0 0\n10 4 WALL -1 0 X 0 0\n11 4 D -1 0 X 0 0\n12 4 ROOT 0 2 N 0 2\n13 4 WALL -1 0 X 0 0\n1 5 A -1 0 X 0 0\n8 5 WALL -1 0 X 0 0\n9 5 WALL -1 0 X 0 0\n12 5 WALL -1 0 X 0 0\n13 5 WALL -1 0 X 0 0\n1 6 B -1 0 X 0 0\n5 6 WALL -1 0 X 0 0\n6 6 C -1 0 X 0 0\n8 6 C -1 0 X 0 0\n9 6 B -1 0 X 0 0\n15 6 A -1 0 X 0 0\n0 7 WALL -1 0 X 0 0\n5 7 WALL -1 0 X 0 0\n9 7 WALL -1 0 X 0 0\n12 7 WALL -1 0 X 0 0");
        Set_cell_data(game, "0 0 WALL -1 0 X 0 0\n2 0 BASIC 1 6 N 1 1\n3 0 BASIC 1 8 N 6 1\n4 0 BASIC 1 10 N 8 1\n5 0 BASIC 1 12 N 10 1\n6 0 BASIC 1 14 N 12 1\n7 0 TENTACLE 1 16 S 14 1\n8 0 TENTACLE 1 18 S 16 1\n9 0 TENTACLE 1 20 S 18 1\n10 0 WALL -1 0 X 0 0\n12 0 WALL -1 0 X 0 0\n13 0 A -1 0 X 0 0\n15 0 C -1 0 X 0 0\n16 0 C -1 0 X 0 0\n17 0 D -1 0 X 0 0\n18 0 D -1 0 X 0 0\n21 0 WALL -1 0 X 0 0\n2 1 ROOT 1 1 N 0 1\n3 1 WALL -1 0 X 0 0\n4 1 WALL -1 0 X 0 0\n5 1 WALL -1 0 X 0 0\n7 1 WALL -1 0 X 0 0\n11 1 A -1 0 X 0 0\n14 1 WALL -1 0 X 0 0\n21 1 WALL -1 0 X 0 0\n2 2 HARVESTER 1 3 S 1 1\n3 2 WALL -1 0 X 0 0\n5 2 WALL -1 0 X 0 0\n6 2 WALL -1 0 X 0 0\n7 2 WALL -1 0 X 0 0\n9 2 WALL -1 0 X 0 0\n10 2 D -1 0 X 0 0\n11 2 D -1 0 X 0 0\n15 2 WALL -1 0 X 0 0\n16 2 WALL -1 0 X 0 0\n18 2 C -1 0 X 0 0\n20 2 WALL -1 0 X 0 0\n1 3 WALL -1 0 X 0 0\n2 3 B -1 0 X 0 0\n6 3 WALL -1 0 X 0 0\n9 3 WALL -1 0 X 0 0\n12 3 WALL -1 0 X 0 0\n13 3 WALL -1 0 X 0 0\n14 3 A -1 0 X 0 0\n16 3 WALL -1 0 X 0 0\n18 3 WALL -1 0 X 0 0\n20 3 WALL -1 0 X 0 0\n2 4 WALL -1 0 X 0 0\n4 4 WALL -1 0 X 0 0\n9 4 B -1 0 X 0 0\n12 4 WALL -1 0 X 0 0\n15 4 B -1 0 X 0 0\n18 4 WALL -1 0 X 0 0\n19 4 A -1 0 X 0 0\n21 4 C -1 0 X 0 0\n1 5 A -1 0 X 0 0\n5 5 WALL -1 0 X 0 0\n6 5 WALL -1 0 X 0 0\n9 5 WALL -1 0 X 0 0\n12 5 WALL -1 0 X 0 0\n15 5 WALL -1 0 X 0 0\n16 5 WALL -1 0 X 0 0\n18 5 BASIC 0 11 N 9 2\n19 5 HARVESTER 0 13 N 11 2\n20 5 A -1 0 X 0 0\n0 6 C -1 0 X 0 0\n2 6 A -1 0 X 0 0\n3 6 WALL -1 0 X 0 0\n6 6 B -1 0 X 0 0\n9 6 WALL -1 0 X 0 0\n12 6 B -1 0 X 0 0\n17 6 WALL -1 0 X 0 0\n18 6 BASIC 0 9 N 7 2\n19 6 WALL -1 0 X 0 0\n1 7 WALL -1 0 X 0 0\n3 7 WALL -1 0 X 0 0\n5 7 WALL -1 0 X 0 0\n7 7 A -1 0 X 0 0\n8 7 WALL -1 0 X 0 0\n9 7 WALL -1 0 X 0 0\n12 7 WALL -1 0 X 0 0\n15 7 WALL -1 0 X 0 0\n18 7 BASIC 0 7 N 5 2\n19 7 BASIC 0 5 N 4 2\n20 7 WALL -1 0 X 0 0\n1 8 WALL -1 0 X 0 0\n3 8 C -1 0 X 0 0\n5 8 WALL -1 0 X 0 0\n6 8 WALL -1 0 X 0 0\n10 8 D -1 0 X 0 0\n11 8 D -1 0 X 0 0\n12 8 WALL -1 0 X 0 0\n14 8 WALL -1 0 X 0 0\n15 8 WALL -1 0 X 0 0\n16 8 WALL -1 0 X 0 0\n18 8 WALL -1 0 X 0 0\n19 8 BASIC 0 4 N 2 2\n20 8 BASIC 0 17 N 15 2\n21 8 BASIC 0 19 N 17 2\n0 9 WALL -1 0 X 0 0\n7 9 WALL -1 0 X 0 0\n10 9 A -1 0 X 0 0\n14 9 WALL -1 0 X 0 0\n16 9 WALL -1 0 X 0 0\n17 9 WALL -1 0 X 0 0\n18 9 WALL -1 0 X 0 0\n19 9 ROOT 0 2 N 0 2\n20 9 BASIC 0 15 N 2 2\n0 10 WALL -1 0 X 0 0\n3 10 D -1 0 X 0 0\n4 10 D -1 0 X 0 0\n5 10 C -1 0 X 0 0\n6 10 C -1 0 X 0 0\n8 10 A -1 0 X 0 0\n9 10 WALL -1 0 X 0 0\n11 10 WALL -1 0 X 0 0\n21 10 WALL -1 0 X 0 0");
        Proteins.set_stock(0, 13, 0, 7);
        Moves m = new Moves(game, 1);
        while(Moves.moves.Count > 0)
            Console.WriteLine(Moves.moves.Dequeue());
        watch.Stop();
        var elapsedMs = watch.ElapsedMilliseconds;
        Console.WriteLine(elapsedMs);
        //Console.WriteLine("3 0 WALL -1 0 X 0 0\n6 0 WALL -1 0 X 0 0\n10 0 WALL -1 0 X 0 0\n15 0 WALL -1 0 X 0 0\n0 1 A -1 0 X 0 0\n6 1 B -1 0 X 0 0\n7 1 C -1 0 X 0 0\n9 1 C -1 0 X 0 0\n10 1 WALL -1 0 X 0 0\n14 1 B -1 0 X 0 0\n2 2 WALL -1 0 X 0 0\n3 2 WALL -1 0 X 0 0\n6 2 WALL -1 0 X 0 0\n7 2 WALL -1 0 X 0 0\n14 2 A -1 0 X 0 0\n2 3 WALL -1 0 X 0 0\n3 3 ROOT 1 1 N 0 1\n4 3 D -1 0 X 0 0\n5 3 WALL -1 0 X 0 0\n6 3 D -1 0 X 0 0\n8 3 WALL -1 0 X 0 0\n7 4 WALL -1 0 X 0 0\n9 4 D -1 0 X 0 0\n10 4 WALL -1 0 X 0 0\n11 4 D -1 0 X 0 0\n12 4 ROOT 0 2 N 0 2\n13 4 WALL -1 0 X 0 0\n1 5 A -1 0 X 0 0\n8 5 WALL -1 0 X 0 0\n9 5 WALL -1 0 X 0 0\n12 5 WALL -1 0 X 0 0\n13 5 WALL -1 0 X 0 0\n1 6 B -1 0 X 0 0\n5 6 WALL -1 0 X 0 0\n6 6 C -1 0 X 0 0\n8 6 C -1 0 X 0 0\n9 6 B -1 0 X 0 0\n15 6 A -1 0 X 0 0\n0 7 WALL -1 0 X 0 0\n5 7 WALL -1 0 X 0 0\n9 7 WALL -1 0 X 0 0\n12 7 WALL -1 0 X 0 0");
    }
}