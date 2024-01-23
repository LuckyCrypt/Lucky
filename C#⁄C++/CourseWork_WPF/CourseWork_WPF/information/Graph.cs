using CourseWork_WPF.algoritms;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;

namespace CourseWork_WPF.information
{
    internal class Graph : IEnumerable<Edge>
    {
        public static List<Point> Vertexes = new List<Point>();
        public static List<Edge> Edges = new List<Edge>();

        public int VertexCount => Vertexes.Count;
        public int EdgeCount => Edges.Count;
        public static List<MPolygons> MemoryPolygon = new List<MPolygons>();

        public Graph()
        {
            Edges = new List<Edge>();
        }

        public Graph(Edge val)
        {
            Edge[] value = new Edge[] { val };

            Edges = new List<Edge>(value);
        }
        public void DeleteVert(Point vertex)
        {
            Vertexes.Remove(vertex);
        }
        public List<Edge> ReturnEdge()
        {
            var list = new List<Edge>();
            return list;
        }
        public void AddVert(Point vertex)
        {
            Vertexes.Add(vertex);
        }
        public void AddEdge(Edge edge)
        {
            Edges.Add(edge);
        }
        public void DeletEdge(Edge edge)
        {
            Edges.Remove(edge);
        }
        public static int[,] GetMatrix()
        {
            var matrix = new int[Vertexes.Count, Vertexes.Count];
            foreach (var edge in Edges)
            {
                var row = edge.From.Number;
                var column = edge.To.Number;

                matrix[row, column] = edge.EdgeWeight;
            }
            return matrix;
        }

        public List<Point> GetVertexLists(Point vertex)
        {
            var result = new List<Point>();

            foreach (var edge in Edges)
            {
                if (edge.From == vertex)
                {
                    result.Add(edge.To);
                }
            }
            return result;

        }
        public bool Wave(Point start, Point end)
        {
            var list = new List<Point>()
            {
                start
            };

            for (int i = 0; i < list.Count; i++)
            {
                var vertex = list[i];
                foreach (var v in GetVertexLists(vertex))
                {
                    if (!list.Contains(v))
                    {
                        list.Add(v);
                    }
                }
            }


            return list.Contains(end);
        }
       
/*        public List<Edge> GetInfo()
        {
            foreach (var v in Edge.)
            {
                if (!Lis.Contains(v))
                {
                    list.Add(v);
                }
            }

        }*/
        public IEnumerator<Edge> GetEnumerator()
        {
            return Edges.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return Edges.GetEnumerator();
        }
        public double GetWeight()
        {
            double weight = 0;
            foreach (Edge edge in Edges)
            {
                weight += edge.EdgeWeight;
            }
            return weight;
        }
        public static void Sort()
        {
            Edges.Sort();
        }
        public void Add(Graph graph)
        {
            foreach (Edge edge in graph)
            {
                Edges.Add(edge);
            }
        }

        public void Add(Edge edge)
        {
            Edges.Add(edge);
        }
        
    }
    
}
