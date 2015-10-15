using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Ports;
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
using System.Windows.Threading;
using System.Xml;


namespace Bluetooth_UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
         


        public MainWindow()
        {
            InitializeComponent();

            SettingsManager.Initialize(this);
        }

        private void Connect_Click(object sender, RoutedEventArgs e)
        {
            SettingsManager.Instance.Connect();
        }

        private void Send_Click(object sender, RoutedEventArgs e)
        {
           
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            SettingsManager.Instance.SaveToFile();
        }


        private void cbCommands_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void rbIsChecked_Changed(object sender, RoutedEventArgs e)
        {
            
        }

        //ToDo: get distance, get speed checkbox lekezelése -> parancsküldés.

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            SettingsManager.Instance.Close();
        }
    }
}
