using GMap.NET.WindowsPresentation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseWork_WPF.information
{
    internal class MPolygons
    {
        public GMapPolygon polygon;
        public string Names;

        public MPolygons(GMapPolygon p, string a)
        {
            polygon = p;

            Names = a;
        }
    }
}
