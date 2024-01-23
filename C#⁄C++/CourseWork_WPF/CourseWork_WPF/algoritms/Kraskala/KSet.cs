using System.Collections.Generic;

namespace LogicClasses
{
    public class KSet
    {
        public KGraph SetGraph;
        public List<string> Vertices;

        public KSet(KEdge edge)
        {
            SetGraph = new KGraph(edge);

            Vertices = new List<string>();
            Vertices.Add(edge.VertexA);
            Vertices.Add(edge.VertexB);
        }

        public void Union(KSet set, KEdge connectingEdge)
        {
            SetGraph.Add(set.SetGraph);
            Vertices.AddRange(set.Vertices);
            SetGraph.Add(connectingEdge);
        }

        public void AddEdge(KEdge edge)
        {
            SetGraph.Add(edge);
            Vertices.Add(edge.VertexA);
            Vertices.Add(edge.VertexB);
        }

        public bool Contains(string vertex)
        {
            return Vertices.Contains(vertex);
        }
    }
}
