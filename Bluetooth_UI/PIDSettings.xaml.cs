using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;

namespace Bluetooth_UI
{
    /// <summary>
    /// Interaction logic for PIDSettings.xaml
    /// </summary>
    public partial class PIDSettings : Window
    {
        private double kc;
        private double ti;
        private double td;


        public double Kc { get { return kc; } set { if (value >= 0) kc = value; else kc = 0; } }
        public double Ti { get { return ti; } set { if (value >= 0) ti = value; else ti = 0; } }
        public double Td { get { return td; } set { if (value >= 0) td = value; else td = 0; } }

        public PIDSettings()
        {
            InitializeComponent();

            vsParamKc.lbValueSetter.Content = "Kc";
            vsParamTi.lbValueSetter.Content = "Ti";
            vsParamTd.lbValueSetter.Content = "Td";
        }

        private void btnDialogOk_Click(object sender, RoutedEventArgs e)
        {
            double test;
            if (double.TryParse(vsParamKc.tbValueSetter.Text, out test))
            {
                Kc = test;
            }
            if (double.TryParse(vsParamTi.tbValueSetter.Text, out test))
            {
                Ti = test;
            }
            if (double.TryParse(vsParamTd.tbValueSetter.Text, out test))
            {
                Td = test;
            }
            this.DialogResult = true;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            vsParamKc.tbValue = kc.ToString();
            vsParamTi.tbValue = ti.ToString();
            vsParamTd.tbValue = td.ToString();
        }
    }
}
