using System.ComponentModel;
using System.Data;
using System.Diagnostics.CodeAnalysis;
public enum enDirection{
        E = 'E', W = 'W', N = 'N', S = 'S', X = 'X'
    } 

public enum enType {
    WALL = 'W', ROOT = 'R', BASIC = 'B', TENTACLE = 'T', HARVESTER = 'H', SPORER = 'S', A = 'A', B = 'b', C = 'C', D = 'D', non
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
public class MyEntitys
{
    public Dictionary<string, Cell> nodes;
    public Dictionary<string, Cell> roots;
    public Dictionary<string, Cell> HARVESTER;
    public Dictionary<string, Cell> TENTACLE;
    public Dictionary<string, Cell> SPORE;
    public Dictionary<string, Cell> BASIC;
    public Dictionary<string, Cell> A;
    public Dictionary<string, Cell> B;
    public Dictionary<string, Cell> C;
    public Dictionary<string, Cell> D;
    public Dictionary<string, Cell> p;
    public Dictionary<string, Cell> organ;
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
public class GraphMatrix
{
    public MyEntitys myEntitys;
    int _max_x, _max_y;
    public enum enGraphDirectionType {Directed, unDirected}
    Wieght [,] _adjacencyMatrix;
    Dictionary<string, int> _vertexDictionary;
    int _numberOfVertices;
    enGraphDirectionType _GraphDirectionType = enGraphDirectionType.unDirected;
    public GraphMatrix(int max_x, int max_y)
    {
        _max_x = max_x;
        _max_y = max_y;
        _numberOfVertices = max_x * max_y;
        _adjacencyMatrix = new Wieght[_numberOfVertices,_numberOfVertices];
        _vertexDictionary = new();
        myEntitys = new();
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
        weight.SPORE = (byte)enDirection.E;
        for(int i = 0; i < _max_x; i++)
        {
            if(i == x || i == x - 1 || i == x - 2 || i == x + 1 || i == x + 2){
                weight.SPORE = (byte)enDirection.W;
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
    public void DeleteAdge(int x, int y)
    {
        Wieght weight = new();
        int sourceIndex = _vertexDictionary[$"{x} {y}"];
        for(int i = 0; i < _numberOfVertices; i++)
        {
            _adjacencyMatrix[sourceIndex, i] = weight;
            if(_GraphDirectionType == enGraphDirectionType.unDirected)
                _adjacencyMatrix[i, sourceIndex] = weight;
        }
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
            for(int i = 0; i < _numberOfVertices; i++)
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
            for(int i = 0; i < _numberOfVertices; i++)
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
        bool []visited = new bool[_numberOfVertices];
        for(byte count = 0; count < _numberOfVertices - 1; count++)
        {
            int minIndex = GetminiIndex(visited, Dijkstart_table);
            if(minIndex < 0)
                continue;
            visited[minIndex] = true;
            for(int i = 0; i < _numberOfVertices; i++)
            {
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
    public void GetPath(string start, string end)
    {
        Dictionary<int, Dijkstart_node>? result = Dijkstart(start);
        Stack<Dijkstart_node> stack = new();
        if(result == null)
            return;
        if(!result.ContainsKey(_vertexDictionary[end]))
            return;
        var current_node = result[_vertexDictionary[end]];
        while(true)
        {
            if(current_node.vertexName == start)
                break;
            stack.Push(current_node);
            current_node = result[current_node.prev_node_index];
        }
        while(stack.Count > 0)
        {
            Dijkstart_node node = stack.Pop();
            Console.WriteLine($"GROW {myEntitys.organ[node.prev_node_Name].organId} {node.vertexName} BASIC");
        }
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
        return node;
    }
    string GetVertexName(int vertexIndex)
    {
        foreach(var vertex in _vertexDictionary)
        {
            if(vertex.Value == vertexIndex)
                return vertex.Key;
        }
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
            for(int j = 0; j < _numberOfVertices; j++)
                Console.Write($"\t{_adjacencyMatrix[i, j].BASIC},{_adjacencyMatrix[i, j].SPORE_ROOT}");
            Console.WriteLine();
            i++;
        }
    }
}