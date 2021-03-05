using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Smart_resistor
{
    public static class ColorButton
    {
        /*public string TableValue
        {
            get { return (string)GetValue(TableValueProperty); }
            set { SetValue(TableValueProperty, value); }
        }

        public static readonly DependencyProperty TableValueProperty = 
            DependencyProperty.Register("MyProperty", typeof(int), typeof(ColorButton), new UIPropertyMetadata(0));
            */

        public static readonly DependencyProperty TableValue = DependencyProperty.RegisterAttached("TableValue",
            typeof(string), typeof(ColorButton), new FrameworkPropertyMetadata(null));

        public static string GetTableValue(UIElement element)
        {
            if (element == null)
                throw new ArgumentNullException("element");
            return (string)element.GetValue(TableValue);
        }
        public static void SetTableValue(UIElement element, string value)
        {
            if (element == null)
                throw new ArgumentNullException("element");
            element.SetValue(TableValue, value);
        }
    }
}
