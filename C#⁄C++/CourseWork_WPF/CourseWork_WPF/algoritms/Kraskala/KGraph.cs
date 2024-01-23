using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace LogicClasses
{
    public class KGraph : IEnumerable<KEdge>
    {
        private List<KEdge> _graph;

        public KGraph()
        {
            _graph = new List<KEdge>();
        }

        public KGraph(KEdge val)
        {
            KEdge[] value = new KEdge[] { val };

            _graph = new List<KEdge>(value);
        }

        public void Add(KGraph graph)
        {
            foreach (KEdge edge in graph)
            {
                _graph.Add(edge);
            }
        }

        public void Add(KEdge edge)
        {
            _graph.Add(edge);
        }

        public int GetWeight()
        {
            int weight = 0;
            foreach (KEdge edge in _graph)
            {
                weight += edge.EdgeWeight;   
            }
            return weight;
        }

        public KGraph FindMinimumSpanningTree()
        {
            Sort();
            var disjointSets = new KSystemOfDisjointSets();
            foreach (KEdge edge in _graph)
            {
                disjointSets.AddEdgeInSet(edge);
            }

            return disjointSets.Sets.First().SetGraph;
        }

        public override string ToString()
        {
            string result = string.Empty;

            foreach (KEdge edge in _graph)
            {
                result += $"{edge.VertexA} {edge.VertexB} {edge.EdgeWeight}\n";
            }

            return result;
        }

        public void Sort()
        {
            _graph.Sort();
        }

        public IEnumerator<KEdge> GetEnumerator()
        {
            return _graph.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _graph.GetEnumerator();
        }
    }
}
