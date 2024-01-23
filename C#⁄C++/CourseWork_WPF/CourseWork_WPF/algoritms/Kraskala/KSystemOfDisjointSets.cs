using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicClasses
{
    class KSystemOfDisjointSets
    {
        public List<KSet> Sets;

        public KSystemOfDisjointSets()
        {
            Sets = new List<KSet>();
        }

        public void AddEdgeInSet(KEdge edge)
        {
            KSet setA = Find(edge.VertexA);
            KSet setB = Find(edge.VertexB);

            if (setA != null && setB == null)
            {
                setA.AddEdge(edge);
            }
            else if (setA == null && setB != null)
            {
                setB.AddEdge(edge);
            }
            else if (setA == null && setB == null)
            {
                KSet set = new KSet(edge);
                Sets.Add(set);
            }
            else if (setA != null && setB != null)
            {
                if (setA != setB)
                {
                    setA.Union(setB, edge);
                    Sets.Remove(setB);
                }
            }
        }

        public KSet Find(string vertex)
        {
            foreach (KSet set in Sets)
            {
                if (set.Contains(vertex)) return set;
            }
            return null;
        }
    }
}
