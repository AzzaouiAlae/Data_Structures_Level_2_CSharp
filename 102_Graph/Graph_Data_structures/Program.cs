using System.Runtime.CompilerServices;

namespace Graph_Data_structures;

class GraphMatrix
{
    public enum enGraphDirectionType {Directed, unDirected}
    byte [,] _adjacencyMatrix;
    Dictionary<string, byte > _vertexDictionary;
    byte _numberOfVertices;
    enGraphDirectionType _GraphDirectionType = enGraphDirectionType.unDirected;
    public GraphMatrix(List<string> vertices, enGraphDirectionType graphDirectionType)
    {
        _numberOfVertices = (byte)vertices.Count;
        _adjacencyMatrix = new byte[_numberOfVertices,_numberOfVertices];
        _vertexDictionary = new();
        for(byte i = 0; i < _numberOfVertices; i++)
            _vertexDictionary[vertices[i]] = i;
    }
    public void AddEdge(string source, string destination, byte weight)
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

            return _adjacencyMatrix[srcIndex,dstIndex] > 0;
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
                if(_adjacencyMatrix[i, vertexIndex] > 0)
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
                if(_adjacencyMatrix[vertexIndex, i] > 0)
                    degree++;
            }
        }
        return degree;
    }
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
                Console.Write($"\t{_adjacencyMatrix[i, j]}");
            Console.WriteLine();
            i++;
        }
    }
    public void BFS_print(string root)
    {
        Queue<string> queue = new();
        List<string> visited = new();
        if(_vertexDictionary.ContainsKey(root))
        {
            visited.Add(root);
            queue.Enqueue(root);
            while(queue.Count > 0)
            {
                var current = queue.Dequeue();
                int rootIndex = _vertexDictionary[current];
                int i = 0;
                foreach(string vertex in _vertexDictionary.Keys)
                {
                    if(_adjacencyMatrix[rootIndex, i] > 0 && !visited.Contains(vertex))
                    {
                        visited.Add(vertex);
                        queue.Enqueue(vertex);
                    }
                    i++;
                }
                Console.WriteLine($"{current}");
            }
        }
    }
    public void DFS_print(string root)
    {
        Stack<string> stack = new();
        bool [] visited = new bool[_numberOfVertices];
        if(_vertexDictionary.ContainsKey(root))
        {
            int rootIndex = _vertexDictionary[root];
            visited[rootIndex] = true;
            stack.Push(root);
            while(stack.Count > 0)
            {
                var current = stack.Pop();
                rootIndex = _vertexDictionary[current];
                int i = 0;
                foreach(string vertex in _vertexDictionary.Keys)
                {
                    if(_adjacencyMatrix[rootIndex, i] > 0 && !visited[i])
                    {
                        visited[i] = true;
                        stack.Push(vertex);
                    }
                    i++;
                }
                Console.Write($"{current} -> ");
            }
        }
    }
    public struct Dijkstart_node
    {
        public string vertexName;
        public int vertexIndex;
        public byte distance;
        public int prev_node_index;
    }
    public Dictionary<int, Dijkstart_node>? Dijkstart(string StratVertex)
    {
        if(!_vertexDictionary.ContainsKey(StratVertex))
            return null;
        Dictionary<int, Dijkstart_node> Dijkstart_table = new();
        Dijkstart_table.Add(_vertexDictionary[StratVertex], create_Dijkstart_node(0, -1, StratVertex, _vertexDictionary[StratVertex]));
        bool []visited = new bool[_numberOfVertices];
        for(byte count = 0; count < _numberOfVertices - 1; count++)
        {
            int minIndex = GetminiIndex(visited, Dijkstart_table);
            if(minIndex < 0)
                continue;
            visited[minIndex] = true;
            for(int i = 0; i < _numberOfVertices; i++)
            {
                if(!visited[i] && _adjacencyMatrix[minIndex, i] > 0)
                {
                    if(!Dijkstart_table.ContainsKey(i))
                        Dijkstart_table.Add(i, create_Dijkstart_node((byte)(_adjacencyMatrix[minIndex, i] + Dijkstart_table[minIndex].distance), minIndex, GetVertexName(i), i));
                    else if(Dijkstart_table[i].distance > _adjacencyMatrix[minIndex, i] + Dijkstart_table[minIndex].distance)
                        Dijkstart_table[i] = create_Dijkstart_node((byte)(_adjacencyMatrix[minIndex, i] + Dijkstart_table[minIndex].distance), minIndex, GetVertexName(i), i);
                }
            }
        }
        return Dijkstart_table;
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
            stack.Push(current_node);
            if(current_node.vertexName == start)
                break;
            current_node = result[current_node.prev_node_index];
        }
        foreach(var node in stack)
        {
            Console.Write($"{node.vertexName}({node.distance}) -> ");
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

    Dijkstart_node create_Dijkstart_node(byte distance, int prev_node_index, string vertexName, int vertexIndex)
    {
        Dijkstart_node node = new();
        node.distance = distance;
        node.prev_node_index = prev_node_index;
        node.vertexName = vertexName;
        node.vertexIndex = vertexIndex;
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
}
public class GraphList
{
    public enum enGraphDirectionType {Directed, unDirected}
    Dictionary<string, List<Tuple<string, int>>> _adjacencyList;
    Dictionary<string, int> _vertexDictionary;
    int _numberOfVertices;
    enGraphDirectionType _GraphDirectionType = enGraphDirectionType.unDirected;
    public GraphList(List<string> vertices, enGraphDirectionType graphDirectionType)
    {
        _GraphDirectionType = graphDirectionType;
        _numberOfVertices = vertices.Count;
        _adjacencyList = new();
        _vertexDictionary = new();
        for(int i = 0; i < vertices.Count; i++)
        {
            _vertexDictionary[vertices[i]] = i;
            _adjacencyList[vertices[i]] = new();
        }
    }
    public void AddEdge(string source, string destination, int weight)
    {
        if(_vertexDictionary.ContainsKey(source) && _vertexDictionary.ContainsKey(destination))
        {
            RemoveEdge(source, destination);
            _adjacencyList[source].Add(new(destination, weight));
            if(_GraphDirectionType == enGraphDirectionType.unDirected)
                _adjacencyList[destination].Add(new(source, weight));
        }
    }
    public void RemoveEdge(string source, string destination)
    {
        if(_vertexDictionary.ContainsKey(source) && _vertexDictionary.ContainsKey(destination))
        {
            _adjacencyList[source].RemoveAll(edge => edge.Item1 == destination);
            if(_GraphDirectionType == enGraphDirectionType.unDirected)
                _adjacencyList[destination].RemoveAll(edge => edge.Item1 == source);
        }
    }
    public bool IsEdge(string source, string destination)
    {
        if(_vertexDictionary.ContainsKey(source) && _vertexDictionary.ContainsKey(destination))
        {
            foreach(var edge in _adjacencyList[source])
            {
                if(edge.Item1 == destination)
                    return true;
            }
        }
        return false;
    }
    public void PrintGraph(string message = "")
    {
        Console.WriteLine($"{message}");
        foreach(var vertex in _adjacencyList)
        {
            Console.Write(vertex.Key + " -> ");
            foreach(var edge in vertex.Value)
                Console.Write($"{edge.Item1}({edge.Item2}) ");
            Console.WriteLine();
        }
    }
    public int GetInDegree(string vertex)
    {
        int inDegree = 0;
        if(_vertexDictionary.ContainsKey(vertex))
        {
            foreach(var source in _adjacencyList)
            {
                foreach(var edge in source.Value)
                {
                    if(edge.Item1 == vertex)
                        inDegree++;
                }
            }
        }
        return inDegree;
    }
    public int GetOutDegree(string vertex)
    {
        int outDegree = 0;
        if(_vertexDictionary.ContainsKey(vertex))
            outDegree = _adjacencyList[vertex].Count;
        return outDegree;
    }
}
class Program
{
    static void Main(string[] args)
    {
        List<string> list = new() {"A", "B", "C", "D", "E"};
        GraphMatrix graph = new GraphMatrix(list, GraphMatrix.enGraphDirectionType.unDirected);
        graph.AddEdge("A", "B", 5);
        graph.AddEdge("A", "C", 3);
        graph.AddEdge("B", "D", 12);
        graph.AddEdge("B", "E", 2);
        graph.AddEdge("C", "A", 3);
        graph.AddEdge("C", "B", 1);
        graph.AddEdge("D", "E", 7);
        //graph.BFS_print("A");
        //graph.DFS_print("A");
        graph.GetPath("A", "D");
        
    }
}
