using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Color = System.Drawing.Color;
using MauiColor = Microsoft.Maui.Graphics.Color;

namespace kolory
{
    public static class ColorParser
    {
        public static Color ToColor(this MauiColor mauiColor)
        {
            return Color.FromArgb(
                (int)(mauiColor.Alpha * 255),
                (int)(mauiColor.Red * 255),
                (int)(mauiColor.Green * 255),
                (int)(mauiColor.Blue * 255)
            );
        }

        public static MauiColor ToMauiColor(this Color color)
        {
            return new MauiColor(color.R / 255f, color.G / 255f, color.B / 255f, color.A / 255f);
        }

    }
}
