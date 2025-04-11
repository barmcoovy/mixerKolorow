using Color = System.Drawing.Color;
using MauiColor = Microsoft.Maui.Graphics.Color;

namespace kolory
{
    public partial class MainPage : ContentPage
    {
        public int R = 0;
        public double G = 0;

        public double B = 0;

        public MainPage()
        {
            InitializeComponent();
        }

        private void ChangeColor(double R, double G, double B)
        {
            boxViewColor.BackgroundColor = MauiColor.FromRgb(R, G, B);
        }

        private void R_ValueChanged(object sender, ValueChangedEventArgs e)
        {
            R = Convert.ToInt32(e.NewValue) / 255;
            ChangeColor(R, G, B);
        }

        private void G_ValueChanged(object sender, ValueChangedEventArgs e)
        {
            G = Convert.ToInt32(e.NewValue) / 255;
            ChangeColor(R, G, B);

        }
        private void B_ValueChanged(object sender, ValueChangedEventArgs e)
        {
            B = Convert.ToInt32(e.NewValue) / 255;
            ChangeColor(R, G, B);

        }


        public double deltaE(Color color1, MauiColor color2)
        {
            var lab1 = RGBtoLAB(color1);

            var lab2 = RGBtoLAB(ColorParser.ToColor(color2));

            double deltaL = lab1.L - lab2.L;
            double deltaA = lab1.A - lab2.A;
            double deltaB = lab1.B - lab2.B;

            double deltaE = Math.Sqrt(Math.Pow(deltaL, 2) + Math.Pow(deltaA, 2) + Math.Pow(deltaB, 2));
            return deltaE;
        }

        public LAB RGBtoLAB(Color color)
        {
            XYZ xyz = RGBtoXYZ(color);
            LAB lab = XYZToLAB(xyz);
            return lab;
        }

        public XYZ RGBtoXYZ(Color color)
        {
            double r = color.R / 255.0;
            double g = color.G / 255.0;
            double b = color.B / 255.0;


            // zmienna R
            if (r > 0.4045)
            {
                r = Math.Pow(((r + 0.055) / 1.055), 2.4);
            }
            else
            {
                r = r / 12.92;
            }

            //zmienna G
            if (g > 0.4045)
            {
                g = Math.Pow(((g + 0.055) / 1.055), 2.4);
            }
            else
            {
                g = g / 12.92;
            }

            //zmienna B
            if (b > 0.4045)
            {
                b = Math.Pow(((b + 0.055) / 1.055), 2.4);
            }
            else
            {
                b = b / 12.92;
            }


            double X = (r * 0.4124564) + (g * 0.3575761) + (b * 0.1804375);
            double Y = (r * 0.2126729) + (g * 0.7151522) + (b * 0.0721750);
            double Z = (r * 0.0193339) + (g * 0.1191920) + (b * 0.9503041);


            XYZ xyz = new XYZ(X, Y, Z);

            return xyz;
        }


        public LAB XYZToLAB(XYZ xyz)
        {
            double Xr = 95.047;
            double Yr = 100.000;
            double Zr = 108.883;

            double x = xyz.X;
            double y = xyz.Y;
            double z = xyz.Z;

            double fx = F(x);
            double fy = F(y);
            double fz = F(z);


            double L = (166 * fy) - 16;
            double A = 500 * (fx - fy);
            double B = 200 * (fy - fz);

            LAB lab = new LAB(L, A, B);
            return lab;
        }


        public double F(double t)
        {
            if (t > 0.008856)
            {
                return Math.Pow(t, 1 / 3);
            }
            else
            {
                return ((7.787 * t) + (16 / 166));
            }
        }
    }

}