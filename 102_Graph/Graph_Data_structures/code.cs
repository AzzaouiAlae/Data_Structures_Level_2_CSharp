using System.ComponentModel.Design;
using System.Runtime.CompilerServices;

namespace Graph_Data_structures;

class GraphMatrix2
{
    public enum enGraphDirectionType {Directed, unDirected}
    int [,] _adjacencyMatrix;
    Dictionary<string, int> _vertexDictionary;
    int _numberofVertices;
    enGraphDirectionType _GraphDirectionType = enGraphDirectionType.unDirected;
    public GraphMatrix2(List<string> vertices, enGraphDirectionType graphDirectionType)
    {
        _numberofVertices = vertices.Count;
        _adjacencyMatrix = new int[_numberofVertices, _numberofVertices];
        _vertexDictionary = new();
        for(int i = 0; i < _numberofVertices; i++)
            _vertexDictionary[vertices[i]] = i;
    }
    public void AddEdge(string source, string destination, int weight)
    {
        if(_vertexDictionary.ContainsKey(source) && _vertexDictionary.ContainsKey(destination))
        {
            int srcIndex = _vertexDictionary[source];
            int dstIndex = _vertexDictionary[destination];

            _adjacencyMatrix[srcIndex, dstIndex] = weight; 
            if(_GraphDirectionType == enGraphDirectionType.unDirected)
                _adjacencyMatrix[dstIndex, srcIndex] = weight;
        }
    }
    public bool isEdge(string source, string destination)
    {
        if(_vertexDictionary.ContainsKey(source) && _vertexDictionary.ContainsKey(destination))
        {
            int srcIndex = _vertexDictionary[source];
            int dstIndex = _vertexDictionary[destination];

            if(_adjacencyMatrix[srcIndex, dstIndex] > 0)
                return true;
        }
        return false;
    }
    public int GetInDegree(string vertex)
    {
        int degree = 0;
        if(_vertexDictionary.ContainsKey(vertex))
        {
            int vertexIndex = _vertexDictionary[vertex];
            for(int i = 0; i < _numberofVertices; i++)
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
            for(int i = 0; i < _numberofVertices; i++)
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
            for(int j = 0; j < _numberofVertices; j++)
                Console.Write($"\t{_adjacencyMatrix[i,j]}");
            Console.WriteLine();
            i++;
        }
    }
    public void BFS_print(string root)
    {
        Queue<string> queue = new();
        bool []visited = new bool[_numberofVertices]; 
        if(_vertexDictionary.ContainsKey(root))
        {
            queue.Enqueue(root);
            while(queue.Count > 0)
            {
                var current = queue.Dequeue();
                int rootIndex = _vertexDictionary[current];
                visited[rootIndex] = true;
                int i = 0;
                foreach(string vertex in _vertexDictionary.Keys)
                {
                    if(_adjacencyMatrix[rootIndex, i] > 0 && !visited[i])
                    {
                        visited[i] = true;
                        queue.Enqueue(vertex);
                    }
                    i++;
                }
                Console.WriteLine($"{current}");
            }
        }
    }
    
}

class GraphList2
{
    public enum enGraphDirectionType {Directed, unDirected}
    Dictionary<string, List<Tuple<string, int>>> _adjacencyList;
    Dictionary<string, int> _vertexDictionary;
    int _numberOfVertices;
    enGraphDirectionType _GraphDirectionType = enGraphDirectionType.unDirected;
    public GraphList2(List<string> vertices, enGraphDirectionType graphDirectionType)
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
            _adjacencyList[source].Add(new (destination, weight));
            if(_GraphDirectionType == enGraphDirectionType.unDirected)
                _adjacencyList[destination].Add(new (source, weight));
        }
    }
    public void RemoveEdge(string source, string destination)
    {
        if(_vertexDictionary.ContainsKey(source) && _vertexDictionary.ContainsKey(source))
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
    public void PrintGraph()
    {
        foreach(var vertex in _vertexDictionary.Keys)
        {
            Console.Write($"{vertex} ->");
            foreach(var edge in _adjacencyList[vertex])
                Console.Write($" {edge.Item1}({edge.Item2})");
            Console.WriteLine();
        }
    }




}