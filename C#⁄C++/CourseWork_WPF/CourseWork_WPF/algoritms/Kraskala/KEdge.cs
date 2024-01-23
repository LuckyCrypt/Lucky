
using System;

namespace LogicClasses
{
    public class KEdge : IComparable<KEdge>
    {
        public int EdgeWeight { get; set; }
        public string VertexA { get; set; }
        public string VertexB { get; set; }


        public KEdge(string vertexA, string vertexB, int weight)
        {
            VertexA = vertexA;
            VertexB = vertexB;
            EdgeWeight = weight;
        }

        public int CompareTo(KEdge other)
        {
            if (other == null) return 1;
            return EdgeWeight.CompareTo(other.EdgeWeight);
        }
    }
}
