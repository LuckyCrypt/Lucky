using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseWork_WPF.information
{
    internal class Point
    {
        public int Number { get; set; }
        public string Name { get; set; }
        public string Discription { get; set; }
        public string Model { get; set; }
        public string X { get; set; }
        public string Y { get; set; }

        public Point(int number, string name, string discription, string model, string x, string y)
        {
            Number = number;
            Name = name;
            Discription = discription;
            Model = model;
            X = x;
            Y = y;
        }
        public Point(int number, string name, string discription, string x, string y)
        {
            Number = number;
            Name = name;
            Discription = discription;
            X = x;
            Y = y;
        }
        public Point(int number, string name, string x, string y)
        {
            Number = number;
            Name = name;
            X = x;
            Y = y;
        }
        public Point(string name, string x, string y)
        {
            Name = name;
            X = x;
            Y = y;
        }
        public override string ToString()
        {
            return Number.ToString();
        }
    }
}
