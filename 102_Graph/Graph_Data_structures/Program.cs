namespace Graph_Data_structures;

class Graph
{
    public enum enGraphDirectionType {Directed, unDirected}
    int [,] _adjacencyMatrix;
    Dictionary<string, int> _vertexDictionary;
    int _numberOfVertices;
    enGraphDirectionType _GraphDirectionType = enGraphDirectionType.unDirected;
    public Graph(List<string> vertices, enGraphDirectionType graphDirectionType)
    {
        _numberOfVertices = vertices.Count;
        _adjacencyMatrix = new int[_numberOfVertices,_numberOfVertices];
        _vertexDictionary = new();
        for(int i = 0; i < _numberOfVertices; i++)
            _vertexDictionary[vertices[i]] = i;
    }
    public void AddEdge(string source, string destination, int weight)
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
}
class Program
{
    static void Main(string[] args)
    {
        List<string> list = new() {"A", "B", "C", "D", "E"};
        Graph graph = new Graph(list, Graph.enGraphDirectionType.unDirected);
        graph.AddEdge("A", "B", 5);
        graph.AddEdge("A", "C", 3);
        graph.AddEdge("B", "D", 12);
        graph.AddEdge("B", "E", 2);
        graph.AddEdge("C", "A", 3);
        graph.AddEdge("D", "E", 7);
        graph.PrintGraph();
    }
}
