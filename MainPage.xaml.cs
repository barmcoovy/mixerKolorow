using Color = System.Drawing.Color;
using MauiColor = Microsoft.Maui.Graphics.Color;

namespace kolory
{
    public partial class MainPage : ContentPage
    {
        public double R = 0;
        public double G = 0;
        public double B = 0;

        Random random = new Random();
        MauiColor mauiColor;
        MauiColor mauiColorRgbBoxView;

        public MainPage()
        {
            InitializeComponent();
             mauiColor = MauiColor.FromRgb(random.Next(1, 255), random.Next(1, 255), random.Next(1, 255));

            boxViewRandomColor.BackgroundColor = mauiColor;
        }

        private void Slider_ValueChanged(object sender, ValueChangedEventArgs e)
        {
            mauiColorRgbBoxView = MauiColor.FromRgb(rSlider.Value / 255, gSlider.Value / 255, bSlider.Value / 255); ;
            boxViewColor.BackgroundColor = mauiColorRgbBoxView;
;
        }



        private void Submit_Clicked(object sender, EventArgs e)
        {
            double delta = DeltaE(mauiColor, mauiColorRgbBoxView);
            string wynik = "";
            
            if(delta>0 && delta < 1)
            {
                deltaLbl.Text = "Rożnica prawie nie zauważalna";
            }
            else if(delta>1 && delta<2){
                deltaLbl.Text = "Bardzo mała różnica, trudna do wychwycenia";

            }
            else if(delta> 2 && delta < 3)
            {
                deltaLbl.Text = "Mała różnica, widoczna przy porównaniu obok siebie";

            }
            else if(delta>3 && delta < 5)
            {
                deltaLbl.Text = "Widoczna różnica kolorów";

            }
            else if (delta>5 && delta < 10)
            {
                deltaLbl.Text = "Znaczna różnica";

            }
            else
            {
                deltaLbl.Text = "Kolory zupełnie inne";

            }
        }



        public double DeltaE(MauiColor color1, MauiColor color2)
        {
            var lab1 = RGBtoLAB(ColorParser.ToColor(color1));
            var lab2 = RGBtoLAB(ColorParser.ToColor(color2));

            double deltaL = lab1.L - lab2.L;
            double deltaA = lab1.A - lab2.A;
            double deltaB = lab1.B - lab2.B;

            return Math.Sqrt(deltaL * deltaL + deltaA * deltaA + deltaB * deltaB);
        }

        public LAB RGBtoLAB(Color color)
        {
            XYZ xyz = RGBtoXYZ(color);
            return XYZtoLAB(xyz);
        }

        public XYZ RGBtoXYZ(Color color)
        {
            double r = color.R / 255.0;
            double g = color.G / 255.0;
            double b = color.B / 255.0;

            r = (r > 0.04045) ? Math.Pow((r + 0.055) / 1.055, 2.4) : r / 12.92;
            g = (g > 0.04045) ? Math.Pow((g + 0.055) / 1.055, 2.4) : g / 12.92;
            b = (b > 0.04045) ? Math.Pow((b + 0.055) / 1.055, 2.4) : b / 12.92;

            double X = (r * 0.4124564) + (g * 0.3575761) + (b * 0.1804375);
            double Y = (r * 0.2126729) + (g * 0.7151522) + (b * 0.0721750);
            double Z = (r * 0.0193339) + (g * 0.1191920) + (b * 0.9503041);

            return new XYZ(X * 100, Y * 100, Z * 100); // skalowanie do 0-100
        }

        public LAB XYZtoLAB(XYZ xyz)
        {
            double Xr = 95.047;
            double Yr = 100.000;
            double Zr = 108.883;

            double x = xyz.X / Xr;
            double y = xyz.Y / Yr;
            double z = xyz.Z / Zr;

            double fx = f(x);
            double fy = f(y);
            double fz = f(z);

            double L = (116 * fy) - 16;
            double A = 500 * (fx - fy);
            double B = 200 * (fy - fz);

            return new LAB(L, A, B);
        }

        private double f(double t)
        {
            return (t > 0.008856) ? Math.Pow(t, 1.0 / 3.0) : ((7.787 * t) + (16.0 / 116.0));
        }

    }

}