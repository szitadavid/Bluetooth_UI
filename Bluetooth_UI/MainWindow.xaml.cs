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
        
        StreamWriter filestream;
        SerialPort comPort;
        bool toFile = false;
        XmlDocument xdoc;
        DispatcherTimer timer;

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

            cbBaudRate.Items.Add(300);
            cbBaudRate.Items.Add(600);
            cbBaudRate.Items.Add(1200);
            cbBaudRate.Items.Add(2400);
            cbBaudRate.Items.Add(9600);
            cbBaudRate.Items.Add(14400);
            cbBaudRate.Items.Add(19200);
            cbBaudRate.Items.Add(38400);
            cbBaudRate.Items.Add(57600);
            cbBaudRate.Items.Add(115200);
            cbBaudRate.SelectedItem = 115200;

            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(80);
            timer.Tick += timer_Tick;

            string appPath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().CodeBase);
            Commands.Source = new Uri(appPath + @"\Commands.xml");

            rbWifi.IsChecked = true;
        }

        private void Connect_Click(object sender, RoutedEventArgs e)
        {
            comPort = new SerialPort();
            comPort.BaudRate = Convert.ToInt32(cbBaudRate.Text);
            comPort.DataBits = Convert.ToInt16(8);
            comPort.StopBits = StopBits.One;
            comPort.Handshake = Handshake.None;
            comPort.Parity = Parity.None;
            try
            {
                comPort.PortName = Convert.ToString(cbPorts.Text);
                comPort.DataReceived += new SerialDataReceivedEventHandler(port_DataReceived);
                comPort.Open();
                if (comPort.IsOpen)
                {
                    lbConnectionState.Text = "Connection: Up!";
                    lbConnectionState.Foreground = Brushes.Green;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void port_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {

            string incomingdata = comPort.ReadExisting();
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
            if (comPort == null)
                MessageBox.Show("First try to connect to the Bluetooth module");
            else if (comPort.IsOpen)
            {
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
                tbOutput.Text += "Command sent: " + command;
                comPort.Write(command);
            }
            else
                MessageBox.Show("Not connected to the Bluetooth module.");

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

            comPort.Write(command);
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
            if(comPort != null)
                if(comPort.IsOpen)
                    comPort.Close();
            if(filestream != null)
                filestream.Close();
        }
    }
}
