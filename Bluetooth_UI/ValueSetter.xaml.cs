using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace Bluetooth_UI
{
    /// <summary>
    /// Interaction logic for ValueSetter.xaml
    /// </summary>
    public partial class ValueSetter : UserControl
    {
        public string tbValue
        {
            get { return (string)GetValue(tbValueProperty); }
            set { SetValue(tbValueProperty, value); }
        }
        public ValueSetter()
        {
            InitializeComponent();
            this.DataContext = this;
        }

        #region idontunderstandit
        public static readonly DependencyProperty tbValueProperty =
            DependencyProperty.Register("tbValue", typeof(string),
            typeof(ValueSetter), new FrameworkPropertyMetadata(string.Empty));
        #endregion

        private void btUp_Click(object sender, RoutedEventArgs e)
        {
            double val;
            if(double.TryParse(tbValue, out val))
            {
                val += 0.1;
                tbValue = val.ToString();
            }
        }

        private void btDown_Click(object sender, RoutedEventArgs e)
        {
            double val;
            if (double.TryParse(tbValue, out val))
            {
                val -= 0.1;
                tbValue = val.ToString();
            }
        }
    }
}
