using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseWork_WPF.information
{
    internal class Edge : IComparable<Edge>
    {
        public Point From { get; set; }
        public Point To { get; set; }

        public int EdgeWeight { get; set; }
        public int Throughput { get; set; }
        
        public Edge(Point from, Point to, int throughput, int weight)
        {
            From = from;

            To = to;

            Throughput = throughput;


            EdgeWeight = weight;
        }
        public override string ToString()
        {
            return $"({From}; {To})";
        }
        public int CompareTo(Edge other)
        {
            if (other == null) return 1;
            return EdgeWeight.CompareTo(other.EdgeWeight);
        }
    }
}
