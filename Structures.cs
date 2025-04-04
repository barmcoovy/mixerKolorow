using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kolory
{
    public struct XYZ
    {
        public double X, Y, Z;
        public XYZ(double x, double y, double z)
        {
            X = x; Y = y; Z = z;
        }
    }
    public struct LAB
    {
        public double L, A, B;
        public LAB(double l, double a, double b)
        {
            L = l; A = a; B = b;
        }
    }
}
