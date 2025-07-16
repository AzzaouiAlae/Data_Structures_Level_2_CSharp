using System.Numerics;
using System.Security.Cryptography;

namespace GraphDS
{
    public class GraphList<T> where T :notnull
    {
        protected readonly Dictionary<T, List<Tuple<T, int>>> _adjcencyList;
        protected readonly int _count;
        public GraphList(List<T> vertcies)
        {
            _count = vertcies.Count;
            _adjcencyList = new(_count);
            foreach (var vertex in vertcies)
                _adjcencyList[vertex] = [];
        }
        public void AddEdge(T src, T dst, int wieght = 1)
        {
            if (_adjcencyList.TryGetValue(src, out var srcEdges) &&
                _adjcencyList.TryGetValue(dst, out var dstEdges))
            {
                srcEdges.Add(new(dst, wieght));
                dstEdges.Add(new(src, wieght));
            }
        }
        public void RemoveEdge(T src, T dst)
        {
            if (_adjcencyList.TryGetValue(src,out var srcEdges) &&
                _adjcencyList.TryGetValue(dst,out var dstEdges))
            {
                srcEdges.RemoveAll(edge => edge.Item1.Equals(dst));
                dstEdges.RemoveAll(edge => edge.Item1.Equals(src));
            }
        }
        public bool IsEdge(T src, T dst)
        {
            if (_adjcencyList.TryGetValue(src, out var srcEdges))
            {
                foreach(var edge in srcEdges)
                {
                    if (edge.Item1.Equals(dst))
                        return true;
                }
            }
            return false;
        }
        public int Weight(T src, T dst)
        {
            if (_adjcencyList.TryGetValue(src, out var srcEdges))
                return srcEdges.Count;
            return 0;
        }
        public int InDegree(T vertex)
        {
            int degree = 0;
            if (_adjcencyList.TryGetValue(vertex, out var srcEdges))
            {
                foreach(var node in _adjcencyList)
                {
                    foreach(var edge in node.Value)
                    {
                        if (edge.Item1.Equals(vertex))
                            degree++;
                    }
                }
            }
            return degree;
        }
        public int OutDegree(T vertex)
        {
            if (_adjcencyList.TryGetValue(vertex, out var edges))
                return edges.Count;
            return 0;
        }
        public void PrintGraph(string msg)
        {            
            foreach (var node in _adjcencyList)
            {
                Console.Write($"{node.Key}");
                foreach (var edge in node.Value)
                    Console.Write("  -->  " + edge.Item1.ToString());
                Console.WriteLine();
            }
        }
    }
    public class GraphMatrix<T> where T : notnull
    {
        readonly protected int [,]_adjacencyMatrix;

        readonly protected Dictionary<T, int> _vertexDict;

        readonly protected int _count;
        public GraphMatrix(List<T> vetecis)
        {
            _count = vetecis.Count;
            _vertexDict = new(_count);
            _adjacencyMatrix = new int[_count, _count];
            for (int i = 0; i < _count; i++)
                _vertexDict[vetecis[i]] = i;
        }
        public virtual void AddEdge(T src, T dst, int wieght = 1)
        {
            if (_vertexDict.TryGetValue(src, out int srcI) &&
                _vertexDict.TryGetValue(dst, out int dstI))
            {
                _adjacencyMatrix[srcI, dstI] = wieght;
                _adjacencyMatrix[dstI, srcI] = wieght;
            }
        }
        public void RemoveEdge(T src, T dst)
        {
            AddEdge(src, dst, 0);
        }
        public bool IsEdge(T src, T dst)
        {
            if (_vertexDict.TryGetValue(src, out int srcI) &&
                _vertexDict.TryGetValue(dst, out int dstI))
                return _adjacencyMatrix[srcI, dstI] > 0;
            return false;
        }
        public int Weight(T src, T dst)
        {
            if (_vertexDict.TryGetValue(src, out int srcI) &&
                _vertexDict.TryGetValue(dst, out int dstI))
                return _adjacencyMatrix[srcI, dstI];
            return 0;
        }
        public int InDegree(T vertex)
        {
            int degree = 0;
            if (_vertexDict.TryGetValue(vertex, out int VertexI))
            {
                for (int i = 0; i < _count; i++)
                {
                    if (_adjacencyMatrix[VertexI, i] > 0) 
                        degree++;
                }
            }
            return degree;
        }
        public int OutDegree(T vertex)
        {
            int degree = 0;
            if (_vertexDict.TryGetValue(vertex, out int vertexI))
            {
                for (int i =0; i < _count; i++)
                {
                    if (_adjacencyMatrix[i, vertexI] > 0)
                        degree++;
                }
            }
            return degree;
        }
        public void PrintGraph(string msg)
        {
            Console.WriteLine(msg);
            Console.Write("\t");
            foreach(var vertex in _vertexDict.Keys)
                Console.Write(vertex.ToString() + "\t");
            Console.WriteLine();
            foreach(var vertex in _vertexDict)
            {
                Console.Write(vertex.Key.ToString() + "\t");
                for (int i = 0;i < _count;i++)
                    Console.Write(_adjacencyMatrix[vertex.Value, i] + "\t");
                Console.WriteLine();
            }
        }
    }
    public class DirectedGraph<T>(List<T> vetecis) : GraphMatrix<T>(vetecis) where T : notnull
    {
        public override void AddEdge(T src, T dst, int wieght = 1)
        {
            if (_vertexDict.TryGetValue(src, out int srcI) &&
                _vertexDict.TryGetValue(dst, out int dstI))
                _adjacencyMatrix[srcI, dstI] = wieght;
        }
    }
    internal class Program
    {
        static void Main()
        {
            List<string> vertices = ["A", "B", "C", "D", "E"];
            GraphList<string> graph1 = new(vertices);

            graph1.AddEdge("A", "B");
            graph1.AddEdge("A", "C");
            graph1.AddEdge("B", "D");
            graph1.AddEdge("C", "D");
            graph1.AddEdge("B", "E");
            graph1.AddEdge("D", "E");
            graph1.PrintGraph("GraphMatrix 1:");


            DirectedGraph<string> Graph2 = new(["A", "B", "C", "D", "E"]);
            Graph2.AddEdge("A", "A");
            Graph2.AddEdge("A", "B");
            Graph2.AddEdge("A", "C");
            Graph2.AddEdge("B", "A", 5);
            Graph2.AddEdge("B", "E");
            Graph2.AddEdge("D", "C");
            Graph2.AddEdge("D", "B");
            Graph2.AddEdge("D", "E");
            Graph2.PrintGraph("\nGraph 2: ");
            Graph2.RemoveEdge("B", "A");
            Graph2.PrintGraph("\nGraph 2 After remove edge: ");

            GraphMatrix<string> Graph3 = new(["A", "B", "C", "D", "E"]);
            Graph3.AddEdge("A", "B", 5);
            Graph3.AddEdge("A", "C", 3);
            Graph3.AddEdge("B", "D", 12);
            Graph3.AddEdge("B", "E", 2);
            Graph3.AddEdge("C", "D", 10);
            Graph3.AddEdge("D", "E", 7);
            Graph3.PrintGraph("\nGraph 3: ");

            Console.WriteLine($"\nA => B = {Graph3.Weight("A", "B")}");
        }
    }
}
