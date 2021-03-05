using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using static Smart_resistor.ValuesTable;

namespace Smart_resistor
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //Barvy prouzku
        private Color[] strip_colors;

        //Prouzky, kterym se nastavi barva
        private Rectangle[] strips;

        //Enum pocet prouzku
        public enum StripsCount
        {
            _3, _4, _5, _6
        };

        //Soucasny pocet prouzku
        private StripsCount strips_count;

        public MainWindow()
        {
            InitializeComponent();

            //Prouzky, kterym se nastavi barva
            strips = new Rectangle[] { strip_out_0, strip_0, strip_1, strip_2, strip_3, strip_out_1 };

            //Barvz prouzku z tabulky hodnot
            strip_colors = ValuesTable.StripColors;

            //Inicializuje tlacitka barev
            InitColorButtons();

            //Nastavi defaultni pocet prouzku
            SetCountOfStrips(StripsCount._5);

            //Nastavi defaultni barvy prouzku
            strip_out_0.Fill = new SolidColorBrush(strip_colors[1]);
            strip_0.Fill = new SolidColorBrush(strip_colors[5]);
            strip_1.Fill = new SolidColorBrush(strip_colors[0]);
            strip_2.Fill = new SolidColorBrush(strip_colors[1]);
            strip_3.Fill = new SolidColorBrush(strip_colors[1]);
            strip_out_1.Fill = new SolidColorBrush(strip_colors[1]);

            //Nastaví hodnotu resistoru
            ChangeResistorValue();
        }

        private void InitColorButtons()
        {
            foreach (StackPanel sp in sp_colors.Children)
            {
                foreach (Button b in sp.Children.OfType<Grid>().First().Children)
                {
                    //Set color of button
                    b.Background = new SolidColorBrush(strip_colors[Grid.GetRow(b)]);
                    //Add click event
                    b.AddHandler(Button.ClickEvent, new RoutedEventHandler(SetColorOfStrip));
                }
            } 
        }

        //Schova vsechny tlacitka barev
        private void SetOffAllColorButtons()
        {
            foreach(StackPanel sp in sp_colors.Children)
            {
                sp.Visibility = Visibility.Collapsed;
            }
        }

        //Schova vsechny prouzky
        private void SetOffAllStrips()
        {
            foreach(Rectangle r in sp_resistor.Children.OfType<Rectangle>())
            {
                r.Visibility = Visibility.Hidden;
            }
        }

        //Nastavi pocet prouzku a tlacitka barev
        private void SetCountOfStrips(StripsCount count)
        {
            SetOffAllColorButtons();
            SetOffAllStrips();

            strips_count = count;

            //Zobrazi sloupce barev
            sp_1st_digit.Visibility = Visibility.Visible;
            sp_2nd_digit.Visibility = Visibility.Visible;
            sp_multiplier.Visibility = Visibility.Visible;

            //Zobrazi prouzky resistoru
            strip_out_0.Visibility = strip_0.Visibility = strip_1.Visibility = Visibility.Visible;

            foreach (Button b in sp_countOfStrips_buttons.Children) b.IsEnabled = true;

            if(count == StripsCount._4)
            {
                bt_4_strips.IsEnabled = false;

                //Zobrazi sloupce barev
                sp_tolerance.Visibility = Visibility.Visible;

                //Zobrazi prouzky resistoru
                strip_3.Visibility = Visibility.Visible;
            }
            else if(count == StripsCount._5)
            {
                bt_5_strips.IsEnabled = false;

                //Zobrazi sloupce barev
                sp_tolerance.Visibility = sp_3st_digit.Visibility = Visibility.Visible;

                //Zobrazi prouzky resistoru
                strip_2.Visibility = strip_3.Visibility = Visibility.Visible;
            }
            else if(count == StripsCount._6)
            {
                bt_6_strips.IsEnabled = false;

                //Zobrazi sloupce barev
                sp_tolerance.Visibility = sp_3st_digit.Visibility = sp_TCR.Visibility = Visibility.Visible;

                //Zobrazi prouzky resistoru
                strip_2.Visibility = strip_3.Visibility = strip_out_1.Visibility = Visibility.Visible;
            }
            else
            {
                bt_3_strips.IsEnabled = false;
            }
        }

        //Přepočítá hodnotu resiztoru dle barev proužků
        private void ChangeResistorValue()
        {
            string s_digit1 = "";
            string s_digit2 = "";
            string s_digit3 = "";
            string s_multiplier = "";
            string s_tolerance = "20";
            string s_trc = "";

            s_digit1 = ValuesTable.GetValue(Stripes.Digit1, GetColorOfStrip(strip_out_0));
            s_digit2 = ValuesTable.GetValue(Stripes.Digit2, GetColorOfStrip(strip_0));

            if (strips_count == StripsCount._3)
            {
                s_multiplier = ValuesTable.GetValue(Stripes.Multiplier, GetColorOfStrip(strip_1));
            }
            else if (strips_count == StripsCount._4)
            {
                s_multiplier = ValuesTable.GetValue(Stripes.Multiplier, GetColorOfStrip(strip_1));
                s_tolerance = ValuesTable.GetValue(Stripes.Tolerance, GetColorOfStrip(strip_3));
            }
            else if (strips_count == StripsCount._5)
            {
                s_digit3 = ValuesTable.GetValue(Stripes.Digit3, GetColorOfStrip(strip_1));
                s_multiplier = ValuesTable.GetValue(Stripes.Multiplier, GetColorOfStrip(strip_2));
                s_tolerance = ValuesTable.GetValue(Stripes.Tolerance, GetColorOfStrip(strip_3));
            }
            else if (strips_count == StripsCount._6)
            {
                s_digit3 = ValuesTable.GetValue(Stripes.Digit3, GetColorOfStrip(strip_1));
                s_multiplier = ValuesTable.GetValue(Stripes.Multiplier, GetColorOfStrip(strip_2));
                s_tolerance = ValuesTable.GetValue(Stripes.Tolerance, GetColorOfStrip(strip_3));
                s_trc = ValuesTable.GetValue(Stripes.TRC, GetColorOfStrip(strip_out_1));
                s_trc += "ppm/k";
            }

            //Získaní skutečné hodnoty multiplieru - odstranění sufixu
            double multiplier = MathHelper.RemoveSufix(s_multiplier);

            //Vypočtení skutečné hodnoty resistoru
            string s_value = s_digit1 + s_digit2 + s_digit3;
            double value = double.Parse(s_value, CultureInfo.InvariantCulture);
            value *= multiplier;

            //Zkracení výsledné hodnoty - přidaní sufixu
            s_value = MathHelper.AddSufix(value);

            string output = String.Format("{0}Ω ±{1}% {2}", s_value, s_tolerance, s_trc);

            //Výstup
            tb_resistorValue.Content = output;
        }

        private Color GetColorOfStrip(Rectangle strip)
        {
            return ((SolidColorBrush)strip.Fill).Color;
        }

        //Handlers
        private void bt_3_strips_Click(object sender, RoutedEventArgs e)
        {
            SetCountOfStrips(StripsCount._3);
            ChangeResistorValue();
        }

        private void bt_4_strips_Click(object sender, RoutedEventArgs e)
        {
            SetCountOfStrips(StripsCount._4);
            ChangeResistorValue();
        }

        private void bt_5_strips_Click(object sender, RoutedEventArgs e)
        {
            SetCountOfStrips(StripsCount._5);
            ChangeResistorValue();
        }

        private void bt_6_strips_Click(object sender, RoutedEventArgs e)
        {
            SetCountOfStrips(StripsCount._6);
            ChangeResistorValue();
        }

        //Nastavi barvu prouzku - Button handler
        private void SetColorOfStrip(object sender, RoutedEventArgs e)
        {
            StackPanel sp = (StackPanel)((Grid)((Button)sender).Parent).Parent;
            Brush brush = ((Button)sender).Background;

            //Nastavi barvu prouzku
            //Resistor s 3 nebo 4 proužky má multiplier na pozici 3. proužku
            if (( strips_count == StripsCount._3 || strips_count == StripsCount._4) && sp.Name == "sp_multiplier")
            {
                strip_1.Fill = brush;
            }
            //na zaklade sloupce ve kterem se nachazi button
            else
            {
                int col = sp_colors.Children.IndexOf(sp);
                strips[col].Fill = brush;
            }

            //Přepočítá hodnotu resistoru
            ChangeResistorValue();
        }
    }
}
