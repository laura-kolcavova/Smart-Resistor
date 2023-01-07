using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Smart_resistor
{
    public static class ValuesTable
    {
        //Hodnoty - řádek = typ proužku), sloupcec = barva
                                                    // BLAC    BROW    RED     ORAN    YELL    GREE    BLUE    PURP   GREY    WHIT    GOLD     SILV
        public static readonly string[] Digit1 =     { "0",    "1",    "2",    "3",    "4",    "5",    "6",    "7",   "8",    "9",    null,    null };
        public static readonly string[] Digit2 =     { "0",    "1",    "2",    "3",    "4",    "5",    "6",    "7",   "8",    "9",    null,    null };
        public static readonly string[] Digit3 =     { "0",    "1",    "2",    "3",    "4",    "5",    "6",    "7",   "8",    "9",    null,    null };
        public static readonly string[] Multiplier = { "1",    "10",   "100",  "1K",   "10K",  "100K", "1M",   "10M", null,   null,   "0.1",   "0.01" };
        public static readonly string[] Tolerance =  { null,   "1",    "2",    null,   null,   "0.5",  "0.25", "0.1", "0.05", null,   "5",     "10" };
        public static readonly string[] TRC =        { null,   "100",  "50",   "15",   "25",   null,   "10",   "5",   null,   null,   null,    null };

        //Barvy prouzku
        public static readonly Color[] StripColors = { Colors.Black, Colors.SaddleBrown, Colors.Red, Colors.DarkOrange, Colors.Yellow,
            Colors.Green, Colors.DarkBlue, Colors.Orchid, Colors.DimGray, Colors.White, Colors.DarkGoldenrod, Colors.Silver };
        
        //Sloupce - typy proužků
        public enum Stripes { Digit1, Digit2, Digit3, Multiplier, Tolerance, TRC };

        public static string GetValue(Stripes strip, Color color)
        {
            int col = Array.IndexOf(StripColors, color);

            if (col == -1) return null;

            switch(strip)
            {
                case Stripes.Digit1: return Digit1[col];
                case Stripes.Digit2: return Digit2[col];
                case Stripes.Digit3: return Digit3[col];
                case Stripes.Multiplier: return Multiplier[col];
                case Stripes.Tolerance: return Tolerance[col];
                case Stripes.TRC: return TRC[col];
                default: return null;
            }
        }
    }
}
