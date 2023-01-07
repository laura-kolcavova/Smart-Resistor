using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace Smart_resistor
{
    public static class MathHelper
    {
       public static double RemoveSufix(string value)
        {
            //Získaní skutečné hodnoty multiplieru - odstranění prefixu
            int sufix = 1;
            if (value.Contains("K")) sufix = 1000;
            else if (value.Contains("M")) sufix = 1000000;
            else if (value.Contains("G")) sufix = 1000000000;

            if (sufix != 1) value = value.Remove(value.Length - 1);

            return double.Parse(value, CultureInfo.InvariantCulture) * sufix;
        }

        public static string AddSufix(double value)
        {
            string sufix = "";

            if (value > 1000000000)
            {
                value /= 1000000000;
                sufix = "G";
            }
            else if (value > 1000000)
            {
                value /= 1000000;
                sufix = "M";
            }
            else if (value > 1000)
            {
                value /= 1000;
                sufix = "K";
            }

            string _value = value.ToString().Replace(',', '.');
            return _value + ' ' + sufix;
        }
    }
}
