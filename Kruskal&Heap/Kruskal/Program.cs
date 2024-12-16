using System;
using System.Collections.Generic;

namespace Kruskal
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Graph graph = new Graph(4);

            graph.AddEdge(0, 1, 10);
            graph.AddEdge(0, 2, 6);
            graph.AddEdge(0, 3, 5);
            graph.AddEdge(1, 3, 15);
            graph.AddEdge(2, 3, 4);

            graph.KruskalMST();
        }
    }

    class Graph
    {
        private int vertices;
        private List<Edge> edges;

        public Graph(int vertices)
        {
            this.vertices = vertices;
            edges = new List<Edge>();
        }

        public void AddEdge(int source, int destination, int weight)
        {
            edges.Add(new Edge { Source = source, Destination = destination, Weight = weight });
        }

        private int Find(int[] parent, int i)
        {
            if (parent[i] != i)
                parent[i] = Find(parent, parent[i]);
            return parent[i];
        }

        private void Union(int[] parent, int[] rank, int x, int y)
        {
            int xRoot = Find(parent, x);
            int yRoot = Find(parent, y);

            if (rank[xRoot] < rank[yRoot])
                parent[xRoot] = yRoot;
            else if (rank[xRoot] > rank[yRoot])
                parent[yRoot] = xRoot;
            else
            {
                parent[yRoot] = xRoot;
                rank[xRoot]++;
            }
        }

        public void KruskalMST()
        {
            edges.Sort();

            int[] parent = new int[vertices];
            int[] rank = new int[vertices];
            for (int v = 0; v < vertices; v++)
            {
                parent[v] = v;
                rank[v] = 0;
            }

            List<Edge> mst = new List<Edge>();

            foreach (var edge in edges)
            {
                int x = Find(parent, edge.Source);
                int y = Find(parent, edge.Destination);

                if (x != y)
                {
                    mst.Add(edge);
                    Union(parent, rank, x, y);
                }
            }

            Console.WriteLine("Edges in the MST:");
            foreach (var edge in mst)
            {
                Console.WriteLine($"{edge.Source} -- {edge.Destination} == {edge.Weight}");
            }
        }

        class Edge : IComparable<Edge>
        {
            public int Source, Destination, Weight;

            public int CompareTo(Edge other)
            {
                return this.Weight.CompareTo(other.Weight);
            }
        }
    }

}
