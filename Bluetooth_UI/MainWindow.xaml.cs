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
using VentilatorComm;

namespace Bluetooth_UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        
        StreamWriter filestream;
        bool toFile = false;
        XmlDocument xdoc;
        DispatcherTimer timer;
        BluetoothConnection bt_conn;    


        public MainWindow()
        {
            InitializeComponent();

            List<string>  ports = new List<string>();
            string[] portlist = SerialPort.GetPortNames();
            foreach (string port in portlist)
            {
                ports.Add(port);
            }
            cbPorts.ItemsSource = ports;
            if (ports.Contains("COM18"))
            {
                cbPorts.SelectedItem = "COM18";
            }

            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(80);
            timer.Tick += timer_Tick;

            string appPath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().CodeBase);
            Commands.Source = new Uri(appPath + @"\Commands.xml");

            rbWifi.IsChecked = true;
        }

        private void Connect_Click(object sender, RoutedEventArgs e)
        {
            

            string portname = Convert.ToString(cbPorts.Text);
            bt_conn = new BluetoothConnection(portname);
            bt_conn.DataReceived += DataReceived;
            bt_conn.Connect();
            if (bt_conn.ConnectionState == "ConnectionUp")
            {
                lbConnectionState.Text = "Connection: Up!";
                lbConnectionState.Foreground = Brushes.Green;
            }
            else
            {
                MessageBox.Show(bt_conn.ConnectionState);
            }
        }

        private void DataReceived(string incomingdata)
        {
            if (toFile)
            {
                filestream.WriteLine(incomingdata);
                filestream.Flush();
            }
            else
            {
                this.Dispatcher.Invoke((Action)(() =>
                {
                    tbOutput.Text += incomingdata;
                    tbOutput.ScrollToEnd();
                }));
            }
        }

        private void Send_Click(object sender, RoutedEventArgs e)
        {
            if (bt_conn == null)
                MessageBox.Show("First try to connect to the Bluetooth module");

            string selectedcommand = cbCommands.SelectedItem.ToString();
            XmlNode root = xdoc.SelectSingleNode("//ToDoList");
            XmlNode currentNode = xdoc.SelectSingleNode("Commands/Command[Commandname='"
                + selectedcommand + "']");

            string command = currentNode.SelectSingleNode("Commandtext").InnerText;
            if (tbParam1.Text != "")
                command += "=" + tbParam1.Text;
            if (tbParam2.Text != "")
                command += "," + tbParam1.Text;
            if (tbParam3.Text != "")
                command += "," + tbParam1.Text;
            command += "\n";
          

            bool sendresult = bt_conn.Send(command);
            if (sendresult)
                tbOutput.Text += "Command sent: " + command;
            else
                tbOutput.Text += "Transmission failed.";

        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            string timestamp = DateTime.Now.ToLongTimeString();
            if (toFile == false)
            {
                btFile.Content = "Stop";
                toFile = true;

                //ToDo: lehessen útvonalat választani - pl OpenFileDialog
                string path = "C:\\Users\\Szita\\Desktop\\test.csv";
                try
                {
                    filestream = new StreamWriter(path);
                    filestream.WriteLine(timestamp);
                    filestream.WriteLine("Error;Control;Speed");
                    filestream.Flush();
                    timer.Start();
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                timer.Stop();
                filestream.WriteLine(timestamp);
                btFile.Content = "Save to file";
                toFile = false;
                filestream.Close();
            }
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            string command = "DC=";
            command += slValue.Value.ToString() + "\n";

            bt_conn.Send(command);
        }

        private void cbCommands_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string selectedcommand = null;
            object selectedItem = cbCommands.SelectedItem;
            if (selectedItem != null)
                selectedcommand = selectedItem.ToString();
            if (selectedcommand != null)
            {
                XmlNode currentNode = xdoc.SelectSingleNode("Commands/Command[Commandname='"
                    + selectedcommand + "']");
                int param_num = Convert.ToInt32(currentNode.SelectSingleNode("Param_num").InnerText);
                tbParam1.Text = "";
                tbParam2.Text = "";
                tbParam3.Text = "";
                tbParam1.IsEnabled = false;
                tbParam2.IsEnabled = false;
                tbParam3.IsEnabled = false;
                if (param_num > 0)
                {
                    tbParam1.IsEnabled = true;
                    try
                    {
                        tbParam1.Text = currentNode.SelectSingleNode("Param1_default").InnerText;
                    }
                    catch (Exception ex) { }
                }
                if (param_num > 1)
                {
                    tbParam2.IsEnabled = true;
                    try
                    {
                        tbParam2.Text = currentNode.SelectSingleNode("Param2_default").InnerText;
                    }

                    catch (Exception ex) { }
                }
                if (param_num > 2)
                {
                    tbParam3.IsEnabled = true;
                    try
                    {
                        tbParam3.Text = currentNode.SelectSingleNode("Param3_default").InnerText;
                    }

                    catch (Exception ex) { }
                }
            }
        }

        private void rbIsChecked_Changed(object sender, RoutedEventArgs e)
        {
            Fill_Commandcb();
        }

        private void Fill_Commandcb()
        {
            cbCommands.Items.Clear();
            xdoc = new XmlDocument();
            if (rbWifi.IsChecked == true)
                xdoc.Load("WifiCommands.xml");
            else
                xdoc.Load("SystemCommands.xml");
            XmlNodeList xnList = xdoc.SelectNodes("/Commands/Command");
            foreach (XmlNode node in xnList)
            {
                cbCommands.Items.Add(node.SelectSingleNode("Commandname").InnerText);
            }
            cbCommands.SelectedIndex = 0;
        }

        //ToDo: get distance, get speed checkbox lekezelése -> parancsküldés.
        //ToDo: még több parancs hozzáadása

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (bt_conn != null)
                bt_conn.Close();
            if(filestream != null)
                filestream.Close();
        }
    }
}
