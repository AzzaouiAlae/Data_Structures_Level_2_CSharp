namespace Graph_Data_structures;

class Graph2
{
    public enum enGraphDirectionTYpe {Directed, unDirected}
    int [,] _adjacencyMatrix;
    Dictionary<string, int> _vertexDictionary;
    int _numberofVertices;
    enGraphDirectionTYpe _GraphDirectionType = enGraphDirectionTYpe.unDirected;
    
}